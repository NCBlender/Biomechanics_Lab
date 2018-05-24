using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public EasingFunctions easingFunctions;

    [Header("Hover over variable for tooltip")]
    public Transform endMarker;
    [Tooltip("Minimum time until camera randomly shifts position")]
    public float MinTimeUntilMove = 1.5f;
    [Tooltip("Maximum time until camera randomly shifts position")]
    public float MaxTimeUntilMove = 3f;
 
    [Range(0.0f, .5f)]
    public float SideSpeed = 1.0f;

    public float[] locations;
    public float[] MaxMinSpeeds;

    private float randomSpeed;

    

    private void Start()
    {
     
        Invoke("ChangePosition", 0.5f);
    }
    
    void Update()
    {
       
        float newXvalue = EasingFunctions.EaseInOutCubic (transform.position.x, endMarker.position.x, SideSpeed);
        transform.position = new Vector3 (newXvalue, transform.position.y, transform.position.z);
              
    }

  
    void ChangePosition()
    {

        randomSpeed = Random.Range(MaxMinSpeeds[0], MaxMinSpeeds[1]);

        int randomLocation = Random.Range(0, locations.Length);
        float currentPoint = locations[randomLocation];

        endMarker.position = new Vector3(currentPoint, transform.position.y, transform.position.z);

        float randomTime = Random.Range(MinTimeUntilMove, MaxTimeUntilMove);
        Invoke("ChangePosition", randomTime);
    }

}



