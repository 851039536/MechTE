using System;

namespace MechTE_Speech
{
    /// <summary>
    /// 存储语音状态单例
    /// </summary>
    public class SpeechSingleton
    {
        private static readonly Lazy<SpeechSingleton> SLazy = new Lazy<SpeechSingleton>(() => new SpeechSingleton());

        public static SpeechSingleton Instance => SLazy.Value;

        private SpeechSingleton() { }
        
        // 设置语音状态是否退出
        public bool SpeechState { get; set; } = true;
    }
}