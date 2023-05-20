using System;
using System.Windows.Forms;

namespace MechTE.Form
{
    /// <summary>
    /// 窗体api
    /// </summary>
    public class TForm
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
            if (result == DialogResult.Yes)
            {
                try
                {
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return false;
        }



    }
}
