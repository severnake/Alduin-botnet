using System;
using System.IO;
using Starksoft.Aspen.Proxy;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace Alduin.Server.Modules
{
    public class Connecter
    {
        public static TcpClient TCP;
        public static StreamWriter Write;
        public static StreamReader Reader;
        private static Socks5ProxyClient proxyClient;

        public static string CreateTcpSend(string Address, string model)
        {
            int ReachPort = 50371; //ReachPort
            try
            {
                proxyClient = new Socks5ProxyClient("127.0.0.1", 9150);
                proxyClient.ProxyUserName = "";
                proxyClient.ProxyPassword = "";
                TCP = proxyClient.CreateConnection(Address, ReachPort);
                Write = new StreamWriter(TCP.GetStream());
                Write.Write(model);
                Write.Flush();
                StreamReader reader = new StreamReader(TCP.GetStream());
                var RespondSize = "";
                Byte[] MessageFront = new Byte[0];
                bool size = true;
                while(reader.Peek() > -1){
                    char character = Convert.ToChar(reader.Read());
                    if (Char.IsNumber(character) && size)
                    {
                        RespondSize += character;
                    }
                    else
                    {
                        size = false;
                        MessageFront = addByteToArray(MessageFront, Convert.ToByte(character));
                    }
                }
                NetworkStream stream = TCP.GetStream();
                int readSoFar = 0;
                //int messageSize = TCP.ReceiveBufferSize;
                int messageSize = Int32.Parse(RespondSize);
                byte[] msg = new byte[messageSize];
                while (readSoFar < messageSize)
                {
                    var read = stream.Read(msg, readSoFar, msg.Length - readSoFar);
                    readSoFar += read;
                    if (read == 0)
                        break;  
                }
                stream.Close();
                TCP.Close();
                string responseData = Reverse(System.Text.Encoding.UTF8.GetString(cutByte(MessageFront), 0, cutByte(MessageFront).Length));
                responseData += System.Text.Encoding.UTF8.GetString(cutByte(msg), 0, cutByte(msg).Length);
                return responseData;
            }
            catch (Exception ex)
            {
                return "Error: " + ex;
            }
        }
        private static Byte[] cutByte(Byte[] inputByte)
        {
            int i = inputByte.Length - 1;
            while (inputByte[i] == 0)
                --i;

            byte[] bar = new byte[i + 1];
            Array.Copy(inputByte, bar, i + 1);
            return bar;
        }
        public static byte[] addByteToArray(byte[] bArray, byte newByte)
        {
            byte[] newArray = new byte[bArray.Length + 1];
            bArray.CopyTo(newArray, 1);
            newArray[0] = newByte;
            return newArray;
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
