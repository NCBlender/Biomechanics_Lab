using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwaySpawner : MonoBehaviour {

	public GameObject[] lanePrefabs;
    public float laneSpawnDistance = 20f;
    private int offset = 0;
    public GameObject player;

    //public GameObject CurvesObject;
   // private CurveController curveController;
    //private float roadAngle;
   //public float LerpSmooth = 5f;
    //public int WaitTime = 5;


    private void Start()
    {
        //curveController = CurvesObject.GetComponent<CurveController>();
        //InvokeRepeating("ChangeCurve", 2.0f, 15);

    }

    // Use this for initialization
    void Update () {

        
        //curveController.x = Mathf.Lerp(curveController.x, roadAngle, Time.deltaTime * LerpSmooth);

        while (offset < laneSpawnDistance + player.transform.position.z)
        {
            CreateRandomLane(offset);
            offset += 5;
            
        }
        foreach (Transform laneTransform in this.transform)
        {
            if (laneTransform.position.z + laneSpawnDistance < player.transform.position.z)
            {
                Destroy(laneTransform.gameObject);
            }
        }
     }
     

	void CreateRandomLane(float offset){
		int laneIndex = Random.Range(0, lanePrefabs.Length);
		var lane = Instantiate(lanePrefabs[laneIndex]);
        lane.transform.SetParent(transform, false);
        lane.transform.Translate(0,0, offset);
	}

   

    //void ChangeCurve() {
    //    roadAngle = Mathf.Sin(Time.fixedTime);
    //    roadAngle *= 125;
    //    Debug.Log(roadAngle);
    //}
	
}
