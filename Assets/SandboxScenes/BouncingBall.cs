using UnityEngine;
using System.Collections;

public class BouncingBall : MonoBehaviour {

    Rigidbody rb;

    int rndLow = 40;
    int rndHigh = 40;
    Vector3 prevPos;

	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(Random.Range(rndLow, rndHigh), 0, Random.Range(rndLow, rndHigh));
	}
	
	void FixedUpdate()
    {
        if (rb.velocity.x > rndHigh)
                rb.velocity = new Vector3(rndHigh, 0, rb.velocity.z);
        if (rb.velocity.x < -rndHigh)
            rb.velocity = new Vector3(-rndHigh, 0, rb.velocity.z);
        if (rb.velocity.z > rndHigh)
                rb.velocity = new Vector3(rb.velocity.x, 0, rndHigh);
        if (rb.velocity.z < -rndHigh)
            rb.velocity = new Vector3(rb.velocity.x, 0, -rndHigh);

        if (rb.velocity.x < rndLow && rb.velocity.x > 0)
            rb.velocity = new Vector3(rndLow, 0, rb.velocity.z);
        if (rb.velocity.x > -rndLow && rb.velocity.x < 0)
            rb.velocity = new Vector3(-rndLow, 0, rb.velocity.z);
        if (rb.velocity.z < rndLow && rb.velocity.z > 0)
            rb.velocity = new Vector3(rb.velocity.x, 0, rndLow);
        if (rb.velocity.z > -rndLow && rb.velocity.z < 0)
            rb.velocity = new Vector3(rb.velocity.x, 0, -rndLow);

        prevPos = transform.position;
	}

    int RndNum()
    {
        return Random.Range(rndHigh-1, rndHigh);
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "VerticalBumper")
        {
            print("VerticalBumper");
            if (rb.velocity.z < 0)
                rb.velocity = new Vector3(rb.velocity.x, 0, RndNum());
            else
                rb.velocity = new Vector3(rb.velocity.x, 0, -RndNum());

            transform.position = new Vector3(transform.position.x, 0.5f, prevPos.z);

        }
        if (obj.tag == "Bumper")
        {
            print("Bumper");

            if (rb.velocity.x < 0)
                rb.velocity = new Vector3(RndNum(), 0, rb.velocity.z);
            else
                rb.velocity = new Vector3(-RndNum(), 0, rb.velocity.z);

            transform.position = new Vector3(prevPos.x, 0.5f, transform.position.z);
        }
    }
}