using System;
using System.Windows.Forms;

namespace MechTE_480.Form
{
    public partial class UpFile : System.Windows.Forms.Form
    {
        public UpFile()
        {
            InitializeComponent();
        }

        private void UpFile_Load(object sender,EventArgs e)
        {
            AllowDrop = true;
            TopMost = true;
        }

        private void UpFile_DragEnter(object sender,DragEventArgs e)
        {

            //判断是否有文件拖入
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                //设置拖入放置效果
                e.Effect = DragDropEffects.Link;
                // 获取多个文件拖入的文件路径
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                for (int i = 0 ; i < files.Length ; i++) {
                    //MessageBox.Show(files[i]);
                }
            } else {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 处理拖入的数据及操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpFile_DragDrop(object sender,DragEventArgs e)
        {
            //文件路径
            string path = ( (Array)e.Data.GetData(DataFormats.FileDrop) ).GetValue(0).ToString();
            // 处理文件或文件夹path...
            listBox1.Items.Add(path);
        }

        /// <summary>
        /// 退出窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpFile_FormClosing(object sender,FormClosingEventArgs e)
        {
            e.Cancel = true;
            Environment.Exit(0);
        }
    }
}
