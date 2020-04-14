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
            try
            {
                _client = new TcpClient("localhost", 5402);
                _ns = _client.GetStream();
                _ns.ReadTimeout = 10000;
                _ns.WriteTimeout = 10000;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Disconnect()
        {
            try {
                _ns.Close();
                _client.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public string Read()
        {
            string retval;
            byte[] bytes= new byte[1024];
            int bytesRead;
            try { 
                bytesRead = _ns.Read(bytes, 0, bytes.Length); 
            } catch (TimeoutException e)
            {
                throw e;
            } catch(Exception e)
            {
                if (e.Message.Contains("time"))
                {
                    throw new TimeoutException(e.Message);
                }
                throw e;
            }
            
            if(bytesRead == 0)
            {
                throw new Exception("couldn't read from server");
            }
            retval = Encoding.ASCII.GetString(bytes, 0, bytesRead);
            return retval;
        }

        public void Write(string command)
        {
            byte[] bytes = new byte[1024];
            bytes = Encoding.ASCII.GetBytes(command);
            try
            {
                _ns.Write(bytes, 0, bytes.Length);
            }
            catch(Exception e)
            {
                if (e.Message.Contains("time"))
                {
                    throw new TimeoutException(e.Message);
                }
                throw e;
            }                    
        }
    }
}
