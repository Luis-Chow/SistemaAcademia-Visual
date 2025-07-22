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
    public partial class MantenimientoSeccionForm : Form
    {
        private readonly JsonLoader loader;
        private readonly DBComponent db;
        public MantenimientoSeccionForm(DBComponent dbComponent, JsonLoader jsonLoader)
        {
            InitializeComponent();
            db = dbComponent ?? throw new ArgumentNullException(nameof(dbComponent));
            loader = jsonLoader ?? throw new ArgumentNullException(nameof(jsonLoader));
            this.Load += MantenimientoSeccionForm_Load;
        }

        private void MantenimientoSeccionForm_Load(object? sender, EventArgs e)
        {
            LoadSecciones();
            gridSeccion.CellValueChanged += gridSeccion_CellValueChanged;
            gridSeccion.CurrentCellDirtyStateChanged += gridSeccion_CurrentCellDirtyStateChanged;

        }


        private void BtnRetroceso_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private List<int> GetCheckedSeccionIDs()
        {
            var ids = new List<int>();

            foreach (DataGridViewRow fila in gridSeccion.Rows)
            {
                if (fila.Cells["Seleccionar"] is DataGridViewCheckBoxCell check &&
                    check.Value is bool marcado && marcado)
                {
                    object raw = fila.Cells["ID"].Value;
                    if (raw != null && int.TryParse(raw.ToString(), out int id))
                        ids.Add(id);
                }
            }

            return ids;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var seleccionados = GetCheckedSeccionIDs();

            if (seleccionados.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos una sección para borrar.");
                return;
            }

            DialogResult r = MessageBox.Show(
                $"¿Seguro que desea borrar {seleccionados.Count} sección(es)?",
                "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (r != DialogResult.Yes) return;

            try
            {
                db.BeginTransaction();

                foreach (int id in seleccionados)
                {
                    string sql = loader.BuildSql("borrar_seccion_por_id");
                    db.ExecuteNonQuery(sql, new[] { new NpgsqlParameter("@id", id) });
                }

                db.Commit();
                MessageBox.Show("Secciones borradas correctamente.");
                LoadSecciones();
            }
            catch (Exception ex)
            {
                db.Rollback();
                MessageBox.Show("Error al borrar secciones: " + ex.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreSeccion.Text.Trim().ToUpper();

            // 🔎 Validación de formato
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre de la sección es obligatorio.");
                return;
            }

            if (nombre.Length != 1 || !char.IsLetter(nombre[0]))
            {
                MessageBox.Show("La sección debe ser una sola letra (A-Z).");
                return;
            }

            try
            {
                // 🔍 Verificación de existencia previa
                string existeSql = loader.BuildSql("seccion_existente_por_nombre");
                var paramExiste = new[] { new NpgsqlParameter("@nombre", nombre) };
                DataTable existe = db.ExecuteQuery(existeSql, paramExiste);

                if (existe.Rows.Count > 0)
                {
                    MessageBox.Show("Ya existe una sección con ese nombre.");
                    return;
                }

                // 📥 Inserción segura
                string insertarSql = loader.BuildSql("insertar_seccion");
                var paramInsertar = new[] { new NpgsqlParameter("@nombre", nombre) };

                // ✨ Validación del SQL generado (opcional para depurar)
                // MessageBox.Show($"SQL:\n{insertarSql}");

                int filas = db.ExecuteNonQuery(insertarSql, paramInsertar);

                if (filas > 0)
                {
                    MessageBox.Show("Sección creada correctamente.");
                    txtNombreSeccion.Clear();
                    LoadSecciones();
                }
                else
                {
                    MessageBox.Show("No se pudo insertar la sección.");
                }
            }
            catch (PostgresException pgEx) when (pgEx.SqlState == "23505") // Violación de clave única
            {
                MessageBox.Show("Ya existe una sección con ese nombre (clave duplicada).");
            }
            catch (PostgresException pgEx)
            {
                MessageBox.Show($"Error de PostgreSQL:\n{pgEx.Message}\nCódigo: {pgEx.SqlState}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado:\n{ex.Message}");
            }
        }


        private void gridSeccion_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (gridSeccion.Columns[e.ColumnIndex].Name != "Seleccionar")
                return;

            var fila = gridSeccion.Rows[e.RowIndex];
            bool seleccionado = fila.Cells["Seleccionar"].Value is true;

            fila.DefaultCellStyle.BackColor = seleccionado ? Color.LightGoldenrodYellow : Color.White;
        }

        private void gridSeccion_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (gridSeccion.IsCurrentCellDirty)
                gridSeccion.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }


        private void LoadSecciones()
        {
            string sql = loader.BuildSql("secciones_todas");
            DataTable dt = db.ExecuteQuery(sql);

            gridSeccion.AutoGenerateColumns = false;

            if (!gridSeccion.Columns.Contains("Seleccionar"))
            {
                var colCheck = new DataGridViewCheckBoxColumn
                {
                    Name = "Seleccionar",
                    HeaderText = "",
                    Width = 40
                };
                gridSeccion.Columns.Insert(0, colCheck);
            }

            gridSeccion.Columns["ID"].DataPropertyName = "id";
            gridSeccion.Columns["Seccion"].DataPropertyName = "nombre";

            gridSeccion.DataSource = dt;
            gridSeccion.ClearSelection();

            gridSeccion.ReadOnly = false;
            gridSeccion.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            gridSeccion.AllowUserToAddRows = false;
            gridSeccion.Columns["Seleccionar"].ReadOnly = false;
        }

    }
}
