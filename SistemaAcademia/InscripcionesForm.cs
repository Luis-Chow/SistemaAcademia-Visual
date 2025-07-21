using Npgsql;
using System.Data;

namespace SistemaAcademia
{
    public partial class InscripcionesForm : Form
    {
        private int? idEstudianteActual = null;
        private readonly DBComponent db;
        private readonly JsonLoader loader;

        public InscripcionesForm(DBComponent dbComponent, JsonLoader jsonLoader)
        {
            InitializeComponent();
            this.Load += InscripcionesForm_Load;
            SearchBox.KeyDown += SearchBox_KeyDown;
            db = dbComponent ?? throw new ArgumentNullException(nameof(dbComponent));
            loader = jsonLoader ?? throw new ArgumentNullException(nameof(jsonLoader));
        }

        private void InscripcionesForm_Load(object? sender, EventArgs e)
        {
            dataGridCursos.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridCursos.ReadOnly = false;
            dataGridCursos.SelectionChanged += dataGridCursos_SelectionChanged;
            SearchBox.Click += SearchBox_Click;
            dataGridHorario.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridHorario.RowTemplate.Height = 60; 
            dataGridCursos.AutoGenerateColumns = false;
            dataGridHorario.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dataGridCursos.Rows.Clear();
            dataGridHorario.Rows.Clear();
            dataGridHorario.ReadOnly = false; 
            dataGridHorario.Enabled = true;    
            dataGridHorario.AllowUserToAddRows = false;
            dataGridHorario.AllowUserToDeleteRows = false;
            dataGridHorario.EditMode = DataGridViewEditMode.EditProgrammatically;

            if (!dataGridCursos.Columns.Contains("Seleccionar"))
            {
                var colCheck = new DataGridViewCheckBoxColumn
                {
                    Name = "Seleccionar",
                    HeaderText = "✔",
                    Width = 50,
                    ReadOnly = false
                };
                dataGridCursos.Columns.Insert(0, colCheck);
            }

            if (!dataGridCursos.Columns.Contains("id_curso"))
            {
                var colHidden = new DataGridViewTextBoxColumn
                {
                    Name = "id_curso",
                    Visible = false
                };
                dataGridCursos.Columns.Add(colHidden);
            }

            dataGridCursos.CellValueChanged += dataGridCursos_CellValueChanged;
            dataGridCursos.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dataGridCursos.CurrentCell is DataGridViewCheckBoxCell &&
                    dataGridCursos.IsCurrentCellDirty)
                {
                    dataGridCursos.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            };
            dataGridHorario.Refresh();
        }

        private void dataGridCursos_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridCursos.CurrentRow?.Cells["ID"].Value is not string idCursoStr ||
                !int.TryParse(idCursoStr, out int idCurso))
                return;

            string sql = loader.BuildSql("horario_por_curso");
            var parameters = new[] { new NpgsqlParameter("@curso", idCurso) };
            DataTable horario = db.ExecuteQuery(sql, parameters);

            DrawIncrementalSchedule(horario);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (idEstudianteActual is null)
            {
                MessageBox.Show("Busque primero al estudiante.");
                return;
            }

            int contador = 0;
            foreach (DataGridViewRow fila in dataGridCursos.Rows)
            {
                bool seleccionado = fila.Cells["Seleccionar"].Value is true;
                if (!seleccionado) continue;

                if (!int.TryParse(fila.Cells["id_curso"].Value?.ToString(), out int idCurso))
                    continue;

                string sqlPeriodo = loader.BuildSql("periodo_de_curso");
                var paramPeriodo = new[] { new NpgsqlParameter("@id", idCurso) };
                DataTable resultado = db.ExecuteQuery(sqlPeriodo, paramPeriodo);

                string periodo = resultado.Rows.Count > 0
                    ? resultado.Rows[0]["periodo_academico"]?.ToString() ?? ""
                    : "";

                if (string.IsNullOrWhiteSpace(periodo))
                {
                    MessageBox.Show($"Curso {idCurso} no tiene período definido.");
                    continue;
                }

                string sql = loader.BuildSql("insertar_inscripcion");
                var parameters = new[]
                {
            new NpgsqlParameter("@persona", idEstudianteActual),
            new NpgsqlParameter("@curso", idCurso),
            new NpgsqlParameter("@fecha", DateTime.Today),
            new NpgsqlParameter("@periodo", periodo)
        };

                try
                {
                    db.ExecuteNonQuery(sql, parameters);
                    contador++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al inscribir curso {idCurso}: {ex.Message}");
                }
            }

            if (contador > 0)
            {
                MessageBox.Show($"Se inscribieron {contador} curso(s).");
                ShowCursosAvailable(idEstudianteActual.Value);
            }
            else
            {
                MessageBox.Show("No se inscribió ningún curso.");
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SearchBox_Click(object? sender, EventArgs e)
        {
            string cedula = SearchBox.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(cedula))
            {
                MessageBox.Show("Por favor ingrese una cédula.");
                return;
            }

            int? resultado = SearchIdByCedula(cedula);

            if (resultado is null)
            {
                MessageBox.Show("Esta sección es solo para inscripción de estudiantes.");
                return;
            }

            if (resultado == -1)
            {
                MessageBox.Show("Estudiante no encontrado.");
                return;
            }
                
            idEstudianteActual = resultado;
            ShowCursosAvailable(resultado.Value);
        }

        private void ShowCursosAvailable(int idPersona)
        {
            string sqlCursos = loader.BuildSql("cursos_disponibles_con_detalles");
            DataTable todos = db.ExecuteQuery(sqlCursos);

            string sqlYaInscrito = loader.BuildSql("horario_actual_del_estudiante");
            var parameters = new[] { new NpgsqlParameter("@persona", idPersona) };
            DataTable horarioActual = db.ExecuteQuery(sqlYaInscrito, parameters);

            var codigosYaInscritos = horarioActual.Rows.Cast<DataRow>()
                .Where(r => r["codigo_materia"] != DBNull.Value)
                .Select(r => r["codigo_materia"]!.ToString())
                .ToHashSet();

            var disponibles = todos.Rows.Cast<DataRow>()
                .Where(r => r["codigo_materia"] != DBNull.Value &&
                            !codigosYaInscritos.Contains(r["codigo_materia"]!.ToString()))
                .ToList();

            if (disponibles.Count > 0)
                ShowCursosInGrid(disponibles.CopyToDataTable());
            else
                dataGridCursos.Rows.Clear();
        }

        private void SearchBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchBox_Click(SearchBox, EventArgs.Empty);
                e.SuppressKeyPress = true;
            }
        }

        private void ShowCursosInGrid(DataTable cursos)
        {
            dataGridCursos.Rows.Clear();

            foreach (DataRow row in cursos.Rows)
            {
                int idx = dataGridCursos.Rows.Add();
                var r = dataGridCursos.Rows[idx];

                r.Cells["Seleccionar"].Value = false;
                r.Cells["ID"].Value = row["codigo_materia"]?.ToString() ?? "—";
                r.Cells["Materia"].Value = row["materia"]?.ToString() ?? "—";
                r.Cells["Seccion"].Value = row["seccion"]?.ToString() ?? "—";
                r.Cells["Trimestre"].Value = row["trimestre"]?.ToString() ?? "—";
                r.Cells["HC"].Value = row["hc"]?.ToString() ?? "—";
                r.Cells["Cupo"].Value = row["cupo"]?.ToString() ?? "—";
                r.Cells["Docente"].Value = row["docente"]?.ToString() ?? "—";
                r.Cells["id_curso"].Value = row["id_curso"];
            }
        }

        private int? SearchIdByCedula(string cedula)
        {
            string sql = loader.BuildSql("buscar_persona_por_cedula");
            var parameters = new[] { new NpgsqlParameter("@cedula", cedula) };
            DataTable dt = db.ExecuteQuery(sql, parameters);

            if (dt.Rows.Count == 0 || dt.Rows[0]["id"] == DBNull.Value)
                return -1;

            string tipo = dt.Rows[0]["id_tipo_persona"]?.ToString() ?? "";

            if (tipo != "1")
                return null;

            return Convert.ToInt32(dt.Rows[0]["id"]);
        }


        private void dataGridCursos_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (dataGridCursos.Columns[e.ColumnIndex].Name != "Seleccionar")
                return;

            var fila = dataGridCursos.Rows[e.RowIndex];
            bool seleccionado = fila.Cells["Seleccionar"].Value is true;

            if (fila.Cells["id_curso"].Value is not null &&
                int.TryParse(fila.Cells["id_curso"].Value.ToString(), out int idCurso))
            {
                string sql = loader.BuildSql("horario_por_curso");
                var parameters = new[] { new NpgsqlParameter("@curso", idCurso) };
                DataTable horario = db.ExecuteQuery(sql, parameters);

                if (horario.Rows.Count == 0)
                {
                    MessageBox.Show("Este curso no tiene horario registrado.");
                    return;
                }

                if (seleccionado)
                {
                    DrawIncrementalSchedule(horario);
                }
                else
                {
                    RemoveCursoSchedule(horario);
                }
            }
        }

        private void RemoveCursoSchedule(DataTable horario)
        {
            foreach (DataRow row in horario.Rows)
            {
                string dia = row["dia"]?.ToString() ?? "";
                string hi = row["hora_inicio"]?.ToString() ?? "—";
                string hf = row["hora_fin"]?.ToString() ?? "—";
                string aula = row["aula"]?.ToString() ?? "—";
                string materia = row["materia"]?.ToString() ?? "Materia";

                string bloque = $"{hi}-{hf} ({aula})\n{materia}";

                int colIndex = dataGridHorario.Columns[dia]?.Index ?? -1;
                if (colIndex == -1) continue;

                for (int i = 0; i < dataGridHorario.Rows.Count; i++)
                {
                    var cell = dataGridHorario.Rows[i].Cells[colIndex];
                    if (cell.Value?.ToString() == bloque)
                    {
                        cell.Value = ""; 
                        break;
                    }
                }
            }
        }

        private void DrawIncrementalSchedule(DataTable horario)
        {
            if (dataGridHorario.Columns.Count == 0)
            {
                string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };
                foreach (string dia in dias)
                    dataGridHorario.Columns.Add(dia, dia);
            }

            while (dataGridHorario.Rows.Count < 5)
                dataGridHorario.Rows.Add();

            foreach (DataRow row in horario.Rows)
            {
                string dia = row["dia"]?.ToString() ?? "";
                string hi = row["hora_inicio"]?.ToString() ?? "—";
                string hf = row["hora_fin"]?.ToString() ?? "—";
                string aula = row["aula"]?.ToString() ?? "—"; 
                string nombre = row["materia"]?.ToString() ?? "Materia";

                string bloque = $"{hi}-{hf} ({aula})\n{nombre}";

                int colIndex = dataGridHorario.Columns[dia]?.Index ?? -1;
                if (colIndex == -1) continue;

                for (int i = 0; i < dataGridHorario.Rows.Count; i++)
                {
                    var cell = dataGridHorario.Rows[i].Cells[colIndex];
                    if (string.IsNullOrEmpty(cell.Value?.ToString()))
                    {
                        cell.Value = bloque;
                        break;
                    }
                }
            }
        }

    }
}
