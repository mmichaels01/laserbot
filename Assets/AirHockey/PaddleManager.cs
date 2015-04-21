using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PaddleManager : MonoBehaviour
{


    public float colorComparisonStrength = 40f;
    public Color targetColor = new Color(.1f, .1f, .1f);
    public GameObject arenaCamera;
    public float distanceToUpdate;

    WebCamTexture webcamTextureArena;
    float lastUpdate;
    bool[,] robotStateArray;
    int arenaWidth = 160;
    int arenaHeight = 120;

    int numPixels;
    float radius;
    float xTotal;
    float zTotal;
    float xAverage;
    float zAverage;

    Vector3 lastPosition;

    void Start()
    {
        webcamTextureArena = arenaCamera.GetComponent<RawImage>().texture as WebCamTexture;
        numPixels = 0;
        radius = 0;
        robotStateArray = new bool[arenaWidth, arenaHeight];
        lastUpdate = Time.time;
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (webcamTextureArena != null)
        {

            UpdateTextureArena();
            UpdateRobotArray(webcamTextureArena);
            DrawBot();

        }
        else
        {
            UpdateTextureArena();
        }

        float distance = Vector3.Distance(lastPosition, transform.position);
        if (distance > distanceToUpdate)
        {
            //float op = transform.position.x - lastPosition.x;
            //float adj = transform.position.z - lastPosition.z;
            float angle = BotHelper.GetAngle(lastPosition, transform.position);

            //if ((op > 0f && adj > 0f) || ((op > 0f && adj < 0f)))
            //{
            //    angle = Mathf.Rad2Deg * Mathf.Acos(adj / distance);
            //}

            //else if ((op < 0f && adj > 0f) || (op < 0f && adj < 0f))
            //{
            //    angle = 360f - (Mathf.Rad2Deg * Mathf.Acos(adj / distance));
            //}
            //else
            //{
            //    angle = 0f;
            //    print("Probably shouldn't be in this condition");
            //}

            transform.localRotation = Quaternion.AngleAxis(angle, Vector3.up);
            //print(Mathf.Rad2Deg * Mathf.Acos(adj / distance));
            //print("Updating angle");
            lastPosition = transform.position;
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
                transform.localScale = new Vector3(15f, .1f, 15f);
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

    void Rotate(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }

}
