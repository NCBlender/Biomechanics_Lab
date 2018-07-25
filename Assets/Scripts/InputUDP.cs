using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;

public class InputUDP : MonoBehaviour
{
    Thread readThread;
    UdpClient client;
    public int port = 8888;
    public Text dataStream;
    public Text Txt1;
    public static double[] dataIn;
    public static double sideMotion;
    public static double height;
    public static double forwardSpeed;
    public static double other1;
    public static double other2;

    public bool spaceDelimited = true;
    public bool commaDelimited;
    public bool tabDelimited;

    private string message;

    // UDP packet store
    private string lastReceivedUDPPacket = "";
    private string allReceivedUDPPackets = ""; 

    void Start()
    {
        // create thread for reading UDP messages
        readThread = new Thread(new ThreadStart(ReceiveData));
        readThread.IsBackground = true;
        readThread.Start();
    }

    void Update()
    {
        if (spaceDelimited == true)
        {
            dataIn = System.Array.ConvertAll(message.Split(), double.Parse); //space delimited
        }
        if (commaDelimited == true)
        {
            dataIn = System.Array.ConvertAll(message.Split(','), double.Parse); //comma delimited
        }
        if (tabDelimited == true)
        {
            dataIn = System.Array.ConvertAll(message.Split('\t'), double.Parse); //tab delimited
        }
            
        sideMotion = dataIn[0];
        height = dataIn[1];
        forwardSpeed = dataIn[2];
        other1 = dataIn[3];
        other2 = dataIn[4];
        // show received message
        print(message);
        dataStream.text = ((sideMotion.ToString()) + "," + (height.ToString()) + "," + (forwardSpeed.ToString()));
        Txt1.text = "Space Delimited? " + spaceDelimited + "\nComma Delimited? " + commaDelimited + "\nTab Delimited? " + tabDelimited + "\nUDP Data: " + message;
    }

    // Unity Application Quit Function
    void OnApplicationQuit()
    {
        stopThread();
    }

    // Stop reading UDP messages
    private void stopThread()
    {
        if (readThread.IsAlive)
        {
            readThread.Abort();
        }
        client.Close();
    }

    // receive thread function
    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (true)
        {
            try
            {
                // receive bytes
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);

                // encode UTF8-coded bytes to text format
                message = Encoding.UTF8.GetString(data);

                // latest UDPpacket
                lastReceivedUDPPacket = message;

                // ....
                allReceivedUDPPackets = allReceivedUDPPackets + message;
            }

            catch (Exception err)
            {
                //print(err.ToString()); //This wil return "A non-blocking socket operation could not be completed immediately." which is normal behavior, from what I read
            }

            client.Client.Blocking = false;
        }
    }
    // getLatestUDPPacket
    // cleans up the rest
    public string getLatestUDPPacket()
    {
        allReceivedUDPPackets = "";
        return lastReceivedUDPPacket;
    }
    
}