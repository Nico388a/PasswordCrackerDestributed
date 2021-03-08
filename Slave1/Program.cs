using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using PasswordLib;

namespace Slave1
{
   public class Program
    {
        static void Main(string[] args)
        {
            
            Cracking cracking = new Cracking();
            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect(IPAddress.Loopback, 6789);
            Stream ns = clientSocket.GetStream(); //provides a NetworkStream
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            //request the server for a chunk/dictionary/passwords
            sw.WriteLine("password");
            List<UserInfo> userInfos = new List<UserInfo>();
            
            Console.ReadKey();

            ns.Close();
            clientSocket.Close();

        }
    }
}
