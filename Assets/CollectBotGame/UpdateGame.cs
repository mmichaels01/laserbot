using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateGame : MonoBehaviour {

    public GameObject cube;
    public GameObject redBot;
    public GameObject blueBot;
    int redScore = 0;
    int blueScore = 0;
    Text redText;
    Text blueText;
    Text message;
    public int maxPoints;
    public int distance;

	void Start () {
        SetRandomPosition();
        message = GameObject.Find("message").GetComponent<Text>();
        redText = GameObject.Find("RedBotScore").GetComponent<Text>();
        blueText = GameObject.Find("BlueBotScore").GetComponent<Text>();
	}
	
	void FixedUpdate ()
    {
        if (Vector3.Distance(cube.transform.position, redBot.transform.position) < distance)
        {
            RedScore();
            SetRandomPosition();
        }
        if (Vector3.Distance(cube.transform.position, blueBot.transform.position) < distance)
        {
            BlueScore();
            SetRandomPosition();
        }
    }

    Vector3 GetInitialRandomPosition()
    {
        return new Vector3(Random.Range(20, 140), 0, Random.Range(20, 100));
    }

    void SetRandomPosition()
    {
        cube.transform.position = new Vector3(Random.Range(20, 140), 0, Random.Range(20, 100));
    }

    public void BlueScore()
    {
        blueScore++;
        blueBot.SendMessage("IssueCommand", "mp3,r2_surprise");
        if (blueScore == maxPoints)
        {
            message.text = "BlueBot Wins!";
            cube.transform.position = new Vector3(10000, 10000, 10000);
        }
        else
        {
            message.text = "BlueBot has scored a point!";
            SetRandomPosition();
        }

        blueText.text = "BlueBot: " + blueScore;
    }

    public void RedScore()
    {
        redScore++;
        redBot.SendMessage("IssueCommand", "mp3,r2_unbelievable");

        if (redScore == maxPoints)
        {
            message.text = "RedBot Wins!";
            cube.transform.position = new Vector3(10000, 10000, 10000);
        }
        else
        {
            message.text = "RedBot has scored a point!";
            SetRandomPosition();
        }

        redText.text = "RedBot: " + redScore;
    }
}