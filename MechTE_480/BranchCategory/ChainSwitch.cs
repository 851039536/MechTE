using System;

namespace MechTE_480.BranchCategory
{
    /// <summary>
    /// Switch链式调用
    /// </summary>
    public class ChainSwitch
    {
        /// <summary>
        /// 只读的字符串字段，用于存储传递给ChainSwitch构造函数的value
        /// </summary>
        private readonly string _value;

        /// <summary>
        /// 公共字符串字段，用于存储与_value匹配的情况的结果。
        /// </summary>
        public string Values;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public ChainSwitch(string value)
        {
            _value = value;
        }

        ///  <summary>
        /// 匹配_value与func是否相等，如果相等，则执行func函数，并将func函数的返回值赋值给Values字段。
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public ChainSwitch Case(Func<string> func)
        {
            var methodName = func.Method.Name;
            if (_value == methodName)
            {
                Values = func();
            }
            return this;
        }

        ///  <summary>
        /// 匹配_value与func是否相等，如果相等，则执行func函数，并将func函数的返回值赋值给Values字段。
        /// </summary>
        /// <param name="func"></param>
        ///  <param name="name"></param>
        ///  <returns></returns>
        public ChainSwitch Case(Func<string, string> func, string name)
        {
            var methodName = func.Method.Name;
            if (_value == methodName)
            {
                
                Values = func(name);
            }

            return this;
        }

        ///  <summary>
        /// 匹配_value与value是否相等，如果相等，则执行func函数，并将func函数的返回值赋值给Values字段。
        /// </summary>
        /// <param name="value"></param>
        ///  <param name="func"></param>
        ///  <param name="name"></param>
        ///  <returns></returns>
        public ChainSwitch Case(string value, Func<string,string> func , string name)
        {
            if (_value == value)
            {
                
                Values = func(name);
            }
            return this;
        }
        
        /// <summary>
        /// 匹配_value与value是否相等，如果相等，则执行func函数，并将func函数的返回值赋值给Values字段。
        /// 特别注意: .Case("test",  test("01").ToString); 这种方式会连续调用test函数
        /// </summary>
        /// <param name="value"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public ChainSwitch Case(string value, Func<string> func )
        {
            if (_value == value)
            {
                Values = func();
            }
            return this;
        }
    }
}