using UnityEngine;
using System;
using System.Text;

using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class Joystick : MonoBehaviour
{
    int count = 0;
    public float LeftWheel()
    {
        int dir = GetDirection8Way();
        if (dir == 2 || dir == 3 || dir == 4)//left, up-left, or up, send normal magnitude
            return GetMagnitude();
        else if (dir == 1)                  //up-right, move wheel at half the speed to make decent turn
            return GetMagnitude() / 2;
        else if (dir == 5 || dir == 6)      //down-left, down, return negative magnitude to show to reverse the wheels
            return -GetMagnitude();
        else if (dir == 7)                  //down-right
            return -GetMagnitude() / 2;
        return 0;
    }

    public float RightWheel()
    {
        int dir = GetDirection8Way();
        if (dir == 0 || dir == 1 || dir == 2)
            return GetMagnitude();
        else if (dir == 3)
            return GetMagnitude() / 2;
        else if (dir == 7 || dir == 6)
            return -GetMagnitude();
        else if (dir == 5)
            return -GetMagnitude() / 2;
        return 0;
    }

    public float GetMagnitude()
    {
        //return the max value between the two axis
        return Math.Max(Math.Abs(Input.GetAxis("LeftWheel")), Math.Abs(Input.GetAxis("RightWheel") * -1));
    }

    public int GetDirection8Way()
    {
        //Get axis data
        float X = Input.GetAxis("LeftWheel");
        float Y = Input.GetAxis("RightWheel") * -1;
        float rotation = (float)Math.Atan2(Y, X);

        //Sensitivity, if number to low, set rotation to 361, EvalauateDirection will return -1
        if (X < 0.2f && Y < 0.2f &&
            X > -0.2f && Y > -0.2f)
            rotation = 361;

        return EvaluateDirection8Way(rotation);
    }

    public int EvaluateDirection8Way(float direction)
    {
        //Convert direction to degrees
        float n = ToDegrees(direction);

        //if the direction is negative, make it positive
        if (n < 0)
            n += 360;

        //Check to see what range the direction is in and return a number that corresponds with a direction
        if (n < 22.5 || n > 337.5 && n <= 360)
            return 0;//right
        else if (n > 22.5 && n < 67.5)
            return 1;//up-right
        else if (n > 67.5 && n < 112.5)
            return 2;//up
        else if (n > 112.5 && n < 157.5)
            return 3;//up-left
        else if (n > 157.5 && n < 202.5)
            return 4;//left
        else if (n > 202.5 && n < 247.5)
            return 5;//down-left
        else if (n > 247.5 && n < 292.5)
            return 6;//down
        else if (n > 292.5 && n < 337.5)
            return 7;//down-right
        return -1;//none
    }

    public float ToDegrees(float radians)
    {
        return 180 / (float)Math.PI * radians;//Convert radians to degrees, degrees are easier to work with
    }

    //Previous Methods Used
    public int GetDirection()
    {
        float X = Input.GetAxis("LeftWheel");
        float Y = Input.GetAxis("RightWheel") * -1;
        float rotation = (float)Math.Atan2(Y, X);

        if (X < 0.2f && Y < 0.2f &&
            X > -0.2f && Y > -0.2f)
            rotation = 361;

        return EvaluateDirection(rotation);
    }
    public int EvaluateDirection(float direction)
    {
        float n = ToDegrees(direction);
        if (n < 0)
            n += 360;
        if (n < 45 || n > 315 && n <= 360)
            return 0;//right
        else if (n > 45 && n < 135)
            return 1;//up
        else if (n > 225 && n < 315)
            return 2;//down
        else if (n > 135 && n < 225)
            return 3;//left
        return -1;//none
    }
}