using System;
using MechTE.Files;
using MechTE.Cmd;

namespace MechTE.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TFile file = new TFile();
            var r = file.GetDesktop(@"C:\Users\CH190006\Desktop");
            //file.VOpenFile(@"D:\software\MES Client\Sajet MES.exe");

            //var res=  TConvertHelpers.RepairZero("test",10);
            // var res = TConvertHelpers.ConvertBase("15",10,16);
            // var res = TConvertHelpers.StringToBytes("test",System.Text.Encoding.UTF8);
            //var res2 = TConvertHelpers.BytesToString(res,System.Text.Encoding.UTF8);
            // var res2 = TConvertHelpers.BytesToInt32(res);

            //TFile.OpenFile("D:\\sw\\源码 文档 视频 图片\\MD");
            //var res=  TFile.QueryFile("D:\\sw\\源码 文档 视频 图片\\MD\\记录");
            //var R =  TFile.IsExistDirectory(@"D:\\sw\\源码 文档 视频 图片\\MD\\记录");
            //var R = TFile.GetFileNames(@"D:\\sw\\源码 文档 视频 图片\\MD\\记录");
            //var R = TFile.GetDirectories(@"D:\\sw\\源码 文档 视频 图片\\MD\\记录");
            //var R = TFile.IsEmptyDirectory(@"D:\\sw\\源码 文档 视频 图片\\MD\\记录");
            //var R = TFile.Contains(@"D:\\sw\\源码 文档 视频 图片\\MD\\记录","*");
            //TFile.WriteFile("C:\\Users\\ch190006\\Desktop\\1.txt","test"); 
            //var r=   TFile.GetFileAttibe("C:\\Users\\ch190006\\Desktop\\1.txt");
            //  TIni.TIni.TGetIniAll();
            //    TIni.TIni. TDelIniValue(@".\CONFIG.ini","CONFIG","Delay1");
            // TIni.TIni.TDelIniSection(@".\CONFIG.ini","DONGLE1");

            //TForm.MesBox("MechTE.Test","123");
            //var r=  StringCAPTCHA.Number(1);


            //var r=   TSystems.GetWindows();

            //var r = TSystems.GetCurrentProcess();

            // TZips.TZipDirectory(@"C:\Users\ch190006\Desktop\新增資料夾 (2)", @"C:\Users\ch190006\Desktop\新增資料夾 (2)\123.zip");

            //var r = XMLProcess.Read(@"D:\\sw\\type_sw\\19.HDT657\\HDT657\\TestItem\\HDT657\\setting\cmd_sw.xml","/CommandBook");
            //检索计算机上的所有服务
            //ServiceController[] MyServices = ServiceController.GetServices();

            //var r=   TConvertHelpers.ImgToBase();
            //TConvertHelpers.BaseToImg(r);

            // 您需要在 https://beta.openai.com/docs/api-reference/introduction 创建API密钥来访问OpenAI API
            //var apiKey = "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";
            //var prompt = "Hello, how are you?";
            //var model = "text-davinci-002"; // 您可以选择其他模型名称，具体取决于您的需求

            //using (var client = new HttpClient()) {
            //    client.DefaultRequestHeaders.Add("Authorization",$"Bearer {apiKey}");
            //    var requestUri = new Uri($"https://api.openai.com/v1/engines/{model}/completions");
            //    var requestBody = new { prompt = prompt,max_tokens = 60,n = 1,stop = "\n" };
            //    var json = JObject.FromObject(requestBody);
            //    var content = new StringContent(json.ToString(),Encoding.UTF8,"application/json");
            //    var response = client.PostAsync(requestUri,content).Result;
            //    var jsonString = response.Content.ReadAsStringAsync().Result;
            //    var jsonResponse = JObject.Parse(jsonString);
            //    var text = jsonResponse["choices"][0]["text"].ToString();
            //    Console.WriteLine(text);

            Console.ReadKey();
            //}
        }
    }
}