using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPReaderAsServer
{


    class Program
    {

        static void Main(string[] args)
        {
            TCPServer();
        }
        public static void TCPServer()
        {
            string medicion = string.Empty;
            try
            {
                int port = 8010;
                string dato = string.Empty;
                TcpListener myList = new TcpListener(IPAddress.Any, port);
                myList.Start();

                Console.WriteLine($"Server running at port {port}...");
                Console.WriteLine("Waiting for a connection...");

                Socket socket = myList.AcceptSocket();
                Console.WriteLine("Connection accepted from " + socket.RemoteEndPoint);

                byte cantidadEquipos = 2;
                byte[] sizeBuffer = new byte[90 * cantidadEquipos];
                int sizeTrama = socket.Receive(sizeBuffer);
                Console.WriteLine("Recieved...");
                for (int count = 0; count < sizeTrama; count++)
                {
                  Console.Write(Convert.ToChar(sizeBuffer[count]));
                }


                ASCIIEncoding asen = new ASCIIEncoding();
                socket.Send(asen.GetBytes("The string was recieved by the server."));
                Console.WriteLine("\nSent Acknowledgement");
                /* clean up */
                socket.Close();
                Console.Read();
                myList.Stop();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.StackTrace);
                Console.Read();
            }
        }

    }
}