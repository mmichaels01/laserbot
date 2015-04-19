using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowBowlBall : MonoBehaviour
{

    public GameObject ball;
    List<GameObject> pins = new List<GameObject>();

	void Start () {
        //Vector3 

        float pinHeight = 20;
        float pinWidth = 7;
        pins.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        pins[pins.Count - 1].transform.position = new Vector3(-450,0.5f,70);
        pins[pins.Count - 1].transform.localScale = new Vector3(pinWidth, pinHeight, pinWidth);
        pins[pins.Count - 1].AddComponent<Pin>();
	}
	
	void FixedUpdate () {
        if (ball.transform.position.x < 0)
        {
            transform.LookAt(ball.transform.position);
            transform.position = new Vector3(ball.transform.position.x - 30, transform.position.y, transform.position.z);
        }
	}
}