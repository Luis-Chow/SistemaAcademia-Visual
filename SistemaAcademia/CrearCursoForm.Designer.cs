namespace SistemaAcademia
{
    partial class CrearCursoForm
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
            label2 = new Label();
            textBox3 = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label5 = new Label();
            comboBox2 = new ComboBox();
            textBox1 = new TextBox();
            label6 = new Label();
            textBox2 = new TextBox();
            textBox4 = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.Font = new Font("Segoe UI", 35F);
            label1.ForeColor = Color.FromArgb(170, 245, 219);
            label1.Location = new Point(24, 514);
            label1.Name = "label1";
            label1.Size = new Size(369, 60);
            label1.TabIndex = 64;
            label1.Text = "Aula:";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.Font = new Font("Segoe UI", 35F);
            label2.ForeColor = Color.FromArgb(170, 245, 219);
            label2.Location = new Point(30, 406);
            label2.Name = "label2";
            label2.Size = new Size(357, 65);
            label2.TabIndex = 61;
            label2.Text = "Horario:";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(38, 161);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(340, 23);
            textBox3.TabIndex = 60;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.Font = new Font("Segoe UI", 35F);
            label4.ForeColor = Color.FromArgb(170, 245, 219);
            label4.Location = new Point(7, 89);
            label4.Name = "label4";
            label4.Size = new Size(402, 62);
            label4.TabIndex = 59;
            label4.Text = "ID de Materia:";
            label4.TextAlign = ContentAlignment.TopCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.Font = new Font("Segoe UI", 35F);
            label3.ForeColor = Color.FromArgb(170, 245, 219);
            label3.Location = new Point(30, 194);
            label3.Name = "label3";
            label3.Size = new Size(357, 64);
            label3.TabIndex = 57;
            label3.Text = "Seccion:";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.Font = new Font("Segoe UI", 60F);
            label5.ForeColor = Color.FromArgb(170, 245, 219);
            label5.Location = new Point(79, -19);
            label5.Name = "label5";
            label5.Size = new Size(258, 115);
            label5.TabIndex = 56;
            label5.Text = "Crear";
            label5.TextAlign = ContentAlignment.TopCenter;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(38, 268);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(340, 23);
            comboBox2.TabIndex = 66;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(38, 373);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(340, 23);
            textBox1.TabIndex = 68;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.Font = new Font("Segoe UI", 35F);
            label6.ForeColor = Color.FromArgb(170, 245, 219);
            label6.Location = new Point(30, 301);
            label6.Name = "label6";
            label6.Size = new Size(357, 62);
            label6.TabIndex = 67;
            label6.Text = "Periodo:";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(38, 481);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(340, 23);
            textBox2.TabIndex = 69;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(38, 584);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(340, 23);
            textBox4.TabIndex = 70;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(30, 30, 60);
            button1.BackgroundImage = Properties.Resources.Boton_de_Guardado;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(18, 623);
            button1.Name = "button1";
            button1.Size = new Size(384, 94);
            button1.TabIndex = 71;
            button1.UseVisualStyleBackColor = false;
            // 
            // CrearCursoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 60);
            ClientSize = new Size(414, 729);
            Controls.Add(button1);
            Controls.Add(textBox4);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label6);
            Controls.Add(comboBox2);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(textBox3);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label5);
            Name = "CrearCursoForm";
            Text = "CrearCursoForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private TextBox textBox3;
        private Label label4;
        private Label label3;
        private Label label5;
        private ComboBox comboBox2;
        private TextBox textBox1;
        private Label label6;
        private TextBox textBox2;
        private TextBox textBox4;
        private Button button1;
    }
}