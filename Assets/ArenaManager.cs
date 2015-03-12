using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class ArenaManager : MonoBehaviour {
	public Texture2D tileMap;
	public int size;

	public int wallScale;

	MeshFilter filter;
	MeshCollider collider;

	float texScale = .125f;

	MeshGenerator manager;

	public GameObject floor;
	public GameObject[] chunks;

	MeshGenerator floorManager;
	MeshFilter floorFilter;
	MeshCollider floorCollider;

	Texture2D webcamTextureArena;

	void Start()
	{

		//Get the webcam texture
		webcamTextureArena = GameObject.Find("WebCamTextureArena").GetComponent<RawImage>().texture as Texture2D;

		filter = GetComponent<MeshFilter>();
		collider = GetComponent <MeshCollider>();
		manager = new MeshGenerator(texScale, filter, collider);

		for (int x = 0; x < size; x += 32)
		{
			for (int z = 0; z < size; z += 32)
			{
                //Resume here with chunk code
			}
		}


			//Create the arena floor
			floor = GameObject.Instantiate(floor);
		floor.transform.position = new Vector3(0,0,0);
		floor.name = "Arena Floor";
		floorFilter = floor.GetComponent<MeshFilter>();
		floorCollider = floor.GetComponent<MeshCollider>();
		floorManager = new MeshGenerator(texScale,floorFilter, floorCollider);

	}

	void FixedUpdate()
	{
		webcamTextureArena = GameObject.Find("WebCamTextureArena").GetComponent<RawImage>().texture as Texture2D;
		manager.GenearateWallsFromTexture(webcamTextureArena,32,32, wallScale, false);
		floorManager.GenerateFloor(webcamTextureArena, 0, 0, 0, true, 256);
	}

 

	

}
