using Npgsql;
using System.Data;

namespace SistemaAcademia
{
    public partial class CrearPersonaForm : Form
    {   
        private readonly DBComponent db;
        private readonly JsonLoader loader;
        public CrearPersonaForm(DBComponent dbComponent, JsonLoader jsonLoader)
        {
            InitializeComponent();
            db = dbComponent ?? throw new ArgumentNullException(nameof(dbComponent));
            loader = jsonLoader ?? throw new ArgumentNullException(nameof(jsonLoader));
            this.Load += CrearPersonaForm_Load;
        }

        private void CrearPersonaForm_Load(object? sender, EventArgs e)
        {
            LoadComboTipoPersona();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string ci = txtCi.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            int? tipoId = ComboTipo.SelectedValue as int?;

            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || tipoId == null)
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            var parametros = new List<NpgsqlParameter>
    {
        new NpgsqlParameter("@cedula", ci),
        new NpgsqlParameter("@nombre", nombre),
        new NpgsqlParameter("@apellido", apellido),
        new NpgsqlParameter("@id_tipo_persona", tipoId)
    };

            string sql = loader.BuildSql("insertar_persona");
            int filas = db.ExecuteNonQuery(sql, parametros.ToArray());

            if (filas > 0)
            {
                MessageBox.Show("Persona creada exitosamente.");
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo insertar la persona.");
            }
        }


        private void LoadComboTipoPersona()
        {
            ComboTipo.Items.Clear();

            DataTable tipos = db.ExecuteQuery(loader.BuildSql("tipos_de_persona2"));

            if (tipos == null || tipos.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron tipos de persona.");
                return;
            }

            ComboTipo.DataSource = tipos;
            ComboTipo.DisplayMember = "descripcion";
            ComboTipo.ValueMember = "id";
            ComboTipo.SelectedIndex = -1;
        }
    }
}
