using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

    GameObject obj;
	// Use this for initialization
	void Start () {
        obj = GameObject.Find("Ball");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = obj.transform.position;
        if (pos.x < 10)
        {
            transform.position = (new Vector3(pos.x, transform.position.y, pos.z));
        }
	}
}
