using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAcademia
{
    public partial class EditarMateriaForm : Form
    {
        private readonly DBComponent db;
        private readonly JsonLoader loader;
        private readonly string codigoMateria;

        public EditarMateriaForm(DBComponent dbComponent, JsonLoader jsonLoader, string codigo)
        {
            InitializeComponent();
            db = dbComponent;
            loader = jsonLoader;
            codigoMateria = codigo;
            this.Load += EditarMateriaForm_Load;
        }

        private void EditarMateriaForm_Load(object? sender, EventArgs e)
        {
            var parametros = new[] { new NpgsqlParameter("@codigo", codigoMateria) };
            string sql = loader.BuildSql("materia_por_codigo", parametros.ToList());
            DataTable resultado = db.ExecuteQuery(sql, parametros);

            if (resultado.Rows.Count == 0)
            {
                MessageBox.Show("No se encontró la materia.");
                Close();
                return;
            }

            var materia = resultado.Rows[0];
            txtCodigo.Text = materia["codigo"].ToString();
            txtMateria.Text = materia["nombre"].ToString();
            txtHC.Text = materia["unidad_credito"].ToString();
            txtTrimestre.Text = materia["trimestre"].ToString();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string nuevoNombre = txtMateria.Text.Trim();
            string hcTexto = txtHC.Text.Trim();
            string trimestre = txtTrimestre.Text.Trim();

            if (string.IsNullOrWhiteSpace(nuevoNombre) ||
                string.IsNullOrWhiteSpace(hcTexto) ||
                string.IsNullOrWhiteSpace(trimestre))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            if (!int.TryParse(hcTexto, out int unidadCredito) || unidadCredito <= 0)
            {
                MessageBox.Show("HC debe ser un número entero positivo.");
                return;
            }

            var parametros = new List<NpgsqlParameter>
        {
            new("@codigo", codigoMateria),
            new("@nombre", nuevoNombre),
            new("@unidad_credito", unidadCredito),
            new("@trimestre", trimestre)
        };

            try
            {
                db.BeginTransaction();

                string sql = loader.BuildSql("update_materia");
                db.ExecuteNonQuery(sql, parametros.ToArray());

                db.Commit();
                MessageBox.Show("Materia actualizada correctamente.");
                Close();
            }
            catch (Exception ex)
            {
                db.Rollback();
                MessageBox.Show("Error al actualizar materia: " + ex.Message);
            }
        }
    }
}
