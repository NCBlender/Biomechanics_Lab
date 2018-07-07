using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    private float forwardSpeed;
	
	// Update is called once per frame
	void Update ()
    {
        forwardSpeed = Network_Client.forwardSpeed;
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
    }
}
