using System;
using System.Net.Sockets;
using Physalis.Utils;

namespace Start
{
	/// <summary>
	/// TraceOutput is a specialized TraceOutput that sends the output
	/// of <c>Debug.Write</c> or <c>Debug.WriteLine</c> to a server using
	/// UDP/IP. This class is intended to be used with the TraceViewer VS.Net Add-In.
	/// </summary>
    public class TraceOutput : ITracesOutput
    {
        private TcpClient client = null;
        
        public TraceOutput(string server, int port)
        {
			client = new TcpClient(server, port);
		}

        public void OutputTrace(string trace)
        {
            NetworkStream stream = client.GetStream();
            Byte[] data = System.Text.Encoding.Unicode.GetBytes(trace);
            stream.Write(data, 0, data.Length);
        }
    }
}
