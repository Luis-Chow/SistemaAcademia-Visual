namespace SistemaAcademia
{
    partial class CrearMateriaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            comboBoxMateria = new ComboBox();
            label2 = new Label();
            textBox3 = new TextBox();
            label4 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            label5 = new Label();
            BtnGuardado = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.Font = new Font("Segoe UI", 60F);
            label1.ForeColor = Color.FromArgb(170, 245, 219);
            label1.Location = new Point(72, -263);
            label1.Name = "label1";
            label1.Size = new Size(258, 115);
            label1.TabIndex = 37;
            label1.Text = "Editar";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // comboBoxMateria
            // 
            comboBoxMateria.FormattingEnabled = true;
            comboBoxMateria.Location = new Point(36, 504);
            comboBoxMateria.Name = "comboBoxMateria";
            comboBoxMateria.Size = new Size(340, 23);
            comboBoxMateria.TabIndex = 45;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.Font = new Font("Segoe UI", 35F);
            label2.ForeColor = Color.FromArgb(170, 245, 219);
            label2.Location = new Point(28, 425);
            label2.Name = "label2";
            label2.Size = new Size(357, 59);
            label2.TabIndex = 43;
            label2.Text = "HC:";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(36, 253);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(340, 23);
            textBox3.TabIndex = 42;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.Font = new Font("Segoe UI", 35F);
            label4.ForeColor = Color.FromArgb(170, 245, 219);
            label4.Location = new Point(28, 171);
            label4.Name = "label4";
            label4.Size = new Size(357, 62);
            label4.TabIndex = 41;
            label4.Text = "ID:";
            label4.TextAlign = ContentAlignment.TopCenter;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(36, 382);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(340, 23);
            textBox1.TabIndex = 40;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.Font = new Font("Segoe UI", 35F);
            label3.ForeColor = Color.FromArgb(170, 245, 219);
            label3.Location = new Point(28, 296);
            label3.Name = "label3";
            label3.Size = new Size(357, 66);
            label3.TabIndex = 39;
            label3.Text = "Materia:";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.Font = new Font("Segoe UI", 60F);
            label5.ForeColor = Color.FromArgb(170, 245, 219);
            label5.Location = new Point(77, 9);
            label5.Name = "label5";
            label5.Size = new Size(258, 115);
            label5.TabIndex = 38;
            label5.Text = "Crear";
            label5.TextAlign = ContentAlignment.TopCenter;
            // 
            // BtnGuardado
            // 
            BtnGuardado.BackColor = Color.FromArgb(30, 30, 60);
            BtnGuardado.BackgroundImage = Properties.Resources.Boton_de_Guardado;
            BtnGuardado.BackgroundImageLayout = ImageLayout.Stretch;
            BtnGuardado.FlatAppearance.BorderSize = 0;
            BtnGuardado.FlatStyle = FlatStyle.Flat;
            BtnGuardado.Location = new Point(18, 623);
            BtnGuardado.Name = "BtnGuardado";
            BtnGuardado.Size = new Size(384, 94);
            BtnGuardado.TabIndex = 46;
            BtnGuardado.UseVisualStyleBackColor = false;
            // 
            // CrearMateriaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 60);
            ClientSize = new Size(414, 729);
            Controls.Add(BtnGuardado);
            Controls.Add(comboBoxMateria);
            Controls.Add(label2);
            Controls.Add(textBox3);
            Controls.Add(label4);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label5);
            Controls.Add(label1);
            Name = "CrearMateriaForm";
            Text = "CrearMateriaForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox comboBoxMateria;
        private Label label2;
        private TextBox textBox3;
        private Label label4;
        private TextBox textBox1;
        private Label label3;
        private Label label5;
        private Button BtnGuardado;
    }
}