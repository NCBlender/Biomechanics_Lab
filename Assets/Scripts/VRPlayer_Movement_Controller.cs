//THIS IS USED FOR THE PERIPHERAL DEVICE, IF NEEDED. Probably just use another program to send info to the port.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class VRPlayer_Movement_Controller : MonoBehaviour 
{
    [Range (0.1f,1.0f)]
    public float sideToSideSpeed = 0.71f;
    [Range(0.1f, 0.5f)]
    public float sideDist = 0.3f;
    [Range(0.0f, 2.0f)]
    public float height = 1;
    [Range(0f, 2f)]
    public float forwardSpeed = .75f;
    [Range(0.25f, 2f)]
    public float VRscale = 1.0f;

    private float position;



  

    void Update()
    {
        PositionChange();
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
        transform.position = new Vector3(position, height, transform.position.z);
        transform.localScale = new Vector3(VRscale, VRscale, VRscale);
    }

 

    public void PositionChange()
    {
        position = (((Mathf.Sin((float)((Time.time) / (0.8 * sideToSideSpeed)))) + (Mathf.Sin((float)((Time.time) / (0.5 * sideToSideSpeed))))) *sideDist);

    }

}