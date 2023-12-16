using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MechTE_480.network
{
    /// <summary>
    /// Socket客户端
    /// </summary>
    public class SocketClient
    {
        private readonly string _ip;
        private readonly int _port;
        private Socket _socket;
        private readonly byte[] _buffer = new byte[1024 * 1024 * 2];
 
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">连接服务器的IP</param>
        /// <param name="port">连接服务器的端口</param>
        public SocketClient(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">监听当前电脑端口</param>
        public SocketClient(int port)
        {
            _ip = "127.0.0.1";
            _port = port;
        }
        
        
        /// <summary>
        /// 连接服务器
        /// </summary>
        public void ConnectServer()
        {
            try
            {
                //1.0 实例化套接字(IP4寻址地址,流式传输,TCP协议)
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //2.0 创建IP对象
                IPAddress address = IPAddress.Parse(_ip);
                //3.0 创建网络端口包括ip和端口
                IPEndPoint endPoint = new IPEndPoint(address, _port);
                //4.0 与远程主机建立连接。Connect() 有四个重载方法，不必关注，只需知道，必需提供 IP 和 Post 两个值
                _socket.Connect(endPoint);
                _socket.Send(Encoding.UTF8.GetBytes("连接服务器成功"));
            }
            catch
            {
                // ignored
            }
        }
  
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(string message)
        {
            try
            {
                _socket.Send(Encoding.UTF8.GetBytes(message));
            }
            catch
            {
                // ignored
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            try
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
            }
            catch
            {
                // ignored
            }
        }
        
        /// <summary>
        /// 接收服务端消息
        /// </summary>
        /// <returns></returns>
        public string ReceiveMessage()
        {
            try
            {
                //获取从服务端发来的数据
                int length = _socket.Receive(_buffer);
                return _socket.RemoteEndPoint+Encoding.UTF8.GetString(_buffer, 0, length);
            }
            catch (Exception)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
            }

            return "false";
        }
      
        
        // /// <summary>
        // /// 开启服务,连接服务端
        // /// </summary>
        // public void StartClient()
        // {
        //     try
        //     {
        //         //1.0 实例化套接字(IP4寻址地址,流式传输,TCP协议)
        //         _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //         //2.0 创建IP对象
        //         IPAddress address = IPAddress.Parse(_ip);
        //         //3.0 创建网络端口包括ip和端口
        //         IPEndPoint endPoint = new IPEndPoint(address, _port);
        //         //4.0 建立连接
        //         _socket.Connect(endPoint);
        //          Console.WriteLine(@"连接服务器成功");
        //         //5.0 接收数据
        //         int length = _socket.Receive(_buffer);
        //         Console.WriteLine(@"接收服务器{0},消息:{1}", _socket.RemoteEndPoint, Encoding.UTF8.GetString(_buffer,0,length));
        //         //6.0 像服务器发送消息
        //         for (int i = 0; i < 10; i++)
        //         {
        //             Thread.Sleep(2000);
        //             string sendMessage = $"{i}:客户端发送的消息,当前时间{DateTime.Now.ToString(CultureInfo.CurrentCulture)}";
        //             _socket.Send(Encoding.UTF8.GetBytes(sendMessage));
        //             // Console.WriteLine(@"像服务发送的消息:{0}", sendMessage);
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         _socket.Shutdown(SocketShutdown.Both);
        //         _socket.Close();
        //         Console.WriteLine(ex.Message);
        //     }
        //     Console.WriteLine(@"发送消息结束");
        //     Console.ReadKey();
        // }
    }
}