using System;
using System.Drawing;
using System.Windows.Forms;

namespace MechTE_480.MECH
{
    /// <summary>
    /// 窗体api
    /// </summary>
    public class MechForm
    {
        /// <summary>
        /// 弹框提示
        /// </summary>
        /// <param name="name">描述</param>
        /// <param name="title">标题</param>
        /// <returns>bool</returns>
        public static bool MesBox(string name, string title)
        {
            var result = MessageBox.Show(@name, @title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return false;
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 错误提示,
        /// 消息框包含一个符号，该符号包含一个红色背景圆圈，圆圈中为白色 X 符号
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="prompt">描述</param>
        public static void ShowErr(string title, string prompt)
        {
            MessageBox.Show(prompt, title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        /// <summary>
        /// 弹窗接收输入参数
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="prompt">描述</param>
        /// <returns></returns>
        public static string ShowInputDialog(string title, string prompt)
        {
            var inputBox = new System.Windows.Forms.Form();
            var label = new Label();
            var textBox = new TextBox();
            var buttonOk = new Button();
            var buttonCancel = new Button();

            // 设置窗体标题和大小
            inputBox.Text = title;
            inputBox.ClientSize = new Size(400, 135);
            
            // 设置标题的文本和样式
            label.Text = prompt;
            label.AutoSize = true;
            
            label.Font = new Font("Arial", 12, FontStyle.Bold);
            label.ForeColor = Color.Blue;

            //// 设置确定按钮的样式
            buttonOk.Text = @"确定";
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            
            buttonOk.Font = new Font("Arial", 12, FontStyle.Bold);
            buttonOk.BackColor = Color.LightSkyBlue;
            buttonOk.ForeColor = Color.White;
            buttonOk.Padding = new Padding(0, 1, 0, 1);

            
            // 设置取消按钮的样式
            buttonCancel.Text = @"取消";
            buttonCancel.DialogResult = DialogResult.Cancel;
            
            buttonCancel.Font = new Font("Arial", 12, FontStyle.Bold);
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Padding = new Padding(0, 1, 0, 1);
            
            // 设置文本框的样式
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            textBox.Font = new Font("Arial", 12);
            textBox.BackColor = Color.LightGray;
 
            // 设置控件的位置和大小
            textBox.SetBounds(12, 50, 372, 35);
            buttonCancel.SetBounds(309, 100, 75, 30);
            buttonOk.SetBounds(228, 100, 75, 30);
            label.SetBounds(10, 18, 372, 12);
         
            //// 将控件添加到窗体上
            inputBox.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.StartPosition = FormStartPosition.CenterScreen;
            inputBox.MinimizeBox = false;
            inputBox.MaximizeBox = false;
            inputBox.AcceptButton = buttonOk;
            inputBox.CancelButton = buttonCancel;

            var result = inputBox.ShowDialog();
            if (result == DialogResult.OK)
            {
                return textBox.Text;
            }
            return null;
        }
        
        
        


    }
}
