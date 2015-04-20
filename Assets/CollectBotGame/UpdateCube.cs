using UnityEngine;
using System.Collections;

public class UpdateCube : MonoBehaviour {

	void FixedUpdate()
    {
        transform.Rotate(new Vector3(5, 5, 0));
	}

    void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "BlueBot")
            ((UpdateGame)GameObject.Find("UpdateGameLogic").GetComponent(typeof(UpdateGame))).BlueScore();
        else if (obj.tag == "RedBot")
            ((UpdateGame)GameObject.Find("UpdateGameLogic").GetComponent(typeof(UpdateGame))).RedScore();
    }
}