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
        if (ball.transform.position.x < 0)
        {
            transform.LookAt(ball.transform.position);
            transform.position = new Vector3(ball.transform.position.x - 30, transform.position.y, transform.position.z);
        }
	}
}