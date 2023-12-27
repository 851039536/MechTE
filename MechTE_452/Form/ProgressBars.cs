using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechTE_452.Form
{
    /// <summary>
    /// 进度条
    /// </summary>
    public partial class ProgressBars : System.Windows.Forms.Form
    {
        /// <summary>
        /// 标识
        /// </summary>
        public bool Ide = false;
        private readonly int _time;


        /// <summary>
        /// 进度条初始化
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="time">时间毫秒</param>
        public ProgressBars(string name,int time)
        {
            InitializeComponent();
            Text = name;
            _time = time;

            this.FormBorderStyle = FormBorderStyle.None;
            // 设置窗体为置顶
            this.TopMost = true;
        }
        int i = 0;
        /// <summary>
        /// 程序加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressBars_Load(object sender,EventArgs e)
        {
            //i = 0;
            ////设置间隔多少毫秒执行
            //times.Interval = _time;
            //times.Enabled = true;

        }


        /// <summary>
        /// 执行窗体操作
        /// </summary>
        /// <param name="action">方法</param>
        public void ExecuteTest(Action action)
        {
            Control.CheckForIllegalCrossThreadCalls = false;//关闭跨线程访问检测
            Task.Run(() =>
            {
                i = 0;
                //设置间隔多少毫秒执行
                times.Interval = _time;
                times.Enabled = true;
                ShowDialog();
            });
            action();
            //关闭串口
            if (Ide)
            {
                times.Enabled = false;
                progressBarForm.Value = 90;
                Thread.Sleep(200);
                progressBarForm.Value = 100;
                Thread.Sleep(200);
                DialogResult = DialogResult.No;
                times.Enabled = false;
                Close();
            }
        }

        private void timer1_Tick(object sender,EventArgs e)
        {
            i = i + 1;
            progressBarForm.Value = i;
            if (i < 100) return;
            DialogResult = DialogResult.No;
            times.Enabled = false;
            Close();

        }

        private void progressBar1_Click(object sender,EventArgs e)
        {

        }
    }
}
