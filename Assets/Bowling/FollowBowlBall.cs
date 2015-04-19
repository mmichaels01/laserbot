using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowBowlBall : MonoBehaviour
{

    public GameObject ball;
    List<GameObject> pins = new List<GameObject>();
    PinPositions pinPos = new PinPositions();

	void Start () {

        float pinHeight = 30;
        float pinWidth = 12;

        for (int i = 0; i < pinPos.pinPositions.Length; i++)
        {
            pins.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
            pins[pins.Count - 1].transform.position = pinPos.pinPositions[i];
            pins[pins.Count - 1].transform.localScale = new Vector3(pinWidth, pinHeight, pinWidth);
            pins[pins.Count - 1].AddComponent<Pin>();
        }
	}
	
	void FixedUpdate () {
        if (ball.transform.position.x < 0)
        {
            transform.LookAt(ball.transform.position);
            transform.position = new Vector3(ball.transform.position.x - 30, transform.position.y, transform.position.z);
        }
	}
}