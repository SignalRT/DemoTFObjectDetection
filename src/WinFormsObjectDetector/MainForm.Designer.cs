namespace WinFormsObjectDetector
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.videoImage = new System.Windows.Forms.PictureBox();
            this.panButton = new System.Windows.Forms.Panel();
            this.chbFromFile = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbFront = new System.Windows.Forms.RadioButton();
            this.rdbBack = new System.Windows.Forms.RadioButton();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.cbxModel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.videoImage)).BeginInit();
            this.panButton.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // videoImage
            // 
            this.videoImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoImage.Location = new System.Drawing.Point(0, 0);
            this.videoImage.Name = "videoImage";
            this.videoImage.Size = new System.Drawing.Size(1309, 909);
            this.videoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.videoImage.TabIndex = 0;
            this.videoImage.TabStop = false;
            // 
            // panButton
            // 
            this.panButton.Controls.Add(this.label1);
            this.panButton.Controls.Add(this.cbxModel);
            this.panButton.Controls.Add(this.chbFromFile);
            this.panButton.Controls.Add(this.groupBox1);
            this.panButton.Controls.Add(this.lblTime);
            this.panButton.Controls.Add(this.btnStop);
            this.panButton.Controls.Add(this.btnStart);
            this.panButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.panButton.Location = new System.Drawing.Point(0, 0);
            this.panButton.Name = "panButton";
            this.panButton.Size = new System.Drawing.Size(1309, 199);
            this.panButton.TabIndex = 1;
            // 
            // chbFromFile
            // 
            this.chbFromFile.AutoSize = true;
            this.chbFromFile.Location = new System.Drawing.Point(752, 34);
            this.chbFromFile.Name = "chbFromFile";
            this.chbFromFile.Size = new System.Drawing.Size(134, 29);
            this.chbFromFile.TabIndex = 4;
            this.chbFromFile.Text = "From File";
            this.chbFromFile.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbFront);
            this.groupBox1.Controls.Add(this.rdbBack);
            this.groupBox1.Location = new System.Drawing.Point(388, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(334, 67);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera";
            // 
            // rdbFront
            // 
            this.rdbFront.AutoSize = true;
            this.rdbFront.Location = new System.Drawing.Point(200, 21);
            this.rdbFront.Name = "rdbFront";
            this.rdbFront.Size = new System.Drawing.Size(93, 29);
            this.rdbFront.TabIndex = 1;
            this.rdbFront.Text = "Front";
            this.rdbFront.UseVisualStyleBackColor = true;
            // 
            // rdbBack
            // 
            this.rdbBack.AutoSize = true;
            this.rdbBack.Checked = true;
            this.rdbBack.Location = new System.Drawing.Point(103, 21);
            this.rdbBack.Name = "rdbBack";
            this.rdbBack.Size = new System.Drawing.Size(91, 29);
            this.rdbBack.TabIndex = 0;
            this.rdbBack.TabStop = true;
            this.rdbBack.Text = "Back";
            this.rdbBack.UseVisualStyleBackColor = true;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(930, 34);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(83, 25);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "Time....";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(207, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(155, 68);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop...";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(22, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(159, 68);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start...";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "mp4";
            this.openFileDialog.FileName = "Open Video";
            // 
            // cbxModel
            // 
            this.cbxModel.FormattingEnabled = true;
            this.cbxModel.Location = new System.Drawing.Point(388, 121);
            this.cbxModel.Name = "cbxModel";
            this.cbxModel.Size = new System.Drawing.Size(625, 33);
            this.cbxModel.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Model:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1309, 909);
            this.Controls.Add(this.panButton);
            this.Controls.Add(this.videoImage);
            this.Name = "MainForm";
            this.Text = "SignalRT Object Detector";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.videoImage)).EndInit();
            this.panButton.ResumeLayout(false);
            this.panButton.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox videoImage;
        private System.Windows.Forms.Panel panButton;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbBack;
        private System.Windows.Forms.RadioButton rdbFront;
        private System.Windows.Forms.CheckBox chbFromFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxModel;
    }
}

