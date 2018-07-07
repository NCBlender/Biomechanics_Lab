using UnityEngine;
using System;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Collections.Generic;
using System.Text;

public class Server : MonoBehaviour
{
    Socket SeverSocket = null;
    Thread Socket_Thread = null;
    bool Socket_Thread_Flag = false;

    //for received message
    //    private float mouse_delta_x;
    //    private float mouse_delta_y;
    //    private bool isTapped;
    //    private bool isDoubleTapped;
    //
    //    public float getMouseDeltaX(){return mouse_delta_x;    }
    //    public float getMouseDeltaY(){return mouse_delta_y;    }
    //    public bool getTapped(){return isTapped;}
    //    public bool getDoubleTapped(){return isDoubleTapped;}
    //
    //    public void setMouseDeltaX(float dx){mouse_delta_x = dx;}
    //    public void setMouseDeltaY(float dy){mouse_delta_y = dy;}
    //    public void setTapped(bool t){isTapped = t;}
    //    public void setDoubleTapped(bool t){isDoubleTapped = t;}
    //
    //    private int tick =0;
    //private string[] receivedMSG;
    //public string[] getMsg(){return receivedMSG;    }


    string[] stringSeparators = new string[] { "*TOUCHEND*", "*MOUSEDELTA*", "*Tapped*", "*DoubleTapped*" };

    void Awake()
    {
        Socket_Thread = new Thread(Dowrk);
        Socket_Thread_Flag = true;
        Socket_Thread.Start();
    }

    private void Dowrk()
    {
        //receivedMSG = new string[10];
        SeverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9999);
        SeverSocket.Bind(ipep);
        SeverSocket.Listen(10);

        Debug.Log("Socket Standby....");
        Socket client = SeverSocket.Accept();//client에서 수신을 요청하면 접속합니다.
        Debug.Log("Socket Connected.");

        IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;
        NetworkStream recvStm = new NetworkStream(client);
        //tick = 0;

        while (Socket_Thread_Flag)
        {
            byte[] receiveBuffer = new byte[1024 * 80];
            try
            {

                //print (recvStm.Read(receiveBuffer, 0, receiveBuffer.Length));
                if (recvStm.Read(receiveBuffer, 0, receiveBuffer.Length) == 0)
                {
                    // when disconnected , wait for new connection.
                    client.Close();
                    SeverSocket.Close();

                    SeverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    ipep = new IPEndPoint(IPAddress.Any, 10000);
                    SeverSocket.Bind(ipep);
                    SeverSocket.Listen(10);
                    Debug.Log("Socket Standby....");
                    client = SeverSocket.Accept();//client에서 수신을 요청하면 접속합니다.
                    Debug.Log("Socket Connected.");

                    clientep = (IPEndPoint)client.RemoteEndPoint;
                    recvStm = new NetworkStream(client);

                }
                else
                {


                    string Test = Encoding.Default.GetString(receiveBuffer);
                    //string Test = Convert.ToBase64String(receiveBuffer);
                    //Test = Test.Normalize();


                    print(Test);
                    //string[] splitMsg = Test.Split(stringSeparators,System.StringSplitOptions.RemoveEmptyEntries);
                    // parsing gogo

                    //                    string[] splitMsg = Test.Split('*');
                    ////                    print (splitMsg);
                    //                    if(splitMsg.Length>1)
                    //                    {
                    //                        if(splitMsg[1].CompareTo("Tapped")==0){
                    //                            print ("tap");
                    //                            isTapped = true;
                    //                        }else if(splitMsg[1].CompareTo("DoubleTapped")==0){
                    //                            print ("double tap");
                    //                            isDoubleTapped = true;
                    //                        }else if(splitMsg[1].CompareTo("MOUSEDELTA")==0){
                    //                            print ("move");
                    //                            //string[] lastMsg = splitMsg[splitMsg.Length-1].Split('*');
                    //                            mouse_delta_x = (float)Convert.ToDouble(splitMsg[2]);
                    //                            mouse_delta_y = (float)Convert.ToDouble(splitMsg[3]);
                    //                        }else{
                    //                            print ("F*** :"+splitMsg[1].Length);
                    //                          
                    //                        }
                    //                    }
                    //
                    //                    string singletap = "one";
                    //                    string doubletap = "two";
                    //                    if(splitMsg.Length>0){
                    //
                    //

                    //                          
                    //                        if(lastMsg.Length>1){
                    //                      

                    //
                    //                        }else{
                    //
                    //                            print ("split msg : "+splitMsg[0]);
                    //                            int tmp = (int)Convert.ToInt32(splitMsg[0]);
                    //                            if(tmp ==1){
                    //
                    //                                print ("Tapped!~");
                    //                          
                    //                                isTapped = true;
                    //
                    //                            }else if(tmp ==2){
                    //
                    //                                print ("Double Tapped!~");
                    //                          
                    //                                isDoubleTapped = true;
                    //                          
                    //                            }else{              
                    //
                    //                            }
                    //                        }
                    //                    }else{
                    //
                    //                    }

                    //print (receivedMSG);

                }


            }

            catch (Exception e)
            {
                Socket_Thread_Flag = false;
                client.Close();
                SeverSocket.Close();
                continue;
            }

        }

    }

    void OnApplicationQuit()
    {
        try
        {
            Socket_Thread_Flag = false;
            Socket_Thread.Abort();
            SeverSocket.Close();
            Debug.Log("Bye~~");
        }

        catch
        {
            Debug.Log("Error when finished...");
        }
    }


}


