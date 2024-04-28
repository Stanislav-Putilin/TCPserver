namespace ClientTCP
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label4 = new Label();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            label5 = new Label();
            label6 = new Label();
            textBox1 = new TextBox();
            button2 = new Button();
            comboBox1 = new ComboBox();
            label7 = new Label();
            dataGridView1 = new DataGridView();
            button1 = new Button();
            button3 = new Button();
            richTextBox1 = new RichTextBox();
            button4 = new Button();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 0;
            label1.Text = "User:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(51, 9);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 2;
            label4.Text = "label4";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(736, 22);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 372);
            label5.Name = "label5";
            label5.Size = new Size(129, 15);
            label5.TabIndex = 7;
            label5.Text = "Написать сообщение:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 38);
            label6.Name = "label6";
            label6.Size = new Size(125, 15);
            label6.TabIndex = 8;
            label6.Text = "История сообщений:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 388);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(372, 23);
            textBox1.TabIndex = 9;
            // 
            // button2
            // 
            button2.Location = new Point(390, 387);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 10;
            button2.Text = "Send";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(552, 69);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(97, 23);
            comboBox1.TabIndex = 11;
            comboBox1.SelectedValueChanged += comboBox1_SelectedValueChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(552, 51);
            label7.Name = "label7";
            label7.Size = new Size(142, 15);
            label7.TabIndex = 12;
            label7.Text = "Выберите пользователя:";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 69);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(453, 300);
            dataGridView1.TabIndex = 13;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // button1
            // 
            button1.Location = new Point(471, 387);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 14;
            button1.Text = "File";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            button3.Location = new Point(471, 69);
            button3.Name = "button3";
            button3.Size = new Size(75, 300);
            button3.TabIndex = 15;
            button3.Text = "Save file";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(552, 98);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(172, 312);
            richTextBox1.TabIndex = 16;
            richTextBox1.Text = "";
            // 
            // button4
            // 
            button4.Location = new Point(649, 68);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 17;
            button4.Text = "Обновить";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(736, 450);
            Controls.Add(button4);
            Controls.Add(richTextBox1);
            Controls.Add(button3);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(label7);
            Controls.Add(comboBox1);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(statusStrip1);
            Controls.Add(label4);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label4;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Label label5;
        private Label label6;
        private TextBox textBox1;
        private Button button2;
        private ComboBox comboBox1;
        private Label label7;
        private DataGridView dataGridView1;
        private Button button1;
        private Button button3;
        private RichTextBox richTextBox1;
        private Button button4;
    }
}