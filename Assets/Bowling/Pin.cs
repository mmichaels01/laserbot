using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {

    Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
    bool Fallen()
    {
        if (transform.rotation.z != 0)
            return true;
        return false;
    }
}