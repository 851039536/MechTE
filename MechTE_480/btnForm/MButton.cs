using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MechTE_480.Hid;

namespace MechTE_480.btnForm
{
    /// <summary>
    /// 弹窗按键测试类
    /// </summary>
    public class MButton
    {
        /// <summary>
        /// 按键测试
        /// </summary>
        /// <param name="command">command对象</param>
        /// <param name="action">下指令并且获取回传值的整个动作（下指令并且获取回传值事件）例：()=>{ command.WriteSendReturn() } </param>
        /// <param name="readData">按键操作对应指令返回值</param>
        /// <param name="name">按键操作对应窗口名</param>
        /// <returns></returns>
        public bool ButtonTest(MechHID command,Action action,string readData,string name)
        {
            var flag = true;
            Task.Run(() =>
            {
                Thread.Sleep(50);
                while (flag)
                {
                    action.Invoke();
                    if (command.ReturnValue == readData)
                    {
                        _bar.DialogResult = DialogResult.OK;
                    }
                    Thread.Sleep(100);
                }
            });
            var result = ProgressBarsBox(name);
            flag = false;
            return result;
        }
        
        /// <summary>
        /// 按键测试
        /// </summary>
        /// <param name="func">传入方法, _button.ButtonTest(() =&gt; BtnTest("0x01"), "请按Teams键")) </param>
        /// <param name="name">窗口名</param>
        /// <returns></returns>
        public bool ButtonTest(Func<bool> func,string name)
        {
            var flag = true;
            Task.Run(() =>
            {
                Thread.Sleep(50);
                while (flag)
                {
                    if (func.Invoke())
                    {
                        _bar.DialogResult = DialogResult.OK;
                    }
                    Thread.Sleep(100);
                }
            });
            var result = ProgressBarsBox(name);
            flag = false;
            return result;
        }

        private ProgressBars _bar;
        
        #region 进度条
        
        private bool ProgressBarsBox(string name)
        {
            try
            {
                _bar = new ProgressBars(name);
                return _bar.ShowDialog() == DialogResult.OK;
            } catch
            {
                return false;
            }
        }
        #endregion
    }
}
