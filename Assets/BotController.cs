﻿using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class BotController : MonoBehaviour
{
    Socket sock;
    int bytesSent;
    int bytesSentThisUpdate;
    byte[] msg;
    float update;

    // Use this for initialization
    void Start()
    {
        bytesSent = 0;
        bytesSentThisUpdate = 0;
        sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
            ProtocolType.Udp);

        IPAddress ip = IPAddress.Parse("192.168.0.150");

        IPEndPoint ep = new IPEndPoint(ip, 8080);
        try
        {
            print("connecting");
            sock.Connect(ep);
        }
        finally
        {

        }

        // Encode the data string into a byte array.
        byte[] msg = Encoding.ASCII.GetBytes("bot started");
        // Send the data through the socket.
        bytesSent += sock.Send(msg);

        update = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        bytesSentThisUpdate = 0;

        if (Time.time - update > .1f)
        {
            float leftWheel = Input.GetAxis("LeftWheel");
            float rightWheel = Input.GetAxis("RightWheel");
            float aimHorizontal = Input.GetAxis("AimHorizontal");
            float aimVertical = Input.GetAxis("AimVertical");


            //if (!Mathf.Approximately(leftWheel, 0) || !Mathf.Approximately(rightWheel, 0))
            //{
                string msgWheels = "wheels," + leftWheel + "," + rightWheel;
                msg = Encoding.ASCII.GetBytes(msgWheels);
                bytesSent += sock.Send(msg);
            //}

            if (!Mathf.Approximately(aimHorizontal, 0) || !Mathf.Approximately(aimVertical, 0))
            {

                string msgCamera = "camera" + "," + aimHorizontal + "," + aimVertical;
                msg = Encoding.ASCII.GetBytes(msgCamera);
                bytesSent += sock.Send(msg);
            }

            update = Time.time;
        }
    }
}
