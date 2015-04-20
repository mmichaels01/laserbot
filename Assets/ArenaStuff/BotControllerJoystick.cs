using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class BotControllerJoystick : MonoBehaviour
{



    Socket sock1;
    Socket sock2;
    int bytesSent;
    int bytesSentThisUpdate;
    byte[] msg;
    float update;

    public string ip;


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

        IPAddress ip1 = IPAddress.Parse(ip);

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

            float leftWheel1 = jstick.LeftWheel();
            float rightWheel1 = jstick.RightWheel();


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
