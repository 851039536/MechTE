using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MechTE_480.network
{
    /// <summary>
    /// Socket服务端
    /// </summary>
    public class SocketServer
    {
        private readonly string _ip;
        private readonly int _port;
        private Socket _socket;
        private readonly byte[] _buffer = new byte[1024 * 1024 * 2];
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">监听的IP</param>
        /// <param name="port">监听的端口</param>
        public SocketServer(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">监听当前电脑端口</param>
        public SocketServer(int port)
        {
            _ip = "0.0.0.0";
            _port = port;
        }
 
        /// <summary>
        /// 监控所有发送到此主机的连接请求
        /// </summary>
        public void StartListen()
        {
            try
            {
                //1.0 实例化套接字(IP4寻找协议,流式协议,TCP协议)
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //2.0 创建IP对象
                IPAddress address = IPAddress.Parse(_ip);
                //3.0 创建网络端口,包括ip和端口
                IPEndPoint endPoint = new IPEndPoint(address, _port);
                //4.0 绑定套接字
                _socket.Bind(endPoint);
                //5.0 设置最大连接数 / 监控所有发送到此主机的、特点端口的连接请求。服务端使用，客户端不需要
                _socket.Listen(int.MaxValue);
                Console.WriteLine(@"监听{0}消息成功", _socket.LocalEndPoint);
                //6.0 开始监听
                Thread thread = new Thread(ListenClientConnect);
                thread.Start();
 
            }
            catch (Exception)
            {
                // ignored
            }
        }
        /// <summary>
        /// 监听客户端连接
        /// </summary>
        private void ListenClientConnect()
        {
            try
            {
                while (true)
                {
                    //以同步方式监听套接字，在连接请求队列中提取第一个挂起的连接请求，然后创建并返回一个新的 Socket 对象。
                    Socket clientSocket = _socket.Accept();
                    // clientSocket.Send(Encoding.UTF8.GetBytes("服务端发送消息:"));
                    Thread thread = new Thread(ReceiveMessage);
                    thread.Start(clientSocket);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
 
        /// <summary>
        /// 接收客户端消息
        /// </summary>
        /// <param name="socket">来自客户端的socket</param>
        private void ReceiveMessage(object socket)
        {
            Socket clientSocket = (Socket)socket;
            while (true)
            {
                try
                {
                    clientSocket.Send(Encoding.UTF8.GetBytes("test"));
                    //获取从客户端发来的数据
                    int length = clientSocket.Receive(_buffer);
                    Console.WriteLine(@"接收客户端{0},消息:{1}", clientSocket.RemoteEndPoint, Encoding.UTF8.GetString(_buffer, 0, length));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }
            }
        }
    }
}