using UnityEngine;
using System.Collections;

public class PinPositions {
    
    public Vector3[] pinPositions = new Vector3[10];
    float yPlacement = 40f;

    public PinPositions()
    {
        //Fourth row
        pinPositions[0] = new Vector3(-525, yPlacement, 100);
        pinPositions[1] = new Vector3(-525, yPlacement, 100 - 26.25f);
        pinPositions[2] = new Vector3(-525, yPlacement, 100 - (26.25f * 2));
        pinPositions[3] = new Vector3(-525, yPlacement, 100 - (26.25f * 3));

        //Third row
        pinPositions[4] = new Vector3(-525 + 26.25f, yPlacement, 100 - 13.125f);
        pinPositions[5] = new Vector3(-525 + 26.25f, yPlacement, 100 - 26.25f - 13.125f);
        pinPositions[6] = new Vector3(-525 + 26.25f, yPlacement, 100 - (26.25f * 2) - 13.125f);

        //Second row
        pinPositions[7] = new Vector3(-525 + 26.25f * 2, yPlacement, 100 - 26.25f);
        pinPositions[8] = new Vector3(-525 + 26.25f * 2, yPlacement, 100 - 26.25f - 26.25f);

        //First row
        pinPositions[9] = new Vector3(-525 + 26.25f * 3, yPlacement, 100 - 26.25f - 13.125f);
    }
}