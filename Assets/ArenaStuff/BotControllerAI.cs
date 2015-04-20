using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class BotControllerAI : MonoBehaviour
{

    Vector3 targetPosition;

    public GameObject point;

    public float targetDistance = 15f;

    float lastTime;

    bool calibrated;
    bool movedLast;

    void Start()
    {

        lastTime = Time.time;
        calibrated = false;
        movedLast = true;

    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(point.transform.position, transform.position);
        targetPosition = point.transform.position;
        if (Time.time > 5)
        {
            if (Time.time - lastTime > 4)
            {
                float angle = BotHelper.GetAngle(transform.position,targetPosition);
                float angleDiff = angle - transform.localEulerAngles.y;
                print("transform" + (transform.localEulerAngles.y));
                print("angle" + angle);
                print("difference" +angleDiff);
                if (Mathf.Abs(angleDiff) > 15f && movedLast && distance > targetDistance)
                {
                    if(angleDiff > 180){
                        angleDiff -= 360;
                    }
                    else if (angleDiff < -180)
                    {
                        angleDiff += 360;
                    }
                    PlayMp3("ping");
                   RotateMessage(angleDiff);
                   movedLast = false;
                }

                if (distance > targetDistance)
                {
                    PlayMp3("cannon");
                    MoveMessage(6);
                    movedLast = true;
                }

                if (distance < targetDistance)
                {
                    PlayMp3("r2_surprise");
                }

                lastTime = Time.time;
            }
        }
        else if(!calibrated && Time.time > 2)
        {
            calibrated = true;
            MoveMessage(6f);
        }
        
    }

    void Move()
    {

    }

    void MoveMessage(float distance)
    {
        string msg = "move," + distance;
        gameObject.SendMessage("IssueCommand", msg);
    }

    void RotateMessage(float degrees)
    {
        string msg = "rotate," + degrees;
        gameObject.SendMessage("IssueCommand", msg);
    }

    void PlayMp3(string sound)
    {
        string msg = "mp3," + sound;
        gameObject.SendMessage("IssueCommand", msg);
    }

    void PlayWav(string sound)
    {
        string msg = "wav," + sound;
        gameObject.SendMessage("IssueCommand", msg);
    }
}
