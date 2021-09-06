using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
//using System.Threading.Tasks;

namespace SNA.Mobile.PickToLightClient
{
    public class TCPClient
    {
        /// <summary>
        /// Default Constants.
        /// </summary>
        public static IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        public static IPAddress DEFAULT_SERVER = ipHostInfo.AddressList[0];
        //public static IPAddress DEFAULT_SERVER = IPAddress.Parse("127.0.0.1");
        public static string sSTX = "\u0002";
        public static string sETX = "\u0003";
        //public static char cSTX = (char)2;
        //public static char cETX = (char)3;
        public static byte bSTX = 0x02;
        public static byte bETX = 0x03;
        public static byte[] baSTX = new byte[] { 0x02 };
        public static byte[] baETX = new byte[] { 0x03 };

        public static int DEFAULT_PORT = 11000; //31001;
        public static IPEndPoint DEFAULT_IP_END_POINT = new IPEndPoint(DEFAULT_SERVER, DEFAULT_PORT);
        Socket _client = null;

        private Logger _logger;
        bool _logNetworkTraffic = true;

        //// The response from the remote device.  
        //private static String response = String.Empty;

        /// <summary>
        /// Constructors.
        /// </summary>
        public TCPClient(Logger logger, bool logNetworkTraffic)
        {
            _logger = logger;
            Init(DEFAULT_IP_END_POINT);
            _logNetworkTraffic = logNetworkTraffic;
        }

        public TCPClient(Logger logger, IPAddress serverIP, bool logNetworkTraffic)
        {
            _logger = logger;
            Init(new IPEndPoint(serverIP, DEFAULT_PORT));
            _logNetworkTraffic = logNetworkTraffic;
        }

        public TCPClient(Logger logger, int port, bool logNetworkTraffic)
        {
            _logger = logger;
            Init(new IPEndPoint(DEFAULT_SERVER, port));
            _logNetworkTraffic = logNetworkTraffic;
        }

        public TCPClient(Logger logger, IPAddress serverIP, int port, bool logNetworkTraffic)
        {
            _logger = logger;
            Init(new IPEndPoint(serverIP, port));
            _logNetworkTraffic = logNetworkTraffic;
        }

        /// <summary>
        /// Init method that creates a client TCP Socket and connects to a remove server endpoint
        /// based on the IP Address and Port information that is passed in.
        /// </summary>
        /// <param name="ipNport"></param>
        private void Init(IPEndPoint serverIPandPort)
        {
            try
            {
                // Establish the remote endpoint for the socket.  
                //IPHostEntry ipHostInfo = Dns.Resolve("GET HOSTNAME");
                //IPAddress ipAddress = ipHostInfo.AddressList[0];
                //IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.  
                //Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                //client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                //connectDone.WaitOne();
                //Do this Synchronously, because we are in a dedicated thread.
                _client.Connect(serverIPandPort);
                if (_logNetworkTraffic)
                {
                    _logger.Log("TCPClient.Init() - Client Socket connected to Server Remote EndPoint " + _client.RemoteEndPoint.ToString() + "...");
                }
            }
            catch (ArgumentNullException ane)
            {
                _logger.LogError("TCPClient.Init() - ArgumentNullException : " + ane.Message.ToString());
            }
            catch (SocketException se)
            {
                _logger.LogError("TCPClient.Init() - SocketException : " + se.Message.ToString());
            }
            catch (Exception e)
            {
                _logger.LogError("TCPClient.Init() - Unexpected exception : " + e.Message.ToString());
            }
        }

        /// <summary>
        /// Sends a Command to the PickToLightServer via TCP and gets a Response to that Command.
        /// </summary>
        /// <param name="command">Command to send to PickToLightServer.</param>
        /// <param name="commandResponse">Response to the Command just sent.</param>
        public void SendCommand(string command, StringBuilder commandResponse)
        {
            try
            {
                if (_client == null)
                {
                    _logger.LogError("TCPClient.SendCommand() - The client socket hasn't been created!");
                    return;
                }

                // Send using the PickToLight Protocol (STX, ETX)
                Send(command);

                // Receive using the PickToLight Protocol (STX, ETX)
                Receive(commandResponse);

                // Release the socket.  
                _client.Shutdown(SocketShutdown.Both);
                _client.Close(); 

            }
            catch (ArgumentNullException ane)
            {
                _logger.LogError("TCPClient.SendAndReceive() - ArgumentNullException : " + ane.Message.ToString());
            }
            catch (SocketException se)
            {
                _logger.LogError("TCPClient.SendAndReceive() - SocketException : " + se.Message.ToString());
            }
            catch (Exception e)
            {
                _logger.LogError("TCPClient.SendAndReceive() - Unexpected exception : " + e.Message.ToString());
            }
        }

        /// <summary>
        /// Sends a Command to the PickToLightServer using the custom protocol ("<STX>Command Data<ETX>").
        /// </summary>
        /// <param name="command">The actual command data to send.</param>
        private void Send(String command)
        {
            try
            {

                //// Convert the string data to byte data using ASCII encoding.  
                //byte[] byteData = Encoding.ASCII.GetBytes(data);

                int bytesSent = 0;
                //byte[] msg = Encoding.ASCII.GetBytes(data);
                //bytesSent += _client.Send(Encoding.ASCII.GetBytes(sSTX));
                //msg = Encoding.ASCII.GetBytes(data);
                //bytesSent += _client.Send(msg);
                //bytesSent += _client.Send(Encoding.ASCII.GetBytes(sETX));
                byte[] msg = Encoding.ASCII.GetBytes(sSTX + command + sETX);
                bytesSent = _client.Send(msg);
                if (_logNetworkTraffic)
                {
                    _logger.Log("TCPClient.Send() - Sent " + bytesSent.ToString() + " bytes (" + command + ")");
                }
            }
            catch (ArgumentNullException ane)
            {
                _logger.LogError("TCPClient.Send() - ArgumentNullException : " + ane.Message.ToString());
            }
            catch (SocketException se)
            {
                _logger.LogError("TCPClient.Send() - SocketException : " + se.Message.ToString());
            }
            catch (Exception e)
            {
                _logger.LogError("TCPClient.Send() - Unexpected exception : " + e.Message.ToString());
            }
        }

        /// <summary>
        /// Receives the response to a Command that was sent to the PickToLightServer.
        /// The response is expected to be in the format of "<STX>Comand Response<ETX>".
        /// </summary>
        /// <param name="response">The response to the command that was sent to the PickToLightServer.</param>
        private void Receive(StringBuilder response) //TODO: Need to handle Receiving STX and ETX in TCPClient.Receive!!!! (and Send it back in TCPServer and all other places that send!)
        {
            // Data buffer for incoming data.  
            byte[] receiveBuffer = new byte[1024];

            try
            {
                // Receive the response from the remote device.  
                int bytesReceived = _client.Receive(receiveBuffer);

                //Check for valid STX, ETX!!!
                ParseCommandResponse(receiveBuffer, bytesReceived, response);
                if (_logNetworkTraffic)
                {
                    _logger.Log("TCPClient.Receive() - Command Response (" + response.ToString() + "), String Length = " + response.ToString().Length);
                }
                //response.Append(commandResponse);
            }
            catch (ArgumentNullException ane)
            {
                _logger.LogError("TCPClient.Receive() - ArgumentNullException : " + ane.Message.ToString());
            }
            catch (SocketException se)
            {
                _logger.LogError("TCPClient.Receive() - SocketException : " + se.Message.ToString());
            }
            catch (Exception e)
            {
                _logger.LogError("TCPClient.Receive() - Unexpected exception : " + e.Message.ToString());
            }
        }

        private void ParseCommandResponse(Byte[] byteBuffer, int size, StringBuilder response)
        {
            string data = Encoding.ASCII.GetString(byteBuffer, 0, size);
            int endIndex = 0;
            int startIndex = 0;
            StringBuilder processedBuffer = new StringBuilder();

            //
            // Make sure the Command Response is in the proper format "<STX>Response<ETX>".
            //
            // If received data from Server has more than one "STX/ETX", just use the first one, because this shouldn't happen.
            // I will probably add a "MessageID" or something for better error checking.
            do
            {
                endIndex = data.IndexOf(sETX);
                if (endIndex != -1)
                {
                    processedBuffer = processedBuffer.Append(data, 0, endIndex);
                    //Find where the "Response" starts, it will beging with the STX character.
                    startIndex = processedBuffer.ToString().IndexOf(sSTX);
                    if (startIndex != -1)
                    {
                        //STX found, so remove everything up to the STX
                        processedBuffer.Remove(0, startIndex + sSTX.Length);
                        // Process the command received (everything between STX and ETX).
                        response.Append(processedBuffer);
                        return; //Let's bail! We got the first response!
                    }
                    else
                    {
                        //Error, we got the ETX before the STX, LOG IT and clear the buffer after the ETX!!
                        _logger.LogError("ParseCommandResponse() - Invalid Data Format, received ETX, but no STX!\n\t data = (" + data + ")\n\t processedBuffer = (" + processedBuffer.ToString() + ")");
                        //If there is any data AFTER the ETX, copy it back to the main data buffer.
                        if (data.Length > endIndex + sETX.Length)
                        {
                            data = data.Substring(endIndex + sETX.Length, data.Length - endIndex - sETX.Length);
                        }
                        else
                        {
                            data = "";
                        }
                    }
                }
                else
                {
                    if (processedBuffer.ToString().IndexOf(sSTX) != -1)
                    {
                        //Error, no ETX!
                        _logger.LogError("ParseCommandResponse() - Invalid Data Format, no ETX!\n\t data = (" + data + ")\n\t processedBuffer = (" + processedBuffer.ToString() + ")");
                    }
                    else
                    {
                        //Error, no ETX!
                        _logger.LogError("ParseCommandResponse() - Invalid Data Format, no STX or ETX!\n\t data = (" + data + ")\n\t processedBuffer = (" + processedBuffer.ToString() + ")");
                    }
                }
            } while (endIndex != -1);

            //No (valid) response received!
            response.Append("");
        }
    }
}
