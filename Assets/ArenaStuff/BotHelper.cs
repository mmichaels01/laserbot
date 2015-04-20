using UnityEngine;
using System.Collections;

public class BotHelper {

    public static float GetAngle(Vector3 lastPos, Vector3 curPos)
    {
        float distance = Vector3.Distance(lastPos, curPos);
        float op = curPos.x - lastPos.x;
        float adj = curPos.z - lastPos.z;
        float angle = 0f;

        if ((op > 0f && adj > 0f) || ((op > 0f && adj < 0f)))
        {
            angle = Mathf.Rad2Deg * Mathf.Acos(adj / distance);
        }

        else if ((op < 0f && adj > 0f) || (op < 0f && adj < 0f))
        {
            angle = 360f - (Mathf.Rad2Deg * Mathf.Acos(adj / distance));
        }
        else
        {
            angle = 0f;
        }

        return angle;
    }
}
