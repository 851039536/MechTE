using System;
using System.IO.Ports;

namespace MechTE_480.port
{
    /// <summary>
    /// 封装串口操作
    /// </summary>
    public class MSerialPort
    {
        private readonly SerialPort _serialPort;

        /// <summary>
        /// 事件，用于通知接收到的数据
        /// </summary>
        public event EventHandler<string> DataReceived;

        /// <summary>
        /// 对象初始化
        /// </summary>
        /// <param name="portName">COM3</param>
        /// <param name="baudRate">9600</param>
        /// <param name="parity">Parity.None</param>
        /// <param name="dataBits">8</param>
        /// <param name="stopBits">StopBits.One</param>
        public MSerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            _serialPort = new SerialPort();
            _serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            // 绑定数据接受监听事件
            _serialPort.DataReceived += SerialPort_DataReceived;

            // 设置默认的串口属性，你可以根据需要进行修改
            // serialPort.BaudRate = 9600;
            // serialPort.DataBits = 8;
            // serialPort.StopBits = StopBits.One;
            // serialPort.Parity = Parity.None;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // 数据接收后需要干的活
            // 串口数据接收事件处理
            string data = _serialPort.ReadExisting();
            Console.WriteLine(data);
            // 触发事件通知接收到的数据
            OnDataReceived(data);
        }
        

        /// <summary>
        /// 确保事件在非UI线程上触发时不会引发异常
        /// </summary>
        /// <param name="data"></param>
        protected virtual void OnDataReceived(string data)
        {
            // 确保事件在非UI线程上触发时不会引发异常
            DataReceived?.Invoke(this, data);
        }

        /// <summary>
        /// 写字符串指令
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="ApplicationException"></exception>
        public void SendData(string data)
        {
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }

                if (!_serialPort.IsOpen) return;

                _serialPort.Write(data);
                _serialPort.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 写二进制指令
        /// </summary>
        /// <param name="data">如:byte[] s1 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x01, 0x89 }</param>
        /// <param name="f">默认 0</param>
        /// <param name="l">字节数</param>
        /// <exception cref="ApplicationException"></exception>
        public void SendData(byte[] data, int f, int l)
        {
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }

                if (!_serialPort.IsOpen) return;

                _serialPort.Write(data, f, l);
                _serialPort.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        /// <summary>
        /// 16进制字符串转化为字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] ParseHexString(string hexString)
        {
            int numberOfChars = hexString.Length;
            byte[] bytes = new byte[numberOfChars / 2];

            for (int i = 0; i < numberOfChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return bytes;
        }
        
        /// <summary>
        /// 使用16进制字符串发送数据
        /// </summary>
        /// <param name="hexString"></param>
        public void SendHexString(string hexString)
        {
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }

                if (!_serialPort.IsOpen) return;

                byte[] hexBytes = ParseHexString(hexString);

                _serialPort.Write(hexBytes, 0, hexBytes.Length);
                _serialPort.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 获取可用串口设备的名称数组
        /// </summary>
        public static string[] GetPortName()
        {
            // 获取可用串口设备的名称数组
            var portNames = SerialPort.GetPortNames();
            // 检查是否有可用串口
            return portNames.Length > 0 ? portNames : null;
        }
    }
}