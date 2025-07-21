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
    public partial class FiltroPersonaForm : Form
    {
        private readonly DBComponent db;
        private readonly JsonLoader loader;

        public FiltroPersonaForm(DBComponent dbComponent, JsonLoader jsonLoader)
        {
            InitializeComponent();
            this.Load += FiltroPersonaForm_Load;
            db = dbComponent ?? throw new ArgumentNullException(nameof(dbComponent));
            loader = jsonLoader ?? throw new ArgumentNullException(nameof(jsonLoader));
        }


        private void FiltroPersonaForm_Load(object? sender, EventArgs e)
        {
            comboTipo.SelectedIndex = -1;

            LoadComboTipo();
            LoadComboMaterias();
            LoadComboSeccion();
        }

        private void LoadComboTipo()
        {
            try
            {
                comboTipo.Items.Clear();

                DataTable tipos = db.ExecuteQuery(loader.BuildSql("tipos_de_persona"));

                if (tipos == null || tipos.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron tipos de persona en la base.");
                    return;
                }

                foreach (DataRow row in tipos.Rows)
                {
                    string? descripcion = row["descripcion"]?.ToString()?.Trim();
                    if (!string.IsNullOrWhiteSpace(descripcion))
                        comboTipo.Items.Add(descripcion);
                }

                comboTipo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tipos de persona: " + ex.Message);
            }
        }


        private void LoadComboMaterias()
        {
            DataTable materias = db.ExecuteQuery(loader.BuildSql("materias_activas"));

            if (materias == null || materias.Rows.Count == 0)
            {
                MessageBox.Show("No hay materias registradas.");
                comboMateria.DataSource = null;
                return;
            }

            comboMateria.DataSource = materias;
            comboMateria.DisplayMember = "nombre";
            comboMateria.ValueMember = "nombre";
            comboMateria.SelectedIndex = -1;
        }

        private void LoadComboSeccion()
        {
            DataTable secciones = db.ExecuteQuery(loader.BuildSql("secciones_activas"));

            if (secciones == null || secciones.Rows.Count == 0)
            {
                MessageBox.Show("No hay secciones disponibles.");
                comboSeccion.DataSource = null;
                return;
            }

            comboSeccion.DataSource = secciones;
            comboSeccion.DisplayMember = "nombre";
            comboSeccion.ValueMember = "nombre";
            comboSeccion.SelectedIndex = -1;
        }


        private void BtnSaveFilter_Click(object sender, EventArgs e)
        {
            var parametros = new List<NpgsqlParameter>();

            if (!string.IsNullOrWhiteSpace(comboTipo.Text))
                parametros.Add(new NpgsqlParameter("@tipo", comboTipo.Text.Trim()));

            if (!string.IsNullOrWhiteSpace(comboMateria.Text))
                parametros.Add(new NpgsqlParameter("@materia", comboMateria.Text.Trim()));

            if (!string.IsNullOrWhiteSpace(comboSeccion.Text))
                parametros.Add(new NpgsqlParameter("@seccion", comboSeccion.Text.Trim()));

            string tipoSeleccionado = comboTipo.Text.Trim();
            string bloqueJson = tipoSeleccionado == "Profesor" ? "docentes_filtrados" : "personas_filtradas";
            string sql = loader.BuildSql(bloqueJson, parametros);
            DataTable resultado = db.ExecuteQuery(sql, parametros.ToArray());

            if (this.Owner is MantenimientoPersonaForm parent)
            {
                parent.ShowFilteredResults(resultado);
            }

            this.Close();
        }
    }
}
