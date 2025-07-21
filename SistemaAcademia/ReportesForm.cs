using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaAcademia
{
    public partial class ReportesForm : Form
    {
        private readonly DBComponent db;
        private readonly JsonLoader loader;

        public ReportesForm(DBComponent dbComponent, JsonLoader jsonLoader)
        {
            InitializeComponent();
            db = dbComponent;
            loader = jsonLoader;
            BtnRetroceso.Click += BtnRetroceso_Click;
        }

        private void BtnPlanillaNotas_Click(object? sender, EventArgs e)
        {
            this.Hide();
            using (var planillaNotasForm = new PlanillaNotasForm(db, loader))
            {
                planillaNotasForm.ShowDialog();
            }
            this.Show();
        }

        private void BtnPlanillaEvaluaciones_Click(object? sender, EventArgs e)
        {
            this.Hide();
            using (var planillaEvaluacionesForm = new PlanillaEvaluacionesForm(db, loader))
            {
                planillaEvaluacionesForm.ShowDialog();
            }
            this.Show();
        }

        private void BtnRetroceso_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
