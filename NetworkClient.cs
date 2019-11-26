using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace SnakeGame
{
    public class NetworkClient : IConnection
    {
        private readonly TcpClient _client;

        public NetworkClient(TcpClient client)
        {
            _client = client;
        }

        public void Connect(string ipAdress, int port, string nameOfClient)
        {
            try
            {
                _client.Connect(ipAdress, port);
                var stream = _client.GetStream();
                string message = nameOfClient;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
                _client.Close();
            }
            catch (Exception ex)
            {
                var exception = ex;
            }
        }

        public void SendData(Direction direction)
        {
            if (!_client.Connected)
            {
                _client.Connect("127.0.0.1", 8888);
            }
            using (var stream = _client.GetStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write((int)direction);
                    writer.Flush();
                }
            }
        }

        public Direction RecieveData()
        {
            if (!_client.Connected)
            {
                _client.Connect("127.0.0.1", 8888);
            }
            using (var stream = _client.GetStream())
            {
                using (var reader = new BinaryReader(stream))
                {
                    Direction direction = (Direction)reader.ReadInt32();
                    return direction;
                }
            }
        }

        private void Disconnect()
        {
            if (_client != null)
                _client.Close();//отключение клиента
            Environment.Exit(0); //завершение процесса
        }
    }
}
