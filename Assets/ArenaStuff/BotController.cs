using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class BotController : MonoBehaviour
{

    public GameObject botObject;


    Socket sock1;
    Socket sock2;
    int bytesSent;
    int bytesSentThisUpdate;
    byte[] msg;
    float update;


    Joystick jstick = new Joystick();

    // Use this for initialization
    void Start()
    {
        bytesSent = 0;
        bytesSentThisUpdate = 0;
        sock1 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
            ProtocolType.Udp);
        sock2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
            ProtocolType.Udp);

        IPAddress ip1 = IPAddress.Parse("192.168.0.151");

        IPEndPoint ep1 = new IPEndPoint(ip1, 8080);

        try
        {
            sock1.Connect(ep1);
        }
        finally
        {

        }

        // Encode the data string into a byte array.
        byte[] msg = Encoding.ASCII.GetBytes("bot started");
        // Send the data through the socket.
        bytesSent += sock1.Send(msg);

        update = Time.time;
    }

    void FixedUpdate()
    {

        bytesSentThisUpdate = 0;

        if (Time.time - update > .5f)
        {


            #region Mike Keyboard Code

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector2 point = new Vector2(x, y);

            point = point.normalized;

            float leftWheel1 = Mathf.Pow(point.y, 2);
            float rightWheel1 = Mathf.Pow(point.y, 2);

            if (x < 0)
            {
                rightWheel1 += Mathf.Pow(point.x, 2);
            }
            else if (x > 0)
            {
                leftWheel1 += Mathf.Pow(point.x, 2);
            }

            if (y < 0)
            {
                leftWheel1 *= -1;
                rightWheel1 *= -1;
            }

            #endregion

            if (leftWheel1 > .1 && rightWheel1 > .1)
            {
                //Going forward
            }

            else if (leftWheel1 > .1 && rightWheel1 < .1)
            {
                gameObject.SendMessage("Rotate", 30);
            }

            else if (leftWheel1 < .1 && rightWheel1 > .1)
            {
                gameObject.SendMessage("Rotate", -30);

            }

            print(leftWheel1 + " " + rightWheel1 + " - Wheels");


            string msgWheels1 = "wheels," + leftWheel1 + "," + rightWheel1;


            IssueCommand(msgWheels1);


            update = Time.time;
        }
    }

    void IssueCommand(String message)
    {
        msg = Encoding.ASCII.GetBytes(message);
        bytesSent += sock1.Send(msg);
    }


}
