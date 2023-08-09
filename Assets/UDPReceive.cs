using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDPReceive : MonoBehaviour
{
   Thread receiveThread;
   UdpClient client;
   public int port = 5052;
   public bool startReceiving = true;
   public bool printToConsole = false;
   public string data;

    void Start ()
    {
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void ReceiveData()
    {
        client = new UdpClient(port);
        while(startReceiving)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataByte = client.Receive(ref anyIP);
                data = Encoding.UTF8.GetString(dataByte);

                if(printToConsole)
                {
                    print(data);
                }
            }
            catch(Exception err)
            {
                print(err.ToString());    
            }
        }
    }
}