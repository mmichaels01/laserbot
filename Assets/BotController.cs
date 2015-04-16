using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class BotController : MonoBehaviour
{
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
        IPAddress ip2 = IPAddress.Parse("192.168.0.152");

        IPEndPoint ep1 = new IPEndPoint(ip1, 8080);
        IPEndPoint ep2 = new IPEndPoint(ip2, 8080);

        try
        {
            sock1.Connect(ep1);
            sock2.Connect(ep2);
        }
        finally
        {

        }

        // Encode the data string into a byte array.
        byte[] msg = Encoding.ASCII.GetBytes("bot started");
        // Send the data through the socket.
        bytesSent += sock1.Send(msg);
        bytesSent += sock2.Send(msg);

        update = Time.time;
    }

    void FixedUpdate()
    {
        
        bytesSentThisUpdate = 0;

        if (Time.time - update > .1f)
        {
            float leftWheel1 = Input.GetAxis("LeftWheel");
            float rightWheel1 = Input.GetAxis("RightWheel");
            //float aimHorizontal = Input.GetAxis("AimHorizontal");
            //float aimVertical = Input.GetAxis("AimVertical");

            #region Mike Keyboard Code
            
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector2 point = new Vector2(x, y);

            point = point.normalized;

            leftWheel1 = Mathf.Pow(point.y, 2);
            rightWheel1 = Mathf.Pow(point.y,2);

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

            #region Ricky Joystick Code
            float leftWheel2 = jstick.LeftWheel();
            float rightWheel2 = jstick.RightWheel();

            #endregion

            print(leftWheel1 + " " + rightWheel1 + " - Wheels");


            string msgWheels1 = "wheels," + leftWheel1 + "," + rightWheel1;
            string msgWheels2 = "wheels," + leftWheel2 + "," + rightWheel2;

            msg = Encoding.ASCII.GetBytes(msgWheels1);
            bytesSent += sock1.Send(msg);
            msg = Encoding.ASCII.GetBytes(msgWheels2);
            bytesSent += sock2.Send(msg);



            update = Time.time;
        }
    }
}
