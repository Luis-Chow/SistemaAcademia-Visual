using System.Windows.Forms.VisualStyles;
using Newtonsoft.Json;
using Npgsql;
using SistemaAcademia;
using System.Text;

namespace SistemaAcademia
{
    public partial class MenuPrincipal : Form
    {
        private DBComponent db = null!;
        private JsonLoader loader = null!;

        public MenuPrincipal()
        {
            InitializeComponent();

            var contexto = GetJson();
            if (contexto is null)
            {
                MessageBox.Show("No se pudo obtener los archivos json.");
                Close();
                return;
            }

            db = contexto.Value.db;
            loader = contexto.Value.loader;
        }


        private (DBComponent db, JsonLoader loader)? GetJson()
        {
            string rutaConfig = "sql/Rutas.json";
            if (!File.Exists(rutaConfig))
            {
                MessageBox.Show($"No se encontró el archivo de configuración de rutas: {rutaConfig}");
                return null;
            }

            Dictionary<string, string>? rutas;
            try
            {
                string json = File.ReadAllText(rutaConfig, Encoding.UTF8);
                rutas = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al interpretar Rutas.json: {ex.Message}");
                return null;
            }

            if (rutas is null ||
                !rutas.TryGetValue("db", out var rutaDB) ||
                !rutas.TryGetValue("Queries_Joins", out var rutaQueries))
            {
                MessageBox.Show("Las claves 'db' o 'Queries_Joins' faltan en Rutas.json.");
                return null;
            }

            try
            {
                var db = DBComponent.FromSettings(rutaDB);
                var loader = new JsonLoader(rutaQueries);
                return (db, loader);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar componentes desacoplados: {ex.Message}");
                return null;
            }
        }
        private void BtnNotas_Click(object sender, EventArgs e)
        {
            this.Hide();
            using var notasForm = new NotasForm(db, loader);
            notasForm.ShowDialog();
            this.Show();
        }

        private void BtnReportes_Click(object sender, EventArgs e)
        {
            this.Hide();
            using var reportesForm = new ReportesForm(db, loader);
            reportesForm.ShowDialog();
            this.Show();
        }

        private void BtnInscripcion_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (InscripcionesForm inscripcionesForm = new InscripcionesForm(db, loader))
            {
                inscripcionesForm.ShowDialog();
            }
            this.Show();
        }

        private void BtnMantenimiento_Click(object sender, EventArgs e)
        {
            this.Hide();
            var mantenimiento = new MantenimientoForm(db, loader);
            mantenimiento.ShowDialog();
            this.Show();
        }
    }
}
