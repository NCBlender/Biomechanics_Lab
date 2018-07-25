using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;


//using UnityEngine.Networking;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Threading;





public class UDP_Receive : MonoBehaviour
{
  
    public int IpPort = 8888;
      

    public Text Txt1;
    public Text dataStream;

    public static double[] dataIn;
    public static double sideMotion;
    public static double height;
    public static double forwardSpeed;

    public static string message;
    private int recConnectionId;
    private int hostId;
    private int recChannelId;
    private string returnData;
    private byte[] receiveBytes;

    public void Update()
    {
        receiveStatus();
        //Debug.Log();
    }

    private void receiveStatus()
    {
    
         UdpClient client = new UdpClient(IpPort);
         IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            try
        {
            if (client.Available > 0)
            {
                receiveBytes = client.Receive(ref RemoteIpEndPoint);
                String returnData = Encoding.ASCII.GetString(receiveBytes);
                Debug.Log("something");
                client.Close();
            }else
            {
                Debug.Log("NO DATA AVAILABLE");
            }
        }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }

        client.Close();

    }

       
        //dataIn = System.Array.ConvertAll(returnData.Split(','), double.Parse);
       // sideMotion = dataIn[0];
       // height = dataIn[1];
       // forwardSpeed = dataIn[2];

        //dataStream.text = ((sideMotion.ToString()) + "," + (height.ToString()) + "," + (forwardSpeed.ToString()));

        
    }
