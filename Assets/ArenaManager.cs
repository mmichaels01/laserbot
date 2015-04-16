using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class ArenaManager : MonoBehaviour
{

    public int arenaWidth;
    public int arenaHeight;
    public int chunkWidth;
    public int chunkHeight;

    //public int chunkSize;
    public int wallScale;
    float texScale = .125f;
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject arenaCamera;
    public List<GameObject> wallChunks;
    public float colorComparisonStrength = 40f;
    public Color targetColor = new Color(.1f, .1f, .1f);

    List<MeshGenerator> chunkManagers;
    MeshGenerator floorManager;
    WebCamTexture webcamTextureArena;
    float lastUpdate;
    int chunkIndex;
    bool[,] wallStateArray;


    void Start()
    {
        //Get the webcam texture
        webcamTextureArena = arenaCamera.GetComponent<RawImage>().texture as WebCamTexture;

        //Set the size of our bool array
        wallStateArray = new bool[arenaWidth, arenaHeight];

        //Create the chunk list
        chunkManagers = new List<MeshGenerator>();
        for (int x = 0; x < arenaWidth; x += chunkWidth)
        {
            for (int z = 0; z < arenaHeight; z += chunkHeight)
            {
                wallChunks.Add(Instantiate(wallPrefab));
                int index = wallChunks.Count - 1;
                wallChunks[index].transform.position = (new Vector3(x, 0, z));
                wallChunks[index].name = x + "-0-" + z;
                chunkManagers.Add(new MeshGenerator(texScale, wallChunks[index], wallStateArray, chunkWidth, chunkHeight, arenaWidth, arenaHeight));
            }
        }

        //Create the arena floor
        floorPrefab = GameObject.Instantiate(floorPrefab);
        floorPrefab.transform.position = new Vector3(0, 0, 0);
        floorPrefab.name = "Arena Floor";
        floorManager = new MeshGenerator(texScale, floorPrefab.GetComponent<MeshFilter>(), floorPrefab.GetComponent<MeshCollider>(), arenaWidth, arenaHeight);
        floorManager.GenerateFloor(Texture2D.blackTexture, 0, 0, 0, true);

        lastUpdate = Time.time;
        chunkIndex = 0;
    }

    void FixedUpdate()
    {
        if (webcamTextureArena != null)
        {

            UpdateTextureArena();
            DrawWalls();

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

    void DrawWalls()
    {
        ////Draw one each fixed update
        //chunkManagers[chunkIndex].GenerateWallsFromBoolArray(chunkSize, chunkSize, false);
        //chunkIndex++;
        //if (chunkIndex == chunkManagers.Count)
        //{
        //    chunkIndex = 0;
        //    UpdateWallArray(webcamTextureArena);
        //}

        //Draw them all each update
        if (Time.time - lastUpdate > .1f)
        {
            UpdateWallArray(webcamTextureArena);
            foreach (var chunk in chunkManagers)
            {
                chunk.GenerateWallsFromBoolArray(chunkWidth, chunkHeight, true);

            }
            lastUpdate = Time.time;
        }
    }

    void UpdateWallArray(WebCamTexture webcamTextureArena)
    {
            for (int x = 0; x < arenaWidth; x++)
            {
                for (int y = 0; y < arenaHeight; y++)
                {
                    wallStateArray[x, y] = IsWithinColorRange(webcamTextureArena.GetPixel(x, y), targetColor);
                }
            }
            //print(webcamTextureArena.width + " " + webcamTextureArena.height);
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
