using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AirBall : MonoBehaviour {
    public int MAX_SCORE;
    Rigidbody rb;
    float lastX;
    float lastZ;

    public int speed;

	float p1Score = 0;
	float p2Score = 0;

	bool GAMEOVER = false;
	int delay = 0;

	Text p1Text;
	Text p2Text;
	Text extraText;

	int intro = 1;

    public float left;
    public float right;
    public float top;
    public float bottom;

    public AudioSource bounce;
    public AudioSource score;

    float lastCollided;

    Vector3 prevPos;

	void Start () {
        rb = GetComponent<Rigidbody>();
        lastX = rb.velocity.x;
        lastZ = rb.velocity.z;
		p1Text = GameObject.Find("p1Score").GetComponent<Text>();
		p2Text = GameObject.Find("p2Score").GetComponent<Text>();
		extraText = GameObject.Find("ExtraText").GetComponent<Text>();
        lastCollided = Time.time;
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
					Scoring();
				}
				else
					Delay();

				lastX = rb.velocity.x;
                lastZ = rb.velocity.z;
			}
		}
		else
		{
			intro++;
			if (intro > 120)
			{
				intro = 0;
				extraText.text = "";
				rb.velocity = new Vector3(speed, 0, RndZ());
			}
		}

        prevPos = rb.position;
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
		if (transform.position.x < left)
		{
            score.Play();

			rb.position = new Vector3(80, 0.5f, 60);
			rb.velocity = new Vector3(0,0,0);
			p2Score++;
			p2Text.text = "Player Two: " + p2Score;
			if (p2Score >= MAX_SCORE)
			{
				extraText.text = "Player 2 Wins!";
				GAMEOVER = true;
			}
			else
			{
				extraText.text = "Player 2 Scored!";
				delay = 1;
			}
		}
		else if (transform.position.x > right)
		{
            score.Play();

			rb.position = new Vector3(80, 0.5f, 60);
			rb.velocity = new Vector3(0,0,0);
			p1Score++;
			p1Text.text = "Player One: " + p1Score;
			if (p1Score >= MAX_SCORE)
			{
				extraText.text = "Player 1 Wins!";
				GAMEOVER = true;
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
		if (transform.position.z > top - 2.5)
		{
            transform.position = new Vector3(transform.position.x, transform.position.y, top - 2.5f);
			rb.velocity = new Vector3(rb.velocity.x, 0f, -lastZ);
			lastZ = -lastZ;
            print("Z: " + lastZ);
		}
        if (transform.position.z < bottom + 2.5)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, bottom + 2.5f);
			rb.velocity = new Vector3(rb.velocity.x, 0f, -lastZ);
			lastZ = -lastZ;
		}
	}

    void OnTriggerEnter(Collider obj)
    {
        print(obj.tag);

        if (Time.time - lastCollided > .1)
        {
            if (obj.tag == "BlueBot")// && obj.transform.position.x < transform.position.x)
                rb.velocity = new Vector3(speed, 0, rb.velocity.z);
            if (obj.tag == "RedBot")// && obj.transform.position.x > transform.position.x)
                rb.velocity = new Vector3(-speed, 0, rb.velocity.z);

            lastCollided = Time.time;

            rb.position = prevPos;
            transform.position = prevPos;

            bounce.Play();


        }
        //if (obj.tag == "Arena")
        //{
        //    rb.velocity = new Vector3(-rb.velocity.x, 0, rb.velocity.z)
        //}
        if (obj.tag == "Side" )
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, -rb.velocity.z);
        }
        if (obj.tag == "Front" && (transform.position.z < 75 || transform.position.z > 45))
        {

            rb.position = prevPos;
            transform.position = prevPos;
            rb.velocity = new Vector3(-rb.velocity.x, 0, rb.velocity.z);

  
        }
    }
}