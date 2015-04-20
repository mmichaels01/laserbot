using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FollowBowlBall : MonoBehaviour
{
    public GameObject ball;
    public GameObject pinPrefab;

    private AudioSource source;
    public AudioClip pinSound;
    public AudioClip ballSound;

    List<GameObject> pins = new List<GameObject>();
    List<Pin> pinObject = new List<Pin>();
    PinPositions pinPos = new PinPositions();

    int pinsDown = 0;

    int waitTimer = 0;
    int displayScoreTimer = 0;
    bool playSoundRoll = false;
    bool hitPin = false;

    Text message;

    int score = 0;
    int frame = 1;

    int startFrameTimer = 60*4;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        message = GameObject.Find("message").GetComponent<Text>();
    }

	void Start () {
        for (int i = 0; i < pinPos.pinPositions.Length; i++)
        {
            pins.Add(Instantiate(pinPrefab, pinPos.pinPositions[i], Quaternion.identity) as GameObject);
            pinObject.Add(new Pin());
        }
        message.text = ((int)(startFrameTimer / 60)).ToString();
    }
	
	void FixedUpdate ()
    {
        if (startFrameTimer != 0)
        {
            startFrameTimer--;
            if (startFrameTimer <= 60)
                message.text = "Bowl!";
            else
                message.text = ((int)(startFrameTimer / 60)).ToString();
            if (startFrameTimer == 0)
            {
                message.text = "";
                message.rectTransform.Translate(new Vector3(110, 0, 0));
                message.fontSize = 20;
            }
        }
        else
        {
            if (displayScoreTimer > 0)
            {
                displayScoreTimer++;
                if (displayScoreTimer > 60 * 5)
                {
                    if (frame == 3)
                        message.text = "Your final score is " + score + "!";
                    else
                        Reset();
                }
            }
            if (ball.transform.position.x < 0 && ball.transform.position.x > -400f)
            {
                transform.LookAt(ball.transform.position);
                transform.position = new Vector3(ball.transform.position.x + 20, ball.transform.position.y + 50, transform.position.z);
            }
            else if (ball.transform.position.x < -400)
            {
                transform.position = new Vector3(-450f, 200f, 60f);
                transform.LookAt(new Vector3(-450f, 20f, 60f));

                if (ball.transform.position.x < -550 && displayScoreTimer == 0)
                {
                    if (pinsDown == 0)
                        source.Stop();
                    waitTimer++;
                    if (waitTimer > 60 * 1)
                    {
                        displayScoreTimer++;
                        waitTimer = 0;
                        if (pinsDown == 10)
                            message.text = "You got a Strike!!!";
                        else if (pinsDown == 1)
                            message.text = "You knocked over " + pinsDown + " pin!";
                        else if (pinsDown == 0)
                            message.text = "You didn't knock over any pins...";
                        else
                            message.text = "You knocked over " + pinsDown + " pins!";
                    }
                }
            }
            if (ball.transform.position.x < 0 && playSoundRoll == false)
            {
                source.PlayOneShot(ballSound, 0.1f);
                playSoundRoll = true;
                source.loop = true;
            }
            UpdatePins();
        }
	}

    void Reset()
    {
        message.text = "";
        score += pinsDown;
        pinsDown = 0;
        playSoundRoll = false;
        hitPin = false;
        displayScoreTimer = 0;
        waitTimer = 0;

        transform.position = new Vector3(64, 100, 64);
        transform.rotation = Quaternion.EulerRotation(Mathf.PI / 2, Mathf.PI / 2, 0);

        ((BallCylinderManager)ball.GetComponent(typeof(BallCylinderManager))).Reset();

        for (int i = 0; i < pins.Count; i++)
        {
            Destroy(pins[i]);
            pins.RemoveAt(i);
            i--;
        }

        pins.Clear();
        pinObject.Clear();

        for (int i = 0; i < pinPos.pinPositions.Length; i++)
        {
            pins.Add(Instantiate(pinPrefab, pinPos.pinPositions[i], Quaternion.identity) as GameObject);
            pinObject.Add(new Pin());
        }

        startFrameTimer = 60 * 4;
        message.rectTransform.Translate(new Vector3(-110, 0, 0));
        message.fontSize = 40;
        frame++;
    }

    void UpdatePins()
    {
        for (int i = 0; i < pinPos.pinPositions.Length; i++)
        {
            if (Fallen(i))
                pinsDown++;
        }
    }

    public bool Fallen(int i)
    {
        if (Mathf.Abs(pins[i].transform.rotation.z) > 0.05f)
        {
            if (pinObject[i].GetFallen() == false)
            {
                if (hitPin == false)
                {
                    hitPin = true;
                    source.Stop();
                    source.loop = false;
                }
                pinObject[i].SetFallen();
                source.PlayOneShot(pinSound, 0.5f);
                return true;
            }
        }
        return false;
    }
}