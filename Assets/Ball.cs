using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ball : MonoBehaviour {
    Rigidbody rb;
    float lastX;
    float lastZ;

	int speed = 75;

	float p1Score = 0;
	float p2Score = 0;

	bool GAMEOVER = false;
	int delay = 0;

	Text p1Text;
	Text p2Text;
	Text extraText;

	Vector3 prevVelocity;

	int intro = 1;

	void Start () {
        rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(speed, 0, RndZ());
        lastX = rb.velocity.x;
        lastZ = rb.velocity.z;
		p1Text = GameObject.Find("p1Score").GetComponent<Text>();
		p2Text = GameObject.Find("p2Score").GetComponent<Text>();
		extraText = GameObject.Find("ExtraText").GetComponent<Text>();
		prevVelocity = rb.velocity;
	}

    void FixedUpdate()
    {
		if (intro == 0)
		{
			if (GAMEOVER == false)
			{
				if (delay == 0)
				{
					WallCollision();
					HandCollision();
					Scoring();
				}
				else
					Delay();

				prevVelocity = rb.velocity;
			}
		}
		else
		{
			intro++;
			if (intro != 50)
				intro = 0;
		}
    }

	int RndZ()
	{
		int num = Random.Range (0, 50);
		if (Random.Range (0, 100) % 2 == 0)
			num = -num;
		return num;
	}

	void Delay()
	{
		delay++;
		print (transform.position);
		if (delay == 120)
		{
			if (p1Score + p2Score == 1)
				rb.velocity = new Vector3(-speed, 0, RndZ());
			else
				rb.velocity = new Vector3(speed, 0, RndZ());
			delay = 0;
			extraText.text = "";
		}
	}

	void Scoring()
	{
		if (transform.position.x < 0 - 5)
		{
			rb.position = new Vector3(80, 0.5f, 60);
			rb.velocity = new Vector3(0,0,0);
			p2Score++;
			p2Text.text = "Player Two: " + p2Score;
			if (p2Score >= 2)
			{
				extraText.text = "Player 2 Wins!";
				//GAMEOVER = true;
			}
			else
			{
				extraText.text = "Player 2 Scored!";
				delay = 1;
			}
		}
		else if (transform.position.x > 160 + 5)
		{
			rb.position = new Vector3(80, 0.5f, 60);
			rb.velocity = new Vector3(0,0,0);
			p1Score++;
			p1Text.text = "Player One: " + p1Score;
			if (p1Score >= 2)
			{
				extraText.text = "Player 1 Wins!";
				//GAMEOVER = true;
			}
			else
			{
				extraText.text = "Player 1 Scored!";
				delay = 1;
			}
		}
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
	}

	void HandCollision()
	{
		if (rb.velocity.x < speed - 10 && rb.velocity.x > 2 && prevVelocity.x > 0) 
		{
			rb.velocity = new Vector3 (-speed, 0f, rb.velocity.z);
			lastX = -speed;
		}
		else if (rb.velocity.x > -speed+10 && rb.velocity.x < -2 && prevVelocity.x < 0)
		{
			rb.velocity = new Vector3 (speed, 0f, rb.velocity.z);
			lastX = speed;
		}
		else if (rb.velocity.x <= 2 && rb.velocity.x >= 0)
			rb.velocity = new Vector3(-speed, 0f, rb.velocity.z);
		else if (rb.velocity.x >= -2 && rb.velocity.x <= 0)
			rb.velocity = new Vector3(speed, 0f, rb.velocity.z);
		//if (rb.velocity.x < 79.0f && rb.velocity.x > -79.0f )
		//{
		//	rb.velocity = new Vector3(-lastX,0f,rb.velocity.z);
		//	lastX = -lastX;
		//}
	}
}