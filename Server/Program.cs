using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SocketListener
{
    public static int Main()
    {
        StartServer();
        return 0;
    }

    public static void StartServer()
    {
       
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        try
        {

          
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            listener.Bind(localEndPoint);
           
            listener.Listen(1);

            Console.WriteLine("Waiting for a connection...");
            Socket handler = listener.Accept();

            string data = null;
            byte[] bytes = null;

            while (true)
            {
                bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                Console.WriteLine("Text receivedd : {0}", data);
                if (data == "q")
                {
                    break;
                }
                data = null; 
                Console.Write("Send message to client : ");
                string message = Console.ReadLine();
                //string message = "message recieved by server...";
                //byte[] msg = Encoding.ASCII.GetBytes(data);
                byte[] msg = Encoding.ASCII.GetBytes(message);
                handler.Send(msg);
                
             
                
            }

            //Console.WriteLine("Text received : {0}", data);
            //string message = "message recieved by server...";
            ////byte[] msg = Encoding.ASCII.GetBytes(data);
            //byte[] msg = Encoding.ASCII.GetBytes(message);
            //handler.Send(msg);
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }


        Console.WriteLine("\n Press any key to continue...");
       
        //Console.ReadKey();
    }
}