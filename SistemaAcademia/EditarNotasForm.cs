using Npgsql;
using System.Data;

namespace SistemaAcademia
{
    public partial class EditarNotasForm : Form
    {
        private int currentCursoId;
        private int currentPersonaId;
        private NpgsqlConnection connection;
        private NpgsqlTransaction transaction;
        private JsonLoader loader;
        private readonly DBComponent db;

        public EditarNotasForm(DBComponent dbComponent, JsonLoader loader, int idPersona, int idCurso, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            InitializeComponent();

            db = dbComponent;
            this.loader = loader;

            currentPersonaId = idPersona;
            currentCursoId = idCurso;
            connection = conn;
            transaction = tx;

            LoadExistingGrades(idPersona, idCurso);
            btnGuardar.Click += BtnGuardar_Click;
            comboBoxNumero.SelectedIndexChanged += ComboBoxNumero_SelectedIndexChanged;
        }



        private void LoadExistingGrades(int idPersona, int idCurso)
        {
            string sql = loader.BuildSql("notas_existentes_por_estudiante");
            var parameters = new[]
            {
        new NpgsqlParameter("@persona", idPersona),
        new NpgsqlParameter("@curso", idCurso)
    };

            DataTable dt = db.ExecuteQuery(sql, parameters);
            comboBoxNumero.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                int numero = Convert.ToInt32(row["numero"]);
                comboBoxNumero.Items.Add($"N{numero}");
            }

            if (comboBoxNumero.Items.Count > 0)
                comboBoxNumero.SelectedIndex = 0;
        }


        private void ComboBoxNumero_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboBoxNumero.SelectedItem is not string selected || selected.Length < 2)
                return;

            int numero;
            try
            {
                numero = int.Parse(selected.Substring(1));
            }
            catch
            {
                return;
            }

            string sql = loader.BuildSql("nota_por_numero");

            var parameters = new[]
            {
        new NpgsqlParameter("@persona", currentPersonaId),
        new NpgsqlParameter("@curso", currentCursoId),
        new NpgsqlParameter("@numero", numero)
    };

            DataTable dt = db.ExecuteQuery(sql, parameters);

            if (dt.Rows.Count > 0)
            {
                var notaRaw = dt.Rows[0]["nota"];
                var porcentajeRaw = dt.Rows[0]["porcentaje"];

                textBoxNota.Text = notaRaw != DBNull.Value ? string.Format("{0:0.0}", notaRaw) : "";
                textBoxPorcentaje.Text = porcentajeRaw != DBNull.Value ? string.Format("{0:0.0}", porcentajeRaw) : "";
            }
        }


        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (comboBoxNumero.SelectedItem is not string selected || selected.Length < 2)
                return;
            int numero = int.Parse(selected.Substring(1));

            if (!double.TryParse(textBoxNota.Text, out double nota) || nota < 0 || nota > 20)
            {
                MessageBox.Show("Ingrese una nota válida entre 0 y 20.");
                return;
            }

            if (!double.TryParse(textBoxPorcentaje.Text, out double porcentaje) || porcentaje < 0 || porcentaje > 100)
            {
                MessageBox.Show("Ingrese un porcentaje válido entre 0 y 100.");
                return;
            }

            string sql = @"
        UPDATE calificacion
        SET nota = @nota, porcentaje = @porcentaje
        WHERE id_persona = @persona AND id_curso = @curso AND numero = @numero";

            var parameters = new[]
            {
        new NpgsqlParameter("@nota", nota),
        new NpgsqlParameter("@porcentaje", porcentaje),
        new NpgsqlParameter("@persona", currentPersonaId),
        new NpgsqlParameter("@curso", currentCursoId),
        new NpgsqlParameter("@numero", numero)
    };

            int rowsAffected = db.ExecuteNonQuery(sql, parameters);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Nota actualizada correctamente.");
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar la nota.");
            }
        }
    }
}
