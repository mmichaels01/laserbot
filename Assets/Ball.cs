using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    Rigidbody rb;
    float lastX;
    float lastZ;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(80f, 0f, 0f);
        lastX = rb.velocity.x;
        lastZ = rb.velocity.z;
	}

    void FixedUpdate()
    {
        if (rb.velocity.x < 79.0f && rb.velocity.x > -79.0f )
        {
            rb.velocity = new Vector3(-lastX,0f,rb.velocity.z);
            lastX = -lastX;
        }
        if (transform.position.z > 117.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 117.5f);

            rb.velocity = new Vector3(rb.velocity.x, 0f, -lastZ);
            lastZ = -lastZ;

        }
        if (transform.position.z < 2.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 2.5f);
            rb.velocity = new Vector3(rb.velocity.x, 0f, -lastZ);
            lastZ = -lastZ;

        }
    }
}
