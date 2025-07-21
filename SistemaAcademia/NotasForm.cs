using Npgsql;
using System.Data;

namespace SistemaAcademia
{
    public partial class NotasForm : Form
    {
        private readonly JsonLoader loader;
        private readonly DBComponent db;
        private NpgsqlConnection connection;
        private NpgsqlTransaction? currentTransaction;
        private bool actualizandoSecciones = false;
        private bool transaccionActiva = false;

        public NotasForm(DBComponent dbComponent, JsonLoader jsonLoader)
        {
            InitializeComponent();

            db = dbComponent;
            loader = jsonLoader;

            connection = db.Connection ?? throw new InvalidOperationException("Conexión no inicializada.");

            Console.WriteLine(loader.BuildSql("notas_por_materia_y_seccion"));

            dataGridView1.RowTemplate.Height = 32;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dataGridView1.Columns.Add(new DataGridViewImageColumn
            {
                Name = "Anadir",
                HeaderText = "Añadir",
                ImageLayout = DataGridViewImageCellLayout.Zoom
            });

            dataGridView1.Columns.Add(new DataGridViewImageColumn
            {
                Name = "Edit",
                HeaderText = "Editar",
                ImageLayout = DataGridViewImageCellLayout.Zoom
            });

            InitComboBoxes();

            comboBoxMateria.SelectedIndexChanged += comboBoxMateria_SelectedIndexChanged;
            comboBoxSeccion.SelectedIndexChanged += ComboBoxes_SelectionChanged;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;

            comboBoxMateria.SelectedIndex = 0;
        }


        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (transaccionActiva)
                {
                    currentTransaction?.Commit();
                    transaccionActiva = false;
                    currentTransaction = null;
                }

                MessageBox.Show("Cambios guardados.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }

            connection.Close();
            this.Close();
        }

        private void BtnRetroceso_Click(object sender, EventArgs e)
        {
            try
            {
                if (transaccionActiva)
                {
                    currentTransaction?.Rollback();
                    transaccionActiva = false;
                    currentTransaction = null;
                }

                MessageBox.Show("Cambios cancelados.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cancelar: " + ex.Message);
            }

            connection.Close();
            this.Close();
        }

        private void ComboBoxes_SelectionChanged(object? sender, EventArgs? e)
        {
            if (actualizandoSecciones) return;

            if (comboBoxMateria.SelectedItem is not DataRowView materiaRow ||
                comboBoxSeccion.SelectedItem is not DataRowView seccionRow)
                return;

            int selectedMateriaId = (int)materiaRow["id"];
            int selectedSeccionId = (int)seccionRow["id"];

            var sql = loader.BuildSql("notas_por_materia_y_seccion");

            var parameters = new[]
            {
        new NpgsqlParameter("@materia", selectedMateriaId),
        new NpgsqlParameter("@seccion", selectedSeccionId)
    };

            DataTable dt;
            try
            {
                dt = db.ExecuteQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ejecutar la consulta: {ex.Message}");
                return;
            }

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Rows.Clear();

            foreach (DataRow row in dt.Rows)
            {
                int rowIndex = dataGridView1.Rows.Add();
                var gridRow = dataGridView1.Rows[rowIndex];

                gridRow.Cells["Estudiante"].Value = row["estudiante"];
                gridRow.Cells["CI"].Value = row["cedula"];
                gridRow.Cells["Seccion"].Value = row["seccion"];

                gridRow.Cells["NotaFinal"].Value =
                    row["nota_final"] is DBNull or null ? "—" : string.Format("{0:0.0}", row["nota_final"]);

                for (int i = 1; i <= 6; i++)
                {
                    string key = $"n{i}";
                    string col = $"N{i}";
                    var raw = row[key];
                    gridRow.Cells[col].Value = raw is DBNull or null ? "—" : string.Format("{0:0.0}", raw);
                }

                gridRow.Cells["Anadir"].Value = Properties.Resources.BotonAdd;
                gridRow.Cells["Edit"].Value = Properties.Resources.BotonEditar;
            }

            if (dt.Rows.Count == 0)
                MessageBox.Show("No se encontraron notas para la materia y sección seleccionadas.");

            dataGridView1.Refresh();
        }

        private void dataGridView1_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];
            string cedula = row.Cells["CI"].Value?.ToString() ?? "";
            int idPersona = GetPersonaIdByCedula(cedula);

            if (comboBoxMateria.SelectedItem is not DataRowView materiaView ||
                comboBoxSeccion.SelectedItem is not DataRowView seccionView)
                return;

            int materiaId = (int)materiaView["id"];
            int seccionId = (int)seccionView["id"];
            int cursoId = GetCursoId(materiaId, seccionId);
            int numero = GetFirstEmptyNumber(row);

            if (!transaccionActiva)
            {
                currentTransaction = connection.BeginTransaction();
                transaccionActiva = true;
            }

            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index)
            {
                var editForm = new EditarNotasForm(db, loader, idPersona, cursoId, connection, currentTransaction!);
                editForm.ShowDialog();
                ComboBoxes_SelectionChanged(null, null);
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Anadir"].Index)
            {
                if (numero == 8)
                {
                    MessageBox.Show("Este estudiante ya tiene todas las notas registradas.");
                    return;
                }

                var agregarForm = new CrearNotaForm(idPersona, cursoId, numero, db, loader, currentTransaction!);
                agregarForm.ShowDialog();
                ComboBoxes_SelectionChanged(null, null);
            }
        }

        private int GetFirstEmptyNumber(DataGridViewRow row)
        {
            string[] nombres = { "n1", "n2", "n3", "n4", "n5", "n6", "n7" };

            for (int i = 0; i < nombres.Length; i++)
            {
                var celda = row.Cells[nombres[i]].Value;

                bool estaVacia =
                    celda == null ||
                    celda == DBNull.Value ||
                    string.IsNullOrWhiteSpace(celda.ToString()) ||
                    !double.TryParse(celda.ToString(), out _);

                if (estaVacia)
                    return i + 1;
            }

            return 8;
        }

        private int GetPersonaIdByCedula(string cedula)
        {
            string sql = loader.BuildSql("buscar_persona_por_cedula");

            var parameters = new[]
            {
        new NpgsqlParameter("@cedula", cedula)
    };

            DataTable dt = db.ExecuteQuery(sql, parameters);
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["id"]) : -1;
        }

        private int GetCursoId(int materiaId, int seccionId)
        {
            string sql = loader.BuildSql("get_curso_por_materia_y_seccion");

            var parameters = new[]
            {
        new NpgsqlParameter("@materia", materiaId),
        new NpgsqlParameter("@seccion", seccionId)
    };

            DataTable dt = db.ExecuteQuery(sql, parameters);
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["id"]) : -1;
        }

        private void comboBoxMateria_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboBoxMateria.SelectedItem is not DataRowView materiaRow)
                return;

            int idMateria = (int)materiaRow["id"];
            string sql = loader.BuildSql("secciones_por_materia");

            var parameters = new[] {
        new NpgsqlParameter("@materia", idMateria)
    };
            actualizandoSecciones = true;

            DataTable seccionesDisponibles = db.ExecuteQuery(sql, parameters);
            comboBoxSeccion.DataSource = seccionesDisponibles;
            comboBoxSeccion.DisplayMember = "nombre";
            comboBoxSeccion.ValueMember = "id";

            if (seccionesDisponibles.Rows.Count > 0)
                comboBoxSeccion.SelectedIndex = 0;

            actualizandoSecciones = false;

            ComboBoxes_SelectionChanged(null, null);
        }

        private void InitComboBoxes()
        {
            LoadComboBoxes();

            comboBoxMateria.SelectedIndexChanged += comboBoxMateria_SelectedIndexChanged;
            comboBoxSeccion.SelectedIndexChanged += ComboBoxes_SelectionChanged;

            comboBoxMateria.SelectedIndex = 0;
        }
    }
}
