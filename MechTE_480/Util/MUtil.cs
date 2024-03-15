using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using MechTE_480.RegexsCategory;

namespace MechTE_480.util
{
    /// <summary>
    /// 通用工具类
    /// </summary>
    public class MUtil
    {
        /// <summary>
        ///  定义一个泛型委托，用于定义带有超时检查的方法的签名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        public delegate TR TimeOutDelegate<in T, out TR>(T param);

        /// <summary>
        /// 检测传入的方法是否超时
        /// </summary>
        /// <param name="timeoutMethod">方法</param>
        /// <param name="param">方法的参数</param>
        /// <param name="result">执行结果</param>
        /// <param name="timeout">超时时间</param>
        /// <typeparam name="T">方法的参数类型</typeparam>
        /// <typeparam name="TR">执行结果的类型</typeparam>
        /// <returns>是否超时</returns>
        public static bool Execute<T, TR>(
            TimeOutDelegate<T, TR> timeoutMethod, T param, out TR result, TimeSpan timeout)
        {
            // 使用异步方式执行目标方法
            var asyncResult = timeoutMethod.BeginInvoke(param, null, null);

            // 等待指定的超时时间
            if (!asyncResult.AsyncWaitHandle.WaitOne(timeout, false))
            {
                // 如果超时，则将结果设置为默认值，并返回true
                result = default(TR);
                return true;
            }

            // 如果未超时，则获取执行结果，并返回false
            result = timeoutMethod.EndInvoke(asyncResult);
            return false;
        }


        /// <summary>
        /// 在指定的时间内等待某个函数的执行结果,调用 bool result = WaitSomething(5000, 1000, () =>{})
        /// </summary>
        /// <param name="timeout">表示等待的最大时间，以毫秒为单位</param>
        /// <param name="freq">表示等待的频率，即每隔多少毫秒检查一次函数的执行结果</param>
        /// <param name="func">表示要等待的函数，它是一个返回布尔值的委托</param>
        /// <returns></returns>
        public static bool WaitSomething(int timeout, int freq, Func<bool> func)
        {
            for (int index = 0; index < timeout; index += freq)
            {
                if (func())
                    return true;
                Thread.Sleep(freq);
            }

            return false;
        }

        /// <summary>
        /// 获取当前程序根目录地址(D:\File\bin\Debug)
        /// 如果引用dll不在根目录，会触发异常
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentProgramDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
        
        

        /// <summary>
        /// 生成数字字符串序列(传0,6生成 0 1 2 3 4 5)
        /// </summary>
        /// <param name="startNumber">序列中第一个整数的值</param>
        /// <param name="sequenceLength">生成的顺序总条数</param>
        /// <returns>string</returns>  
        public static string GenerateNumberSequence(int startNumber, int sequenceLength)
        {
            // 生成一个包含数字字符串的序列                                                                                                                                 
            var strLen = Enumerable.Range(startNumber, sequenceLength).Select(i => i.ToString())
                .Aggregate((a, b) => a + " " + b);
            return strLen;
        }

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        

        
        
        

        /// <summary>
        /// 验证是否为整数 如果为空，认为验证不合格 返回false
        /// </summary>
        /// <param name="number">要验证的整数</param>        
        public static bool IsInt(string number)
        {
            //如果为空，认为验证不合格
            if (MStringUtil.IsNullOrEmpty(number))
            {
                return false;
            }

            //清除要验证字符串中的空格
            number = number.Trim();

            //模式字符串
            string pattern = @"^[0-9]+[0-9]*$";

            //验证
            return MRegexUtil.IsMatch(number,pattern);
        }

        /// <summary>
        /// 验证是否为数字
        /// </summary>
        /// <param name="number">要验证的数字</param>        
        public static bool IsNumber(string number)
        {
            //如果为空，认为验证不合格
            if (MStringUtil.IsNullOrEmpty(number))
            {
                return false;
            }

            //清除要验证字符串中的空格
            number = number.Trim();

            //模式字符串
            string pattern = @"^[0-9]+[0-9]*[.]?[0-9]*$";

            //验证
            return MRegexUtil.IsMatch(number,pattern);
        }
    }
}