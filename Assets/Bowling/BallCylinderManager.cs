using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class BallCylinderManager : MonoBehaviour {


    public float colorComparisonStrength = 40f;
    public Color targetColor = new Color(.1f, .1f, .1f);
    public GameObject arenaCamera;

    WebCamTexture webcamTextureArena;
    bool[,] robotStateArray;
    int arenaWidth = 160;
    int arenaHeight = 120;

    float lastUpdate;


    float startTime;
    float endTime;

    int numPixels;
    float radius;
    float xTotal;
    float zTotal;
    float xAverage;
    float zAverage;

    Rigidbody rb;

    Vector3 startPos;
    Vector3 endPos;

    bool rolling;

    GameObject[] bumpers;

    void Start()
    {
        webcamTextureArena = arenaCamera.GetComponent<RawImage>().texture as WebCamTexture;
        numPixels = 0;
        radius = 0;
        robotStateArray = new bool[arenaWidth, arenaHeight];
        rb = GetComponent<Rigidbody>();
        lastUpdate = Time.time;
        startTime = 0;
        endTime = 0;

        bumpers = GameObject.FindGameObjectsWithTag("Bumper");
    }

    void Update()
    {
        if (!rolling)
        {
            if (webcamTextureArena != null && endTime < 1)
            {
                UpdateTextureArena();
                UpdateRobotArray(webcamTextureArena);
                DrawBot();
            }

            if (Mathf.Approximately(0f, startTime) && numPixels > 10)
            {
                startTime = Time.time;
                startPos = transform.position;
            }

            if (!Mathf.Approximately(0f, startTime) && Mathf.Approximately(0f, endTime))
            {
                print("x" + transform.position.x);
                print("z" + transform.position.z);


                if (transform.position.x < 25)
                {
                    endTime = Time.time;
                    endPos = transform.position;
                }

            }

            float timeDiff = endTime - startTime;

            if (startTime > 0 && endTime > 0 && (transform.position.x < 25) && timeDiff > .01f)// || transform.position.x > arenaWidth - 10 || transform.position.z < 10 || transform.position.z > arenaHeight - 10))
            {
                print("Setting velocity");
                rb.AddForce(new Vector3(((endPos.x - startPos.x) / (timeDiff)) * 4.0f, 0f, ((endPos.z - startPos.z) / (timeDiff)) * 4.0f), ForceMode.VelocityChange);
                //rb.velocity = new Vector3((endPos.x - startPos.x) / (timeDiff), 0f, (endPos.z - startPos.z) / (timeDiff));
                rolling = true;
            }

            else
            {
                UpdateTextureArena();
            }

        }

        if (Input.GetKeyDown("b"))
        {
            foreach(GameObject bumper in bumpers ){
                if (bumper.activeSelf)
                {
                    bumper.SetActive(false);
                    
                }
                else
                {
                    bumper.SetActive(true);
                }
            }
        }

    }

    void UpdateTextureArena()
    {
        webcamTextureArena = arenaCamera.GetComponent<RawImage>().texture as WebCamTexture;
    }

    void DrawBot()
    {
        //Draw them all each update
        if (Time.time - lastUpdate > .1f)
        {
            numPixels = 0;
            xTotal = 0;
            zTotal = 0;
            UpdateRobotArray(webcamTextureArena);

            if (numPixels > 10)
            {
                radius = Mathf.Sqrt((numPixels / Mathf.PI));
                transform.localScale = new Vector3(radius, radius, radius);
                xAverage = xTotal / numPixels;
                zAverage = zTotal / numPixels;
                transform.position = new Vector3(xAverage, .5f, zAverage);
            }
            lastUpdate = Time.time;
        }
    }

    void UpdateRobotArray(WebCamTexture webcamTextureArena)
    {
        for (int x = 0; x < arenaWidth; x++)
        {
            for (int y = 0; y < arenaHeight; y++)
            {
                robotStateArray[x, y] = IsWithinColorRange(webcamTextureArena.GetPixel(x, y), targetColor);
                if (robotStateArray[x, y])
                {
                    xTotal += x;
                    zTotal += y;
                }
            }
        }
    }

    bool IsWithinColorRange(Color inputColor, Color targetColor)
    {
        if (Mathf.Abs(targetColor.r - inputColor.r) < colorComparisonStrength / 256f
            && Mathf.Abs(targetColor.g - inputColor.g) < colorComparisonStrength / 256f
            && Mathf.Abs(targetColor.b - inputColor.b) < colorComparisonStrength / 256f)
        {
            numPixels++;
            return true;
        }

        return false;
    }

    
    void OnTriggerEnter()
    {
        rb.velocity = (new Vector3(rb.velocity.x, rb.velocity.y, -rb.velocity.z));
    }
}
