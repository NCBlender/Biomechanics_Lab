//THIS IS USED FOR THE COMPUTER RUNNING UNITY

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Network_Client : MonoBehaviour
{
    public int port = 8888;
    public Text Txt1;
    public Text dataStream;

    public static float[] dataIn;
    public static float sideMotion;
    public static float height;
    public static float forwardSpeed;

    public static string message;

    private HostTopology topology;
    private ConnectionConfig config;
    private int recConnectionId;
    private int hostId;
    private int recChannelId;

    void Start()
    {
        NetworkTransport.Init();
        config = new ConnectionConfig();

        config.AddChannel(QosType.UnreliableSequenced);
        topology = new HostTopology(config, 5);
        hostId = NetworkTransport.AddHost(topology, port);
        byte error;
        recConnectionId = NetworkTransport.Connect(hostId, "192.168.0.40", port, 0, out error); //name of server

       

    }

    void Update()
    {

       

        byte[] recBuffer = new byte[1024];
        int bufferSize = 1024;
        int dataSize;
        byte error;
        NetworkEventType networkEvent = NetworkTransport.Receive(out hostId, out recConnectionId, out recChannelId, recBuffer, bufferSize, out dataSize, out error);
        //Txt1.text = "IdS = " + hostId + " IdC = " + recConnectionId;
        NetworkError networkError = (NetworkError)error;
        //Txt1.text = hostId +"-" + recConnectionId + "-" + recChannelId.ToString() + "-" + recBuffer.ToString() + "-" + bufferSize.ToString() + "-" + dataSize.ToString() + "-" + "Error:" + "-" + networkError;
        if (networkError != NetworkError.Ok)
        {
            //Txt1.text = ">" + hostId + recConnectionId.ToString() + "-" + recChannelId.ToString() + "-" + recBuffer.ToString() + "-" + bufferSize.ToString() + "-" + dataSize.ToString() + "-" + "Error:" + "-" + networkError;
        }

        switch (networkEvent)
        {
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.ConnectEvent:
                Txt1.text = "connecting";
                break;
            case NetworkEventType.DataEvent:
                Stream stream = new MemoryStream(recBuffer);
                BinaryFormatter formatter = new BinaryFormatter();
                message = formatter.Deserialize(stream) as string;
                Txt1.text = message;
                break;
            case NetworkEventType.DisconnectEvent:
                Txt1.text = "Disconnected";
                break;
        }

        dataIn = System.Array.ConvertAll(message.Split(','), float.Parse);
        sideMotion = dataIn[0];
        height = dataIn[1];
        forwardSpeed = dataIn[2];

        dataStream.text = ((sideMotion.ToString()) + "," + (height.ToString()) + "," + (forwardSpeed.ToString()));
        
    }

    public void SendSocketMessage()
    {
        byte error;
        byte[] buffer = new byte[1024];
        Stream stream = new MemoryStream(buffer);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, "Hello Client");

        int bufferSize = 1024;

        NetworkTransport.Send(hostId, recConnectionId, recChannelId, buffer, bufferSize, out error);

    }


}