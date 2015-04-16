using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class BotManager : MonoBehaviour {


    public float colorComparisonStrength = 40f;
    public Color targetColor = new Color(.1f, .1f, .1f);
    public GameObject arenaCamera;
    public GameObject botPrefab;

    WebCamTexture webcamTextureArena;
    float lastUpdate;
    bool[,] robotStateArray;
    MeshGenerator botManager;
    int arenaWidth = 160;
    int arenaHeight = 120;

    void Start()
    {
        //Get the webcam texture
        webcamTextureArena = arenaCamera.GetComponent<RawImage>().texture as WebCamTexture;
        botPrefab = GameObject.Instantiate(botPrefab);
        robotStateArray = new bool[arenaWidth, arenaHeight];
        botManager = new MeshGenerator(1, botPrefab, robotStateArray, arenaWidth, arenaHeight);
        lastUpdate = Time.time;
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
            UpdateRobotArray(webcamTextureArena);
            botManager.GenerateBotFromBoolArray(true);
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
            }
        }
        print(webcamTextureArena.width + " " + webcamTextureArena.height);
    }

    bool IsWithinColorRange(Color inputColor, Color targetColor)
    {
        if (Mathf.Abs(targetColor.r - inputColor.r) < colorComparisonStrength / 256f
            && Mathf.Abs(targetColor.g - inputColor.g) < colorComparisonStrength / 256f
            && Mathf.Abs(targetColor.b - inputColor.b) < colorComparisonStrength / 256f)
        {
            return true;
        }

        return false;
    }
}
