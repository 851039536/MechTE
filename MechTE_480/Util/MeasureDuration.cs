using System;
using System.Diagnostics;

namespace MechTE_480.Util
{
    /// <summary>
    /// 定义了一个名为MeasureDuration的类，用于测量代码的执行时间。该类实现了IDisposable接口，以便在代码块结束时自动释放资源。
    /// </summary>
    public class MeasureDuration : IDisposable
    {
        private readonly Stopwatch _stopwatch;
        private readonly Action<TimeSpan> _callback;

        /// <summary>
        /// 构造函数接受一个Action&lt;TimeSpan&gt;类型的回调函数作为参数。在构造函数中，我们将回调函数赋值给私有字段_callback，并使用Stopwatch.StartNew()方法启动一个新的计时器。
        /// </summary>
        /// <param name="callback"></param>
        public MeasureDuration(Action<TimeSpan> callback)
        {
            _callback = callback;
            _stopwatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _stopwatch.Stop();
            _callback(_stopwatch.Elapsed);
        }
    }
}