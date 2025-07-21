namespace SistemaAcademia
{
    partial class EditarMateriaForm
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
            BtnGuardado = new Button();
            comboBoxMateria = new ComboBox();
            label2 = new Label();
            textBox3 = new TextBox();
            label4 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.Font = new Font("Segoe UI", 60F);
            label1.ForeColor = Color.FromArgb(170, 245, 219);
            label1.Location = new Point(75, 4);
            label1.Name = "label1";
            label1.Size = new Size(258, 115);
            label1.TabIndex = 29;
            label1.Text = "Editar";
            label1.TextAlign = ContentAlignment.TopCenter;
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
            BtnGuardado.TabIndex = 53;
            BtnGuardado.UseVisualStyleBackColor = false;
            // 
            // comboBoxMateria
            // 
            comboBoxMateria.FormattingEnabled = true;
            comboBoxMateria.Location = new Point(34, 485);
            comboBoxMateria.Name = "comboBoxMateria";
            comboBoxMateria.Size = new Size(340, 23);
            comboBoxMateria.TabIndex = 52;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.Font = new Font("Segoe UI", 35F);
            label2.ForeColor = Color.FromArgb(170, 245, 219);
            label2.Location = new Point(26, 406);
            label2.Name = "label2";
            label2.Size = new Size(357, 59);
            label2.TabIndex = 51;
            label2.Text = "HC:";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(34, 234);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(340, 23);
            textBox3.TabIndex = 50;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.Font = new Font("Segoe UI", 35F);
            label4.ForeColor = Color.FromArgb(170, 245, 219);
            label4.Location = new Point(26, 152);
            label4.Name = "label4";
            label4.Size = new Size(357, 62);
            label4.TabIndex = 49;
            label4.Text = "ID:";
            label4.TextAlign = ContentAlignment.TopCenter;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(34, 363);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(340, 23);
            textBox1.TabIndex = 48;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.Font = new Font("Segoe UI", 35F);
            label3.ForeColor = Color.FromArgb(170, 245, 219);
            label3.Location = new Point(26, 277);
            label3.Name = "label3";
            label3.Size = new Size(357, 66);
            label3.TabIndex = 47;
            label3.Text = "Materia:";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // EditarMateriaForm
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
            Controls.Add(label1);
            Name = "EditarMateriaForm";
            Text = "EditarMateriaForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Button BtnGuardado;
        private ComboBox comboBoxMateria;
        private Label label2;
        private TextBox textBox3;
        private Label label4;
        private TextBox textBox1;
        private Label label3;
    }
}