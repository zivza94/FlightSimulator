using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide
{
    public class Client : IClient
    {
        private TcpClient _client;
        private NetworkStream _ns;
        public void Connect(string ip, int port)
        {
            _client = new TcpClient("localhost",5402);
            _ns = _client.GetStream();
        }

        public void Disconnect()
        {
            _ns.Close();
            _client.Close();
        }

        public string Read()
        {
            string retval;
            byte[] bytes= new byte[1024];
            int bytesRead = _ns.Read(bytes, 0, bytes.Length);
            retval = Encoding.ASCII.GetString(bytes, 0, bytesRead);
            return retval;
        }

        public void Write(string command)
        {
            byte[] bytes = new byte[1024];
            bytes = Encoding.ASCII.GetBytes(command);
            _ns.Write(bytes,0,bytes.Length);
        }
    }
}
