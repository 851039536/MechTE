using System;
using System.Collections.Generic;

namespace MechTE_480.TemplateCategory
{
    /// <summary>
    /// 模板函数接口
    /// </summary>
    public interface IMerryDll
    {
        /// <summary>
        /// 程序每次开始运行时触发
        /// </summary>
        /// <returns></returns>
        bool StartRun();

        /// <summary>
        /// 程序启动时第三个触发的接口，运作代码可以自定义，返回 false表示程序不允许启动
        /// </summary>
        /// <param name="formsData">数据集合（本dll被实例化为主程序中对象，所以主程序formsData与本参数，以及本参数赋值后的参数，指向同一堆对象）</param>
        /// <param name="handel">主程序主窗体句柄</param>
        /// <returns>启动是否成功</returns>
        bool Start(List<string> formsData, IntPtr handel);

        /// <summary>
        /// 调用内部方法
        /// </summary>
        /// <param name="message">指令，决定调用哪个方法</param>
        /// <returns>方法调用后回传值</returns>
        string Run(string message);

        /// <summary>
        /// 模板中的全局变量,
        /// 接收如BD号，SN，测试站别、机型、客户SN、测试计划、测试值、测试结果等等，详细参数可参照Config字典.xlsx 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        object Interface(Dictionary<string, object> keys);

        /// <summary>
        /// 版本信息接口
        /// </summary>
        /// <returns></returns>
        string[] GetDllInfo();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        void TestsEnd(object obj);
    }
}