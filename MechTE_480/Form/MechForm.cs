﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace MechTE_480.Form
{
    /// <summary>
    /// 窗体api
    /// </summary>
    public partial class MechForm
    {
        
        #region 控件大小随窗体大小等比例缩放
        /// <summary>
        /// 定义当前窗体的宽度
        /// </summary>
        public static float X;
        /// <summary>
        /// 定义当前窗体的高度
        /// </summary>
        public static float Y;
        /// <summary>
        /// 控件大小随窗体大小等比例缩放,
        /// 在窗体重载中使用 >>  MechForm.X = this.Width; MechForm.Y = this.Height;  MechForm.SetTag(this);
        /// </summary>
        /// <param name="cons"></param>
        public static void SetTag(Control cons)
        {
            foreach (Control con in cons.Controls) {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0) {
                    SetTag(con);
                }
            }
        }
        /// <summary>
        /// 设置缩放,在Resize事件中使用 >>
        /// float newX = this.Width / MechForm.X;
        /// float newY = this.Height / MechForm.Y;
        /// MechForm.SetControls(newX,newY,this);
        /// </summary>
        /// <param name="newx">X轴</param>
        /// <param name="newy">Y轴</param>
        /// <param name="cons"></param>
        public static void SetControls(float newx,float newy,Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls) {
                //获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null) {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    //根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * newx);//宽度
                    con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * newy);//高度
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * newx);//左边距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * newy);//顶边距
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                    con.Font = new Font(con.Font.Name,currentSize,con.Font.Style,con.Font.Unit);
                    if (con.Controls.Count > 0) {
                        SetControls(newx,newy,con);
                    }
                }
            }
        }
        #endregion
        
        /// <summary>
        /// 默认弹框提示(确认/取消)
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
        /// 错误提示(确认)
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="prompt">描述</param>
        public static void ShowErr(string title, string prompt)
        {
            // 显示错误信息
            MessageBox.Show(prompt, title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        /// <summary>
        /// 弹窗接收参数(确认/取消)..
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
