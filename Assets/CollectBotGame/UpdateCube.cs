using UnityEngine;
using System.Collections;

public class UpdateCube : MonoBehaviour {
	void FixedUpdate()
    {
        transform.Rotate(new Vector3(5, 5, 0));
	}
}