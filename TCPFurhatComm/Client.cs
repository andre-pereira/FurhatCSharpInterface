using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using ExtensionMethods;

namespace TCPFurhatComm
{
    public class Client
    {
        public event Action<string, string> MessageReceived;

        private TcpClient socket;
        private Thread listenThread;

        /// <summary>
        /// Create a furhat tcp client
        /// </summary>
        /// <param name="host"> use furhat's port or localhost if running the server locally </param>
        /// <param name="port"> port to receive events from furhat/iristk (default: 1932) </param>
        public Client(String host = "localhost", int port = 1932)
        {
            socket = new TcpClient(host, port);
            listenThread = new Thread(ReadThread);
            listenThread.Start();
        }

        public void Close()
        {
            socket.Close();
            listenThread.Abort();
        }

        /// <summary>
        /// Send data to Furhat
        /// </summary>
        /// <param name="msg"> message to send in string format </param>
        public void Send(string msg)
        {
            NetworkStream netStream = socket.GetStream();
            Byte[] sendBytes = Encoding.ASCII.GetBytes(msg);
            netStream.Write(sendBytes, 0, sendBytes.Length);
        }

        private void ReadThread()
        {
            NetworkStream netStream = socket.GetStream();
            StringBuilder sb = new StringBuilder();
            bool connectMessageReceived = false;

            while (socket.Connected)
            {
                //Read however you want, something like:
                // Reads NetworkStream into a byte buffer. 
                byte[] bytes = new byte[socket.ReceiveBufferSize];

                // Read can return anything from 0 to numBytesToRead.  
                // This method blocks until at least one byte is read.
                int bytesRead = netStream.Read(bytes, 0, (int)socket.ReceiveBufferSize);
                //
                //appends to our string builder and clears all \n characters as it makes it easier to parse
                sb.Append((Encoding.ASCII.GetString(bytes, 0, bytesRead)).Replace("\n", string.Empty));

                //Discards the first message received as we do not need to process it
                if (!connectMessageReceived)
                {
                    if (sb.ToString().Contains("SUBSCRIBE **CONNECTED"))
                    {
                        sb.Clear();
                        connectMessageReceived = true;
                    }
                }
                else
                {
                    if (sb.Length > 0)
                    {
                        //means that we received a full event
                        if (sb.ToString().Last() == '}')
                        {
                            //We might have received two events together
                            if (Regex.Matches(sb.ToString(), "EVENT").Count > 1)
                                TriggerMultipleMessageEvents(sb.ToString());
                            else
                            {
                                //process message and clear stringbuilder
                                TriggerMessageEvent(sb.ToString());
                            }
                            sb.Clear();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This function is useful for triggering several events from a joint string
        /// </summary>
        /// <param name="str"> string with multiple events </param>
        private void TriggerMultipleMessageEvents(string str)
        {
            var indexes = str.AllIndexesOf("EVENT");
            for (int i = 0; i < indexes.Count -1; i++)
            {
                TriggerMessageEvent(str.Substring(indexes[i], (indexes[i + 1]) - indexes[i]));
            }
            TriggerMessageEvent(str.Substring(indexes[indexes.Count - 1]));
        }

        private void TriggerMessageEvent(string str)
        {
            try
            {
                var strDividedBySpaces = str.Split(' ');
                var jsonStr = str.Substring(str.IndexOf('{'));
                MessageReceived(strDividedBySpaces[1], jsonStr);
            }
            catch (Exception)
            {

            }
        }

    }
}
