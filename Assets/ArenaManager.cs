using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class ArenaManager : MonoBehaviour {
	public Texture2D tileMap;
	public int size;
	public int wallScale;
	float texScale = .125f;
	public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject arenaCamera;
	public List<GameObject> wallChunks;

    MeshFilter filter;
    MeshCollider collider;
    List<MeshGenerator> chunkManagers;
	MeshGenerator floorManager;
	MeshFilter floorFilter;
	MeshCollider floorCollider;
	Texture2D webcamTextureArena;
    float lastUpdate;

	void Start()
	{

		//Get the webcam texture
		webcamTextureArena = arenaCamera.GetComponent<RawImage>().texture as Texture2D;

		filter = GetComponent<MeshFilter>();
		collider = GetComponent <MeshCollider>();
        chunkManagers = new List<MeshGenerator>();
        for (int x = 0; x < size; x += 32)
        {
            for (int z = 0; z < size; z += 32)
            {
                wallChunks.Add(Instantiate(wallPrefab));

                int index = wallChunks.Count - 1;

                wallChunks[index].transform.position = (new Vector3(x, 0, z));
                wallChunks[index].name = x + "-0-" + z;
                var what = wallChunks[index];
                chunkManagers.Add(new MeshGenerator(texScale, wallChunks[index].GetComponent<MeshFilter>(), wallChunks[index].GetComponent<MeshCollider>(), wallChunks[index]));
            }
        }

        //Create the arena floor
        floorPrefab = GameObject.Instantiate(floorPrefab);
		floorPrefab.transform.position = new Vector3(0,0,0);
		floorPrefab.name = "Arena Floor";
		floorFilter = floorPrefab.GetComponent<MeshFilter>();
		floorCollider = floorPrefab.GetComponent<MeshCollider>();
		floorManager = new MeshGenerator(texScale,floorFilter, floorCollider);
        lastUpdate = Time.time;
	}

	void FixedUpdate()
	{
        if (Time.time - lastUpdate > .1f) 
        {
            webcamTextureArena = arenaCamera.GetComponent<RawImage>().texture as Texture2D;
            foreach (var chunkManager in chunkManagers)
            {
                chunkManager.GenerateWallsFromTexture(webcamTextureArena, 32, 32, wallScale, false);
            }
            floorManager.GenerateFloor(webcamTextureArena, 0, 0, 0, true, 256);

            lastUpdate = Time.time;
        }
	}

 

	

}
