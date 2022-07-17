namespace ModulationTest
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chartVisual1 = new AI.Charts.Control.ChartVisual();
            this.chartVisual2 = new AI.Charts.Control.ChartVisual();
            this.chartVisual3 = new AI.Charts.Control.ChartVisual();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите строку";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(5, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(248, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "ASK Модуляция";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(259, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Модулирующий сигнал";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chartVisual1
            // 
            this.chartVisual1.AutoScroll = true;
            this.chartVisual1.BackColor = System.Drawing.Color.White;
            this.chartVisual1.ChartName = "График";
            this.chartVisual1.ForeColor = System.Drawing.Color.Black;
            this.chartVisual1.IsContextMenu = true;
            this.chartVisual1.IsLogScale = false;
            this.chartVisual1.IsMoove = true;
            this.chartVisual1.IsScale = true;
            this.chartVisual1.IsShowXY = true;
            this.chartVisual1.LabelX = "Ось Х";
            this.chartVisual1.LabelY = "Ось Y";
            this.chartVisual1.Location = new System.Drawing.Point(5, 46);
            this.chartVisual1.Name = "chartVisual1";
            this.chartVisual1.Size = new System.Drawing.Size(968, 261);
            this.chartVisual1.TabIndex = 3;
            // 
            // chartVisual2
            // 
            this.chartVisual2.AutoScroll = true;
            this.chartVisual2.BackColor = System.Drawing.Color.White;
            this.chartVisual2.ChartName = "График QI";
            this.chartVisual2.ForeColor = System.Drawing.Color.Black;
            this.chartVisual2.IsContextMenu = true;
            this.chartVisual2.IsLogScale = false;
            this.chartVisual2.IsMoove = true;
            this.chartVisual2.IsScale = true;
            this.chartVisual2.IsShowXY = true;
            this.chartVisual2.LabelX = "I";
            this.chartVisual2.LabelY = "Q";
            this.chartVisual2.Location = new System.Drawing.Point(5, 313);
            this.chartVisual2.Name = "chartVisual2";
            this.chartVisual2.Size = new System.Drawing.Size(443, 302);
            this.chartVisual2.TabIndex = 4;
            // 
            // chartVisual3
            // 
            this.chartVisual3.AutoScroll = true;
            this.chartVisual3.BackColor = System.Drawing.Color.White;
            this.chartVisual3.ChartName = "График";
            this.chartVisual3.ForeColor = System.Drawing.Color.Black;
            this.chartVisual3.IsContextMenu = true;
            this.chartVisual3.IsLogScale = false;
            this.chartVisual3.IsMoove = true;
            this.chartVisual3.IsScale = true;
            this.chartVisual3.IsShowXY = true;
            this.chartVisual3.LabelX = "Ось Х";
            this.chartVisual3.LabelY = "Ось Y";
            this.chartVisual3.Location = new System.Drawing.Point(5, 621);
            this.chartVisual3.Name = "chartVisual3";
            this.chartVisual3.Size = new System.Drawing.Size(968, 302);
            this.chartVisual3.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 933);
            this.Controls.Add(this.chartVisual3);
            this.Controls.Add(this.chartVisual2);
            this.Controls.Add(this.chartVisual1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private AI.Charts.Control.ChartVisual chartVisual1;
        private AI.Charts.Control.ChartVisual chartVisual2;
        private AI.Charts.Control.ChartVisual chartVisual3;
    }
}

