using System;
using System.Windows.Forms;

namespace MerryUtil.Forms
{
    internal partial class ProgressBars : Form
    {
        public ProgressBars(string name)
        {
            InitializeComponent();
            Text = name;
            label1.Text = name;
           
        }
        private void ProgressBars_Load(object sender, EventArgs e)
        {
            i = 0;
            timer1.Interval = 500;
            timer1.Enabled = true;
            //置顶
            TopMost = true;
        }

        int i = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            i = i + 5;
            progressBar1.Value = i;
            //如果執行時間超過，則this.DialogResult = DialogResult.No;
            if (i >= 100)
            {
                DialogResult = DialogResult.No;
                timer1.Enabled = false;
                Close();
            }
        }
    }
}
