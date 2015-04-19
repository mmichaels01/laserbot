using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowBowlBall : MonoBehaviour
{

    public GameObject ball;
    public GameObject pinPrefab;

    List<GameObject> pins = new List<GameObject>();
    PinPositions pinPos = new PinPositions();

	void Start () {
        for (int i = 0; i < pinPos.pinPositions.Length; i++)
            pins.Add(Instantiate(pinPrefab, pinPos.pinPositions[i], Quaternion.identity) as GameObject);
	}
	
	void FixedUpdate () {
        if (ball.transform.position.x < 0 && ball.transform.position.x > -400f)
        {
            transform.LookAt(ball.transform.position);
            transform.position = new Vector3(ball.transform.position.x + 30, ball.transform.position.y + 50, transform.position.z);
        }
        else if (ball.transform.position.x < -400f)
        {
            transform.position = new Vector3(-450f, 200f, 60f);
            transform.LookAt(new Vector3(-450f, 20f, 60f));

        }
	}
}