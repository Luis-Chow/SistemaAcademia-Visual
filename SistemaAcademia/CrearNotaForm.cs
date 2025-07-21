using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace SistemaAcademia
{
    public partial class CrearNotaForm : Form
    {
        private readonly int idPersona;
        private readonly int idCurso;
        private readonly int numero;
        private readonly double notaMaxima = 20;
        private readonly double porcentajeMaximo = 100;

        private readonly DBComponent db;
        private readonly JsonLoader loader;
        private readonly NpgsqlTransaction transaction;

        public CrearNotaForm(int idPersona, int idCurso, int numero, DBComponent db, JsonLoader loader, NpgsqlTransaction tx)
        {
            InitializeComponent();

            this.idPersona = idPersona;
            this.idCurso = idCurso;
            this.numero = numero;
            this.db = db;
            this.loader = loader;
            this.transaction = tx;

            BtnGuardar.Click += BtnGuardar_Click;
        }

        private void BtnGuardar_Click(object? sender, EventArgs? e)
        {
            if (!double.TryParse(textBoxNota?.Text, out double nota) || nota < 0 || nota > notaMaxima)
            {
                MessageBox.Show("Ingrese una nota válida entre 0 y 20.");
                return;
            }

            if (!double.TryParse(textBoxPorcentaje?.Text, out double porcentaje) || porcentaje < 0 || porcentaje > porcentajeMaximo)
            {
                MessageBox.Show("Ingrese un porcentaje válido entre 0 y 100.");
                return;
            }

            string sql;
            try
            {
                sql = loader.BuildSql("notas_insertar_nueva");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar la consulta desde JSON: {ex.Message}");
                return;
            }

            using var cmd = new NpgsqlCommand(sql, db.Connection, transaction);
            cmd.Parameters.AddWithValue("@persona", idPersona);
            cmd.Parameters.AddWithValue("@curso", idCurso);
            cmd.Parameters.AddWithValue("@numero", numero);
            cmd.Parameters.AddWithValue("@nota", nota);
            cmd.Parameters.AddWithValue("@porcentaje", porcentaje);

            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Nota agregada correctamente.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar la nota.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar la nota: {ex.Message}");
            }
        }
    }
}
