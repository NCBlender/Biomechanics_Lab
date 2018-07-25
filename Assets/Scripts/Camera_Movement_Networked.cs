using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement_Networked : MonoBehaviour {

    private double sideMotion;
    private double height;

    // Update is called once per frame
    void Update () {

        sideMotion = InputUDP.sideMotion;
        height = InputUDP.height;

        transform.position = new Vector3((float)sideMotion, (float)height, transform.position.z);
    }
}
