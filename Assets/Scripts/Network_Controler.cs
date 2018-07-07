//THIS IS USED FOR THE PERIPHERAL DEVICE, IF NEEDED. Probably just use another program to send info to the port.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Network_Device_Test : MonoBehaviour 
{

    public Text Txt1;
    [Range (0, 10)]
    public float speed = 1;
    [Range(0, 5)]
    public float sideDist = 1;

    public int myReliableChannelId;
    public int socketId;
    public int socketPort = 8888;
    public int connectionId;

    private float position;

    void Start()
    {

        NetworkTransport.Init();
        Connect();

    }

    public void Connect()
    {
        ConnectionConfig config = new ConnectionConfig();
        int maxConnections = 5;
        config.AddChannel(QosType.UnreliableSequenced);
        HostTopology topology = new HostTopology(config, maxConnections);
        socketId = NetworkTransport.AddHost(topology, socketPort);
        Debug.Log("Socket Open. SocketId is: " + socketId);
        byte error;
        connectionId = NetworkTransport.Connect(socketId, "192.168.0.40", socketPort, 0, out error);
        //Txt1.text = Txt1.text + "SockId: " + socketId + "| ConnId: " + connectionId + "-" + error.ToString();
    }

    void Update()
    {
        //Customer reciept
        int recChannelId;
        byte[] recBuffer = new byte[1024];
        int bufferSize = 1024;
        int dataSize;
        byte error;
        NetworkEventType networkEvent = NetworkTransport.Receive(out socketId, out connectionId, out recChannelId, recBuffer, bufferSize, out dataSize, out error);
        switch (networkEvent)
        {
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.ConnectEvent:
                Txt1.text = Txt1.text + "connecting";
                break;
            case NetworkEventType.DataEvent:
                Stream stream = new MemoryStream(recBuffer);
                BinaryFormatter formatter = new BinaryFormatter();
                string message = formatter.Deserialize(stream) as string;
                Txt1.text = Txt1.text + "message= " +message;
                break;
            case NetworkEventType.DisconnectEvent:
                Txt1.text = "Disconnected";
                break;
        }

        PositionChange();
        SendSocketMessage();

    }

    public void SendSocketMessage()
    {
        byte error;
        byte[] buffer = new byte[1024];
        Stream stream = new MemoryStream(buffer);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, (position.ToString()));

        int bufferSize = 1024;

        NetworkTransport.Send(socketId, connectionId, myReliableChannelId, buffer, bufferSize, out error);
        // Txt1.text = socketId.ToString() + connectionId.ToString() + myReliableChannelId.ToString() + buffer.ToString() + bufferSize.ToString() + error.ToString();

    }

    public void PositionChange()
    {
        position = Mathf.Sin(Time.time * speed) * sideDist;

        Debug.Log(position.ToString());
    }
}