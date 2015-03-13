using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class ArenaManager : MonoBehaviour
{
    public Texture2D tileMap;
    public int arenaSize;
    public int chunkSize;
    public int wallScale;
    float texScale = .125f;
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject arenaCamera;
    public List<GameObject> wallChunks;
    public float colorComparisonStrength = 40f;
    public Color targetColor = new Color(.1f, .1f, .1f);

    MeshFilter filter;
    MeshCollider collider;
    List<MeshGenerator> chunkManagers;
    MeshGenerator floorManager;
    MeshFilter floorFilter;
    MeshCollider floorCollider;
    Texture2D webcamTextureArena;
    float lastUpdate;

    bool[,] wallStateArray;

    void Start()
    {
        //Get the webcam texture
        webcamTextureArena = arenaCamera.GetComponent<RawImage>().texture as Texture2D;

        //Set the size of our bool array
        wallStateArray = new bool[arenaSize, arenaSize];

        //Create the chunk list
        filter = GetComponent<MeshFilter>();
        collider = GetComponent<MeshCollider>();
        chunkManagers = new List<MeshGenerator>();
        for (int x = 0; x < arenaSize; x += chunkSize)
        {
            for (int z = 0; z < arenaSize; z += chunkSize)
            {
                wallChunks.Add(Instantiate(wallPrefab));
                int index = wallChunks.Count - 1;
                wallChunks[index].transform.position = (new Vector3(x, 0, z));
                wallChunks[index].name = x + "-0-" + z;
                chunkManagers.Add(new MeshGenerator(texScale, wallChunks[index], wallStateArray, arenaSize));
            }
        }

        //Create the arena floor
        floorPrefab = GameObject.Instantiate(floorPrefab);
        floorPrefab.transform.position = new Vector3(0, 0, 0);
        floorPrefab.name = "Arena Floor";
        floorFilter = floorPrefab.GetComponent<MeshFilter>();
        floorCollider = floorPrefab.GetComponent<MeshCollider>();
        floorManager = new MeshGenerator(texScale, floorFilter, floorCollider);


        lastUpdate = Time.time;
    }

    void Update()
    {
        UpdateTextureArena();
        DrawWalls();
    }

    void UpdateTextureArena()
    {
        webcamTextureArena = arenaCamera.GetComponent<RawImage>().texture as Texture2D;
        print(webcamTextureArena.width);
    }

    void DrawWalls()
    {
        if (Time.time - lastUpdate > 1f)
        {
            UpdateWallArray(webcamTextureArena);
            floorManager.GenerateFloor(webcamTextureArena, 0, 0, 0, true, 128);
            lastUpdate = Time.time;

            foreach (var chunkManager in chunkManagers)
            {
                chunkManager.GenerateWallsFromBoolArray(chunkSize, chunkSize, false);
            }
        }
    }

    void UpdateWallArray(Texture2D t)
    {
        for (int x = 0; x < arenaSize; x++)
        {
            for (int y = 0; y < arenaSize; y++)
            {
                wallStateArray[x, y] = IsWithinColorRange(t.GetPixel(x, y), targetColor);
            }
        }
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
