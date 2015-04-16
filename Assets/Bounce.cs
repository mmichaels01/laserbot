using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {
    Rigidbody rb;
    float lastX;
    float lastZ;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(Random.Range(60f, 80f), 0f, Random.Range(60f, 80f));
	}

 
}
