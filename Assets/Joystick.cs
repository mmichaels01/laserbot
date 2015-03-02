using UnityEngine;
using System;

public class Joystick
{
    public int LeftWheel()
    {
        int dir = GetDirection();
        if (dir == 3 || dir == 1)
            return 1;
        return 0;
    }

    public int RightWheel()
    {
        int dir = GetDirection();
        if (dir == 0 || dir == 1)
            return 1;
        return 0;
    }

    public int GetDirection()
    {
        float X = Input.GetAxis("JoystickX");
        float Y = Input.GetAxis("JoystickY") * -1;
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

    public float ToDegrees(float radians)
    {
        return 180 / (float)Math.PI * radians;
    }
}