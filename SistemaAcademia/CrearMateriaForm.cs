using Npgsql;

namespace SistemaAcademia
{
    public partial class CrearMateriaForm : Form
    {
        private readonly DBComponent db;
        private readonly JsonLoader loader;
        public CrearMateriaForm(DBComponent dbComponent, JsonLoader jsonLoader)
        {
            InitializeComponent();
            db = dbComponent ?? throw new ArgumentNullException(nameof(dbComponent));
            loader = jsonLoader ?? throw new ArgumentNullException(nameof(jsonLoader));
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text.Trim();
            string nombre = txtMateria.Text.Trim();
            string hcTexto = txtHC.Text.Trim();
            string trimestre = txtTrimestre.Text.Trim();

            if (string.IsNullOrWhiteSpace(codigo) ||
                string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(hcTexto) ||
                string.IsNullOrWhiteSpace(trimestre))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            if (!int.TryParse(hcTexto, out int unidadCredito) || unidadCredito <= 0)
            {
                MessageBox.Show("Las unidades crédito deben ser un número entero positivo.");
                return;
            }

            var parametros = new List<NpgsqlParameter>
        {
            new("@codigo", codigo),
            new("@nombre", nombre),
            new("@unidad_credito", unidadCredito),
            new("@trimestre", trimestre)
        };

            try
            {
                db.BeginTransaction();

                string sql = loader.BuildSql("insertar_materia");
                int filas = db.ExecuteNonQuery(sql, parametros.ToArray());

                db.Commit();

                if (filas > 0)
                {
                    MessageBox.Show("Materia creada exitosamente.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo insertar la materia.");
                }
            }
            catch (Exception ex)
            {
                db.Rollback();
                MessageBox.Show("Error al guardar materia: " + ex.Message);
            }
        }
    }
}

