 

using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
 
public class UDPSend : MonoBehaviour
{
    private static int localPort;

    // prefs
    private string IP;  // define in init
    public int port;  // define in init

    // "connection" things
    IPEndPoint remoteEndPoint;
    UdpClient client;

    // gui
    string strMessage = "";


    public void Update()
    {
        //sendString("0,1,1,1,1"); //comma test
        //sendString("0\t1\t1\t1\t1"); //tab test
        sendString("0 1 1 1 1"); //space test
    }

    // call it from shell (as program)
    private static void Main()
    {
        UDPSend sendObj = new UDPSend();
        sendObj.init();

        // testing via console
        // sendObj.inputFromConsole();

        // as server sending endless
        sendObj.sendEndless(" endless infos \n");

    }
    // start from unity3d
    public void Start()
    {
        init();
    }

    
           
        
    

    // init
    public void init()
    {
       
        print("UDPSend.init()");

        // define
        IP = "127.0.0.1";
        port = 8888;

        // ----------------------------
        // Send
        // ----------------------------
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        client = new UdpClient();

        // status
        print("Sending to " + IP + " : " + port);
        

    }

    // inputFromConsole
    private void inputFromConsole()
    {
        try
        {
            string text;
            do
            {
                text = Console.ReadLine();

                
                if (text != "")
                {

                    byte[] data = Encoding.UTF8.GetBytes(text);

                    // Den Text zum Remote-Client senden.
                    client.Send(data, data.Length, remoteEndPoint);
                }
            } while (text != "");
        }
        catch (Exception err)
        {
            print(err.ToString());
        }

    }

    // sendData
    private void sendString(string message)
    {
        try
        {
            //if (message != "")
            //{

            // Daten mit der UTF8-Kodierung in das Binärformat kodieren.
            byte[] data = Encoding.UTF8.GetBytes(message);

            // Den message zum Remote-Client senden.
            client.Send(data, data.Length, remoteEndPoint);
            //}
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }


    // endless test
    private void sendEndless(string testStr)
    {
        do
        {
            sendString(testStr);


        }
        while (true);

    }

}

