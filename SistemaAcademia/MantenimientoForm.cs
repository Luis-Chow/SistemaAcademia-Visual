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
    public partial class MantenimientoForm : Form
    {
        private DBComponent db = null!;
        private JsonLoader loader = null!;
        public MantenimientoForm(DBComponent dbComponent, JsonLoader jsonLoader)
        {
            InitializeComponent();
            db = dbComponent ?? throw new ArgumentNullException(nameof(dbComponent));
            loader = jsonLoader ?? throw new ArgumentNullException(nameof(jsonLoader));
        }


        private void BtnRetroceso_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnPersona_Click(object sender, EventArgs e)
        {
            this.Hide();
            var mantenimientoPersonaForm = new MantenimientoPersonaForm(db, loader);
            mantenimientoPersonaForm.ShowDialog();
            this.Show();
        }

        private void BtnMateria_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (MantenimientoMateriaForm materiaForm = new MantenimientoMateriaForm())
            {
                materiaForm.ShowDialog();
            }
            this.Show();
        }

        private void BtnSeccion_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (MantenimientoSeccionForm seccionForm = new MantenimientoSeccionForm())
            {
                seccionForm.ShowDialog();
            }
            this.Show();
        }

        private void BtnCurso_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (MantenimientoCursoForm cursoForm = new MantenimientoCursoForm())
            {
                cursoForm.ShowDialog();
            }
            this.Show();
        }
    }
}

