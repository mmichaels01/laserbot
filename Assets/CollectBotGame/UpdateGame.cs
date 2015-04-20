using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateGame : MonoBehaviour {

    public GameObject cubePrefab;
    public GameObject redBot;
    public GameObject blueBot;
    GameObject cube;
    int redScore = 0;
    int blueScore = 0;
    Text redText;
    Text blueText;
    Text message;
    public int maxPoints;

	void Start () {
        cube = Instantiate(cubePrefab, GetInitialRandomPosition(), Quaternion.identity) as GameObject;
        message = GameObject.Find("message").GetComponent<Text>();
        redText = GameObject.Find("RedBotScore").GetComponent<Text>();
        blueText = GameObject.Find("BlueBotScore").GetComponent<Text>();
	}
	
	void FixedUpdate ()
    {
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
        GameObject.Find("RedBot").SendMessage("IssueCommand", "wav,asshole");

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