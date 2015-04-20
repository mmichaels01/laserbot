using UnityEngine;
using System.Collections;

public class Pin {

    bool fallen = false;

    public Pin()
    {

    }
	
    public bool GetFallen()
    {
        return fallen;
    }

    public void SetFallen()
    {
        fallen = true;
    }
}