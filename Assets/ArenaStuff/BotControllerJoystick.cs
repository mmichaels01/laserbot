using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class BotControllerJoystick : MonoBehaviour
{
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
        sock2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
            ProtocolType.Udp);

        IPAddress ip2 = IPAddress.Parse("192.168.0.152");

        IPEndPoint ep2 = new IPEndPoint(ip2, 8080);

        try
        {
            sock2.Connect(ep2);
        }
        finally
        {

        }

        // Encode the data string into a byte array.
        byte[] msg = Encoding.ASCII.GetBytes("bot started");
        // Send the data through the socket.
        bytesSent += sock2.Send(msg);

        update = Time.time;
    }

    void FixedUpdate()
    {
        
        bytesSentThisUpdate = 0;

        if (Time.time - update > .1f)
        {

            #region Ricky Joystick Code
            float leftWheel2 = jstick.LeftWheel();
            float rightWheel2 = jstick.RightWheel();

            #endregion



            string msgWheels2 = "wheels," + leftWheel2 + "," + rightWheel2;

            msg = Encoding.ASCII.GetBytes(msgWheels2);
            bytesSent += sock2.Send(msg);



            update = Time.time;
        }
    }


}
