namespace ServerTCP
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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            richTextBox1 = new RichTextBox();
            button2 = new Button();
            label6 = new Label();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 2;
            label1.Text = "Server IP:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 35);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 3;
            label2.Text = "Server Port:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(83, 35);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 5;
            label3.Text = "label3";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(83, 9);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 4;
            label4.Text = "label4";
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = SystemColors.ButtonHighlight;
            richTextBox1.Location = new Point(12, 97);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(464, 403);
            richTextBox1.TabIndex = 6;
            richTextBox1.Text = "";
            // 
            // button2
            // 
            button2.Location = new Point(163, 12);
            button2.Name = "button2";
            button2.Size = new Size(313, 38);
            button2.TabIndex = 7;
            button2.Text = "Start Server";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 79);
            label6.Name = "label6";
            label6.Size = new Size(95, 15);
            label6.TabIndex = 9;
            label6.Text = "Message history:";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 503);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(487, 22);
            statusStrip1.TabIndex = 11;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(487, 525);
            Controls.Add(statusStrip1);
            Controls.Add(label6);
            Controls.Add(button2);
            Controls.Add(richTextBox1);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private RichTextBox richTextBox1;
        private Button button2;
        private Label label6;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
    }
}