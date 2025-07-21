using Newtonsoft.Json;
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

    public partial class MantenimientoPersonaForm : Form
    {
        private readonly DBComponent db;
        private readonly JsonLoader loader;
        public MantenimientoPersonaForm(DBComponent dbComponent, JsonLoader jsonLoader)
        {
            InitializeComponent();
            db = dbComponent ?? throw new ArgumentNullException(nameof(dbComponent));
            loader = jsonLoader ?? throw new ArgumentNullException(nameof(jsonLoader));
            this.Load += MantenimientoPersonaForm_Load;
        }

        private void MantenimientoPersonaForm_Load(object? sender, EventArgs e)
        {
            string sql = loader.BuildSql("personas_activas");
            DataTable dt = db.ExecuteQuery(sql);
            LoadPeopleInGrid();
            dataGridPersona.CellValueChanged += dataGridPersona_CellValueChanged;
            dataGridPersona.CurrentCellDirtyStateChanged += (s, ev) =>
            {
                if (dataGridPersona.IsCurrentCellDirty)
                    dataGridPersona.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            dataGridPersona.CellClick += dataGridPersona_CellClick;
            SearchBox.Click += SearchBox_Click;
            SearchBox.KeyDown += SearchBox_KeyDown;
            AddCheckColumn();
        }

        private void LoadPeopleInGrid()
        {
            string sql = loader.BuildSql("personas_activas");
            DataTable dt = db.ExecuteQuery(sql);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("No hay personas registradas.");
                dataGridPersona.DataSource = null;
                return;
            }

            if (!dt.Columns.Contains("id"))
            {
                MessageBox.Show("La consulta 'personas_activas' debe incluir la columna 'id'.");
                return;
            }

            dataGridPersona.AutoGenerateColumns = false;

            if (!dataGridPersona.Columns.Contains("Seleccionar"))
            {
                var colSelect = new DataGridViewCheckBoxColumn
                {
                    Name = "Seleccionar",
                    HeaderText = "✔",
                    Width = 40,
                    ReadOnly = false,
                    TrueValue = true,
                    FalseValue = false
                };

                dataGridPersona.Columns.Insert(0, colSelect);
            }

            dataGridPersona.Columns["CI"].DataPropertyName = "cedula";
            dataGridPersona.Columns["Nombre"].DataPropertyName = "nombre";
            dataGridPersona.Columns["Apellido"].DataPropertyName = "apellido";
            dataGridPersona.Columns["TipoPersona"].DataPropertyName = "tipo_persona";


            dataGridPersona.DataSource = dt;
            dataGridPersona.ClearSelection();

            dataGridPersona.ReadOnly = false;
            dataGridPersona.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dataGridPersona.AllowUserToAddRows = false;
            dataGridPersona.Columns["Seleccionar"].ReadOnly = false;
        }

        private void dataGridPersona_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var grid = dataGridPersona;
            if (grid.Columns[e.ColumnIndex].Name != "Edit")
                return;

            if (grid.Rows[e.RowIndex].DataBoundItem is DataRowView drv)
            {
                object rawId = drv.Row["id"];
                if (rawId == null || !int.TryParse(rawId.ToString(), out int idPersona))
                {
                    MessageBox.Show("No se puede determinar el ID de la persona.");
                    return;
                }

                OpenEditarPersonaForm(idPersona);
            }
        }

        private void OpenEditarPersonaForm(int idPersona)
        {
            using var editarForm = new EditarPersonaForm(db, loader, idPersona);
            editarForm.ShowDialog();
            LoadPeopleInGrid();
        }


        private void dataGridPersona_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (dataGridPersona.Columns[e.ColumnIndex].Name != "Seleccionar")
                return;

            var fila = dataGridPersona.Rows[e.RowIndex];
            bool seleccionado = fila.Cells["Seleccionar"].Value is true;

            fila.DefaultCellStyle.BackColor = seleccionado ? Color.LightGoldenrodYellow : Color.White;
        }

        public void ShowFilteredResults(DataTable dt)
        {
            dataGridPersona.DataSource = null;
            dataGridPersona.Rows.Clear();
            dataGridPersona.Refresh();

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron personas con ese filtro.");
                return;
            }

            var dtSinDuplicados = dt.DefaultView.ToTable(true, "cedula", "nombre", "apellido", "tipo_persona");

            dataGridPersona.AutoGenerateColumns = false;
            dataGridPersona.Columns["CI"].DataPropertyName = "cedula";
            dataGridPersona.Columns["Nombre"].DataPropertyName = "nombre";
            dataGridPersona.Columns["Apellido"].DataPropertyName = "apellido";
            dataGridPersona.Columns["TipoPersona"].DataPropertyName = "tipo_persona";

            dataGridPersona.DataSource = dtSinDuplicados;
            dataGridPersona.ClearSelection();

            dataGridPersona.ReadOnly = false;
            dataGridPersona.AllowUserToAddRows = false;
            dataGridPersona.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;

            if (dataGridPersona.Columns.Contains("Seleccionar"))
                dataGridPersona.Columns["Seleccionar"].ReadOnly = false;

            MessageBox.Show($"Se encontraron {dtSinDuplicados.Rows.Count} personas.");
        }

        private void SearchBox_Click(object? sender, EventArgs e)
        {
            string cedula = SearchBox.Text?.Trim() ?? "";
            SearchByCI(cedula);
        }
        private void SearchBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cedula = SearchBox.Text?.Trim() ?? "";
                SearchByCI(cedula);
                e.SuppressKeyPress = true;
            }
        }

        private List<int> GetCheckedIDs()
        {
            var ids = new List<int>();

            var data = dataGridPersona.DataSource as DataTable;
            if (data == null)
                return ids;

            for (int i = 0; i < dataGridPersona.Rows.Count; i++)
            {
                bool seleccionada = Convert.ToBoolean(dataGridPersona.Rows[i].Cells["Seleccionar"].Value);
                if (seleccionada)
                {
                    object raw = data.Rows[i]["id"];
                    if (raw != null && int.TryParse(raw.ToString(), out int id))
                        ids.Add(id);
                }
            }
            return ids;
        }

        private void AddCheckColumn()
        {
            if (!dataGridPersona.Columns.Contains("Seleccionar"))
            {
                var colCheck = new DataGridViewCheckBoxColumn
                {
                    Name = "Seleccionar",
                    HeaderText = "✔",
                    Width = 40,
                    ReadOnly = false,
                    TrueValue = true,
                    FalseValue = false
                };

                dataGridPersona.Columns.Insert(0, colCheck);
            }
        }


        private void SearchByCI(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
            {
                MessageBox.Show("Por favor ingrese una cédula para buscar.");
                return;
            }

            var parametros = new List<NpgsqlParameter>
    {
        new NpgsqlParameter("@cedula", cedula.Trim())
    };

            string sql = loader.BuildSql("persona_por_cedula", parametros);
            DataTable resultado = db.ExecuteQuery(sql, parametros.ToArray());

            ShowFilteredResults(resultado);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var seleccionados = GetCheckedIDs();

            if (seleccionados.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos una persona para borrar.");
                return;
            }

            DialogResult r = MessageBox.Show(
                $"¿Seguro que desea borrar {seleccionados.Count} persona(s)?",
                "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (r == DialogResult.Yes)
            {
                foreach (int id in seleccionados)
                {
                    var parametros = new[] { new NpgsqlParameter("@id", id) };
                    string sql = loader.BuildSql("borrar_persona_por_id");
                    db.ExecuteNonQuery(sql, parametros);
                }

                MessageBox.Show("Personas borradas correctamente.");
                LoadPeopleInGrid();
            }
        }


        private void BtnFilter_Click(object sender, EventArgs e)
        {
            using (var filtroForm = new FiltroPersonaForm(db, loader))
            {
                filtroForm.Owner = this;
                filtroForm.ShowDialog();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var crearForm = new CrearPersonaForm(db, loader))
            {
                crearForm.ShowDialog();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
