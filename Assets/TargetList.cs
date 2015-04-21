using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetList : MonoBehaviour {

    public List<Vector3> positions;
    public GameObject bot;
    public float distance;
    int position;

	// Use this for initialization
	void Start () {
        position = 0;
        transform.position = positions[position];
	}

    void FixedUpdate()
    {
        if (Vector3.Distance(positions[position], bot.transform.position) < distance && position < positions.Count - 1)
        {
            position++;
            transform.position = positions[position];
            bot.SendMessage("IssueCommand", "mp3,r2_surprise");
        }


    }
}
