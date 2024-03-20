using System;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;

namespace MechTE_Speech
{
    /// <summary>
    /// 语音识别
    /// </summary>
    public static class MSpeech
    {
        private static readonly SpeechSynthesizer SSy = new SpeechSynthesizer();

        /// <summary>
        /// 接收语音输出内容 
        /// </summary>
        /// <param name="cmdText">配置输入内容 Task.Run(() => MSpeech.OutSpeech(new[] { "测试", "一", "二" }));</param>
        public static void OutSpeech(string[] cmdText)
        {
            //创建中文识别器,引擎
            using (var recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("zh-CN")))
            {
                foreach (var config in SpeechRecognitionEngine.InstalledRecognizers())
                {
                    Console.WriteLine(config.Id);
                }

                //初始化命令词
                var commons = new Choices();
                //添加命令词
                commons.Add(cmdText);
                //初始化命令词管理
                var gBuilder = new GrammarBuilder();
                //将命令词添加到管理中
                gBuilder.Append(commons);
                //实例化命令词管理
                var grammar = new Grammar(gBuilder);

                //创建并加载听写语法(添加命令词汇识别的比较精准)
                recognizer.LoadGrammarAsync(grammar);
                //为语音识别事件添加处理程序。
                recognizer.SpeechRecognized += Recognizer_SpeechRecongized;
                // 使用默认音频设备  
                recognizer.SetInputToDefaultAudioDevice();
                // 或者  
                // recognizer.SetInputToWaveFile("path_to_wav_file.wav"); // 从 WAV 文件中读取音频

                //启动异步，连续语音识别。
                recognizer.RecognizeAsync(RecognizeMode.Multiple); //连续语音识别
                //recognizer.RecognizeAsync(RecognizeMode.Single); // 识别一次退出

                //配置识别引擎属性 , 你可以设置这些属性以优化识别性能或改变引擎的行为。
                // recognizer.BabbleTimeout = TimeSpan.FromSeconds(2); // 设置非语音（噪音）超时  
                // recognizer.InitialSilenceTimeout = TimeSpan.FromSeconds(1); // 设置初始静音超时  
                // recognizer.EndSilenceTimeout = TimeSpan.FromSeconds(1); // 设置结束静音超时  
                // recognizer.EndSilenceTimeoutAmbiguous = TimeSpan.FromSeconds(5); // 设置模拟识别超时
                
                // 获取 Singleton 的唯一实例  
                SpeechSingleton singletonInstance = SpeechSingleton.Instance;
                // 现在可以使用 singletonInstance 进行操作  
                while (singletonInstance.SpeechState) // 防止退出
                {
                    Thread.Sleep(1000);
                }
                Console.WriteLine("退出!");
            }
        }

        //事件处理(识别后进行业务处理)
        private static void Recognizer_SpeechRecongized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result == null || !(e.Result.Confidence > 0.6)) return;
            
            
            Console.WriteLine(@"识别结果：" + e.Result.Text + @" " + e.Result.Confidence + @" " + DateTime.Now);
            SSy.Speak(e.Result.Text);
        }
    }
}