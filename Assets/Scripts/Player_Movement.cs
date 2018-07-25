using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    private double forwardSpeed;
	
	// Update is called once per frame
	void Update ()
    {
        forwardSpeed = InputUDP.forwardSpeed;
        transform.Translate(Vector3.forward * Time.deltaTime * (float)forwardSpeed);
    }
}
