namespace MechTE_452.Form
{
    partial class ProgressBars
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressBars));
            progressBarForm = new System.Windows.Forms.ProgressBar();
            times = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // progressBarForm
            // 
            progressBarForm.ForeColor = System.Drawing.Color.DeepSkyBlue;
            progressBarForm.Location = new System.Drawing.Point(0,-1);
            progressBarForm.Name = "progressBarForm";
            progressBarForm.Size = new System.Drawing.Size(588,44);
            progressBarForm.Step = 1;
            progressBarForm.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            progressBarForm.TabIndex = 2;
            progressBarForm.Click += progressBar1_Click;
            // 
            // times
            // 
            times.Interval = 1000;
            times.Tick += timer1_Tick;
            // 
            // ProgressBars
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F,12F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(586,36);
            Controls.Add(progressBarForm);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(2);
            Name = "ProgressBars";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "ProgressBars";
            TopMost = true;
            Load += ProgressBars_Load;
            ResumeLayout(false);
        }

        #endregion

        public System.Windows.Forms.ProgressBar progressBarForm;
        public System.Windows.Forms.Timer times;
    }
}