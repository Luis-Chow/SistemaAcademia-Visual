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
    public partial class EditarPersonaForm : Form
    {
        private readonly DBComponent db;
        private readonly JsonLoader loader;
        private readonly int personaId;

        public EditarPersonaForm(DBComponent dbComponent, JsonLoader jsonLoader, int id)
        {
            InitializeComponent();
            db = dbComponent;
            loader = jsonLoader;
            personaId = id;
            this.Load += EditarPersonaForm_Load;
        }

        private void EditarPersonaForm_Load(object? sender, EventArgs e)
        {
            var parametros = new[] { new NpgsqlParameter("@id", personaId) };
            string sql = loader.BuildSql("persona_por_id", parametros.ToList());
            DataTable resultado = db.ExecuteQuery(sql, parametros);

            if (resultado.Rows.Count == 0)
            {
                MessageBox.Show("No se encontró la persona.");
                Close();
                return;
            }

            LoadComboTipo();

            var persona = resultado.Rows[0];
            txtCi.Text = persona["cedula"].ToString();
            txtNombre.Text = persona["nombre"].ToString();
            txtApellido.Text = persona["apellido"].ToString();
            ComboTipo.SelectedItem = persona["tipo_persona"].ToString();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string ci = txtCi.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();

            if (string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(apellido) ||
                ComboTipo.SelectedIndex < 0)
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            int tipoId = Convert.ToInt32(ComboTipo.SelectedValue);

            var parametros = new List<NpgsqlParameter>
    {
        new("@cedula", ci),
        new("@nombre", nombre),
        new("@apellido", apellido),
        new("@tipo", tipoId),
        new("@id", personaId)
    };

            string sql = loader.BuildSql("update_persona");
            db.ExecuteNonQuery(sql, parametros.ToArray());

            MessageBox.Show("Persona actualizada correctamente.");
            Close();
        }

        private void LoadComboTipo()
        {
            DataTable tipos = db.ExecuteQuery(loader.BuildSql("tipos_de_persona2"));

            if (tipos == null || tipos.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron tipos de persona.");
                ComboTipo.DataSource = null;
                return;
            }

            ComboTipo.DataSource = tipos;
            ComboTipo.DisplayMember = "descripcion";  // lo que se ve
            ComboTipo.ValueMember = "id";             // lo que se usa internamente
            ComboTipo.SelectedIndex = -1;
        }


    }
}
