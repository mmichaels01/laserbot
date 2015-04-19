using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowBowlBall : MonoBehaviour
{

    GameObject ball;
    List<GameObject> pins = new List<GameObject>();

	void Start () {
        ball = GameObject.Find("Ball");

        float pinHeight = 10;
        float pinWidth = 5;
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