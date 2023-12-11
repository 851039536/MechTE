using System;
using System.IO.Ports;
using System.Threading;

namespace MechTE_480.port
{
    /// <summary>
    /// 调节32路继电器类
    /// </summary>
    public static class MPCBA_32
    {
        #region 
        private static readonly byte[] A = { 0x55, 0x01, 0x13, 0x00, 0x00, 0x00, 0x00, 0x69 };
        private static readonly byte[] S1 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x01, 0x89 };
        private static readonly byte[] S2 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x02, 0x8a };
        private static readonly byte[] S3 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x03, 0x8b };
        private static readonly byte[] S4 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x04, 0x8c };
        private static readonly byte[] S5 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x05, 0x8d };
        private static readonly byte[] S6 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x06, 0x8e };
        private static readonly byte[] S7 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x07, 0x8f };
        private static readonly byte[] S8 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x08, 0x90 };
        private static readonly byte[] S9 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x09, 0x91 };
        private static readonly byte[] S10 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x0a, 0x92 };
        private static readonly byte[] S11 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x0b, 0x93 };
        private static readonly byte[] S12 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x0c, 0x94 };
        private static readonly byte[] S13 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x0d, 0x95 };
        private static readonly byte[] S14 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x0e, 0x96 };
        private static readonly byte[] S15 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x0f, 0x97 };
        private static readonly byte[] S16 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x10, 0x98 };
        private static readonly byte[] S17 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x11, 0x99 };
        private static readonly byte[] S18 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x12, 0x9a };
        private static readonly byte[] S19 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x13, 0x9b };
        private static readonly byte[] S20 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x14, 0x9c };
        private static readonly byte[] S21 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x15, 0x9d };
        private static readonly byte[] S22 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x16, 0x9e };
        private static readonly byte[] S23 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x17, 0x9f };
        private static readonly byte[] S24 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x18, 0xa0 };
        private static readonly byte[] S25 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x19, 0xa1 };
        private static readonly byte[] S26 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x1a, 0xa2 };
        private static readonly byte[] S27 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x1b, 0xa3 };
        private static readonly byte[] S28 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x1c, 0xa4 };
        private static readonly byte[] S29 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x1d, 0xa5 };
        private static readonly byte[] S30 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x1e, 0xa6 };
        private static readonly byte[] S31 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x1f, 0xa7 };
        private static readonly byte[] S32 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x20, 0xa8 };
        
        /// <summary>
        /// 关闭的指令
        /// </summary>
        private static readonly byte[] Off = { 0x55, 0x01, 0x31, 0x00, 0x00, 0x00, 0x00, 0x87 };
        
        #endregion
        /// <summary>
        /// 判断存储的容器
        /// </summary>
        private static string[] _vessel;

        
        /// <summary>
        /// 启动指定模板-32
        /// </summary>
        /// <param name="portName">COM口</param>
        /// <param name="number">打开通道的名字用“.”隔开 例子“1.2.3.4.5”</param>
        /// <returns></returns>
        public static bool command(string portName, string number)
        {
            try
            {
                // SerialPort port = new SerialPort(portName);
                var mSerialPort = new MSerialPort(portName, 9600, Parity.None, 8, StopBits.One);
                string[] ch = number.Split('.');
                if (_vessel == null)
                {
                    mSerialPort.SendData(A, 0, A.Length);
                }
                else //第二次下指令将会关闭之前开过的通道
                {
                    for (int i = 0; i < _vessel.Length; i++)
                    {
                        foreach (var item in ch)
                        {
                            if (_vessel[i].Equals(item)) { _vessel[i] = ""; break; }
                        }
                    }
                    //下指令关闭通道
                    foreach (var item in _vessel)
                    {
                        try
                        {
                            if (item == "") continue;
                            Thread.Sleep(2);
                            Off[6] = (byte)(Convert.ToInt16(item));
                            Off[7] = (byte)(Off[0] + Off[1] + Off[2] + Off[3] + Off[4] + Off[5] + Off[6]);
                            mSerialPort.SendData(Off, 0, Off.Length);
                        }
                        catch { }
                    }
                }
                //下指令开通通道
                Thread.Sleep(15);
                foreach (var item in ch)//根据输入的序号判断启动模块
                {
                    Thread.Sleep(5);
                    switch (item)
                    {
                        case "0": mSerialPort.SendData(A, 0, A.Length); break;
                        case "1": mSerialPort.SendData(S1, 0, S1.Length); break;
                        case "2": mSerialPort.SendData(S2, 0, S2.Length); break;
                        case "3": mSerialPort.SendData(S3, 0, S3.Length); break;
                        case "4": mSerialPort.SendData(S4, 0, S4.Length); break;
                        case "5": mSerialPort.SendData(S5, 0, S5.Length); break;
                        case "6": mSerialPort.SendData(S6, 0, S6.Length); break;
                        case "7": mSerialPort.SendData(S7, 0, S7.Length); break;
                        case "8": mSerialPort.SendData(S8, 0, S8.Length); break;
                        case "9": mSerialPort.SendData(S9, 0, S9.Length); break;
                        case "10": mSerialPort.SendData(S10, 0, S10.Length); break;
                        case "11": mSerialPort.SendData(S11, 0, S11.Length); break;
                        case "12": mSerialPort.SendData(S12, 0, S12.Length); break;
                        case "13": mSerialPort.SendData(S13, 0, S13.Length); break;
                        case "14": mSerialPort.SendData(S14, 0, S14.Length); break;
                        case "15": mSerialPort.SendData(S15, 0, S15.Length); break;
                        case "16": mSerialPort.SendData(S16, 0, S16.Length); break;
                        case "17": mSerialPort.SendData(S17, 0, S17.Length); break;
                        case "18": mSerialPort.SendData(S18, 0, S18.Length); break;
                        case "19": mSerialPort.SendData(S19, 0, S19.Length); break;
                        case "20": mSerialPort.SendData(S20, 0, S20.Length); break;
                        case "21": mSerialPort.SendData(S21, 0, S21.Length); break;
                        case "22": mSerialPort.SendData(S22, 0, S22.Length); break;
                        case "23": mSerialPort.SendData(S23, 0, S23.Length); break;
                        case "24": mSerialPort.SendData(S24, 0, S24.Length); break;
                        case "25": mSerialPort.SendData(S25, 0, S25.Length); break;
                        case "26": mSerialPort.SendData(S26, 0, S26.Length); break;
                        case "27": mSerialPort.SendData(S27, 0, S27.Length); break;
                        case "28": mSerialPort.SendData(S28, 0, S28.Length); break;
                        case "29": mSerialPort.SendData(S29, 0, S29.Length); break;
                        case "30": mSerialPort.SendData(S30, 0, S30.Length); break;
                        case "31": mSerialPort.SendData(S31, 0, S31.Length); break;
                        case "32": mSerialPort.SendData(S32, 0, S32.Length); break;
                    }
                }
                _vessel = number.Split('.');
                return true;
            }
            catch { return false; }
        }
    }
}
