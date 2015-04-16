using UnityEngine;
using System.Collections;

public class BreakBall : MonoBehaviour {

	Rigidbody rb;

	float lastX;
	float lastZ;

	int speed = 105;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(speed, 0, RndZ());
		lastX = rb.velocity.x;
		lastZ = rb.velocity.z;
	}

	void Update ()
	{
		WallCollision();
		HandCollision();
	}

	void WallCollision()
	{
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
		if (transform.position.x < 2.5f)
		{
			transform.position = new Vector3(2.5f, transform.position.y, transform.position.z);
			rb.velocity = new Vector3(-lastX, 0f, rb.velocity.z);
			lastX = -lastX;
		}
		if (transform.position.x > 157.5f)
		{
			transform.position = new Vector3(157.5f, transform.position.y, transform.position.z);
			rb.velocity = new Vector3(-lastX, 0f, rb.velocity.z);
			lastX = -lastX;
		}
	}

	int RndZ()
	{
		int num = Random.Range (0, 50);
		if (Random.Range (0, 100) % 2 == 0)
			num = -num;
		return num;
	}
	
	void HandCollision()
	{
		if (rb.velocity.x < speed - 10 && rb.velocity.x > 2 && lastX > 0) 
			rb.velocity = new Vector3 (-speed, 0f, rb.velocity.z);
		else if (rb.velocity.x > -speed+10 && rb.velocity.x < -2 && lastX < 0)
			rb.velocity = new Vector3 (speed, 0f, rb.velocity.z);
		else if (rb.velocity.x <= 2 && rb.velocity.x >= 0)
			rb.velocity = new Vector3(-speed, 0f, rb.velocity.z);
		else if (rb.velocity.x >= -2 && rb.velocity.x <= 0)
			rb.velocity = new Vector3(speed, 0f, rb.velocity.z);
	}
}
