namespace CNC_Drawer_UI
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.btn_browse = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numUpDownFilter = new System.Windows.Forms.NumericUpDown();
            this.btn_exit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cBCom = new System.Windows.Forms.ComboBox();
            this.timerCOMPortUpdate = new System.Windows.Forms.Timer(this.components);
            this.numUpDownZoom = new System.Windows.Forms.NumericUpDown();
            this.btn_help = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_time = new System.Windows.Forms.Label();
            this.lbl_h = new System.Windows.Forms.Label();
            this.lbl_v = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownZoom)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_browse
            // 
            this.btn_browse.Location = new System.Drawing.Point(865, 50);
            this.btn_browse.Margin = new System.Windows.Forms.Padding(4);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(212, 71);
            this.btn_browse.TabIndex = 0;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(840, 718);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(865, 302);
            this.btn_start.Margin = new System.Windows.Forms.Padding(4);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(212, 71);
            this.btn_start.TabIndex = 2;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(864, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Zoom:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(864, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Filter:";
            // 
            // numUpDownFilter
            // 
            this.numUpDownFilter.Location = new System.Drawing.Point(938, 164);
            this.numUpDownFilter.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.numUpDownFilter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownFilter.Name = "numUpDownFilter";
            this.numUpDownFilter.Size = new System.Drawing.Size(139, 30);
            this.numUpDownFilter.TabIndex = 6;
            this.numUpDownFilter.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numUpDownFilter.ValueChanged += new System.EventHandler(this.numUpDownFilter_ValueChanged);
            this.numUpDownFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numUpDownFilter_KeyDown);
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(865, 460);
            this.btn_exit.Margin = new System.Windows.Forms.Padding(4);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(212, 71);
            this.btn_exit.TabIndex = 7;
            this.btn_exit.Text = "Exit";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(864, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "COM:";
            // 
            // cBCom
            // 
            this.cBCom.FormattingEnabled = true;
            this.cBCom.Location = new System.Drawing.Point(938, 11);
            this.cBCom.Name = "cBCom";
            this.cBCom.Size = new System.Drawing.Size(140, 33);
            this.cBCom.TabIndex = 9;
            // 
            // timerCOMPortUpdate
            // 
            this.timerCOMPortUpdate.Interval = 1000;
            this.timerCOMPortUpdate.Tick += new System.EventHandler(this.timerCOMPortUpdate_Tick);
            // 
            // numUpDownZoom
            // 
            this.numUpDownZoom.Location = new System.Drawing.Point(938, 129);
            this.numUpDownZoom.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.numUpDownZoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownZoom.Name = "numUpDownZoom";
            this.numUpDownZoom.Size = new System.Drawing.Size(139, 30);
            this.numUpDownZoom.TabIndex = 10;
            this.numUpDownZoom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownZoom.ValueChanged += new System.EventHandler(this.numUpDownZoom_ValueChanged);
            this.numUpDownZoom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numUpDownZoom_KeyDown);
            // 
            // btn_help
            // 
            this.btn_help.Location = new System.Drawing.Point(865, 381);
            this.btn_help.Margin = new System.Windows.Forms.Padding(4);
            this.btn_help.Name = "btn_help";
            this.btn_help.Size = new System.Drawing.Size(212, 71);
            this.btn_help.TabIndex = 11;
            this.btn_help.Text = "Help";
            this.btn_help.UseVisualStyleBackColor = true;
            this.btn_help.Click += new System.EventHandler(this.btn_help_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(846, 724);
            this.panel1.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(864, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 25);
            this.label4.TabIndex = 13;
            this.label4.Text = "Time:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(864, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "Horizontal:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(864, 273);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 25);
            this.label6.TabIndex = 15;
            this.label6.Text = "Vertical:";
            // 
            // lbl_time
            // 
            this.lbl_time.AutoSize = true;
            this.lbl_time.Location = new System.Drawing.Point(965, 204);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(57, 25);
            this.lbl_time.TabIndex = 16;
            this.lbl_time.Text = "0:0:0";
            // 
            // lbl_h
            // 
            this.lbl_h.AutoSize = true;
            this.lbl_h.Location = new System.Drawing.Point(965, 239);
            this.lbl_h.Name = "lbl_h";
            this.lbl_h.Size = new System.Drawing.Size(23, 25);
            this.lbl_h.TabIndex = 17;
            this.lbl_h.Text = "0";
            // 
            // lbl_v
            // 
            this.lbl_v.AutoSize = true;
            this.lbl_v.Location = new System.Drawing.Point(965, 273);
            this.lbl_v.Name = "lbl_v";
            this.lbl_v.Size = new System.Drawing.Size(23, 25);
            this.lbl_v.TabIndex = 18;
            this.lbl_v.Text = "0";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 755);
            this.Controls.Add(this.lbl_v);
            this.Controls.Add(this.lbl_h);
            this.Controls.Add(this.lbl_time);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_help);
            this.Controls.Add(this.numUpDownZoom);
            this.Controls.Add(this.cBCom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.numUpDownFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btn_browse);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CNC Drawer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownZoom)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numUpDownFilter;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cBCom;
        private System.Windows.Forms.Timer timerCOMPortUpdate;
        private System.Windows.Forms.NumericUpDown numUpDownZoom;
        private System.Windows.Forms.Button btn_help;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_time;
        private System.Windows.Forms.Label lbl_h;
        private System.Windows.Forms.Label lbl_v;
    }
}

