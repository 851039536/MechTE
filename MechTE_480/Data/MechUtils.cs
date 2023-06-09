using System.Linq;

namespace MechTE_480.Data
{
    public class MechUtils
    {
        /// <summary>
        /// 生成数字字符串序列
        /// </summary>
        /// <param name="startNumber">序列中第一个整数的值</param>
        /// <param name="sequenceLength">要生成的顺序总条数</param>
        /// <returns>生成-> 0 01 02 03...</returns>  
        public static string GenerateNumberStringSequence(int startNumber, int sequenceLength)
        {
            // 生成一个包含数字字符串的序列                                                                                                                                 
            var strLen = Enumerable.Range(startNumber, sequenceLength).Select(i => i.ToString())
                .Aggregate((a, b) => a + " " + b);
            return strLen;
        }
    }
}