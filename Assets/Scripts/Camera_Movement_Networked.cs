using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement_Networked : MonoBehaviour {

    private float sideMotion;
    private float height;

    // Update is called once per frame
    void Update () {

        sideMotion = Network_Client.sideMotion;
        height = Network_Client.height;

        transform.position = new Vector3(sideMotion, height, transform.position.z);
    }
}
