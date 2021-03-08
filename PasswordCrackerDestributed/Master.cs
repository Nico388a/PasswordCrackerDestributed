using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using PasswordLib;

namespace PasswordCrackerDestributed
{
    public class Master
    {
        public void Listener(int port)
        {
            //Read the passwords into a list
            List<UserInfo> userInfos =
                PasswordFileHandler.ReadPasswordFile("passwords.txt");

            ////Read the Desctionary
            List<List<string>> chunks = new List<List<string>>();

            using (FileStream fs = new FileStream("webster-dictionary.txt", FileMode.Open, FileAccess.Read))
            using (StreamReader dictionary = new StreamReader(fs))
            {
                List<string> tempList = new List<string>();
                while (!dictionary.EndOfStream)
                {
                    //if you want to send chunks to the client then create chunks
                    //You must use a logic where it puts 10000 dictinary words in tempList and then
                    //Adds tempList to chucks
                    tempList.Add(dictionary.ReadLine());
                    //after the modulus % condition is satisfied i.e. when there is 10000 words in tempList
                    //then
                    if (tempList.Count == 10000)
                    {
                        chunks.Add(tempList);
                        tempList = new List<string>();

                    }
                    // IEnumerable<UserInfoClearText> partialResult = CheckWordWithVariations(dictionaryEntry, userInfos);
                    // result.AddRange(partialResult);
                }
                chunks.Add(tempList);
            }
            //now you can send the first chuck to the Slave i.e. the client

            //TcpListener serverSocket = new TcpListener(port);
            TcpListener server = new TcpListener(IPAddress.Loopback, port);
            server.Start();
            Console.WriteLine("Server listning on port: " + port);

            TcpClient connectionSocket = server.AcceptTcpClient();
            Console.WriteLine("Server activated");

            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string request = sr.ReadLine();
            Console.WriteLine(request);
        }
    }
}
