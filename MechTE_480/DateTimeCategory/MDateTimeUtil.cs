using System;

namespace MechTE_480.DateTimeCategory
{
    /// <summary>
    /// 时间类操作
    /// </summary>
    public static class MDateTimeUtil
    {

       /// <summary>
       /// 获取当前日期 yyyy-MM-dd
       /// </summary>
       /// <returns></returns>
        public static string GetTime()
        {
            return  DateTime.Now.ToString("yyyy-MM-dd");;
        }
       
       /// <summary>
       /// 获取前一天时间 yyyy-MM-dd
       /// </summary>
       /// <returns></returns>
       public static string GetYesterdayTime()
       {
           return DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
       }
       
    }
}