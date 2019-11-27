using Common.Enums;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace TcpServer
{
    public class ClientObject
    {
        protected internal string Id { get; private set; }
        protected internal NetworkStream Stream { get; private set; }
        string userName;
        TcpClient client;
        ServerObject server; // объект сервера

        public ClientObject(TcpClient tcpClient, ServerObject serverObject)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            server = serverObject;
            serverObject.AddConnection(this);
        }

        public void Process()
        {
            try
            {
                Stream = client.GetStream();
                // получаем имя пользователя
                string message = GetMessage();
                userName = message;
                int dataDirection = 0;

                message = userName + " подключился";
                Console.WriteLine(message);
                // в бесконечном цикле получаем сообщения от клиента
                while (true)
                {
                    try
                    {
                        dataDirection = GetData();
                        Console.WriteLine($"{userName} изменил направление на {(Direction)dataDirection}");

                        if (dataDirection != 0 || dataDirection != 1 || dataDirection != 2 || dataDirection != 3 || dataDirection != 4)
                        {
                            server.BroadcastData((Direction)dataDirection, this.Id);
                        }

                        dataDirection = 0;
                    }
                    catch(Exception ex)
                    {
                        message = string.Format("{0}: отключился", userName);
                        Console.WriteLine(message);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                server.RemoveConnection(Id);
                Close();
            }
        }

        // чтение входящего сообщения и преобразование в строку
        private string GetMessage()
        {
            byte[] data = new byte[64]; // буфер для получаемых данных
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);

            return builder.ToString();
        }

        // чтение входящих данных
        private int GetData()
        {
            byte[] data = new byte[64]; // буфер для получаемых данных
            int dataValue = 0;
            do
            {
                Stream.Read(data, 0, data.Length);

            }
            while (Stream.DataAvailable);
            dataValue = BitConverter.ToInt32(data, 0);
            return dataValue;
        }

        // закрытие подключения
        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
    }
}
