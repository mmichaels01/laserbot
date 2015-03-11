using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class ArenaManager : MonoBehaviour {
	public Texture2D tileMap;
	public int height;
	public int width;

	MeshFilter filter;
	MeshCollider collider;

	float texScale = .125f;

	MeshGenerator manager;

	void Start()
	{
		filter = GetComponent<MeshFilter>();
		collider = GetComponent <MeshCollider>();
		manager = new MeshGenerator(texScale, filter, collider);
	}

	void FixedUpdate()
	{
		manager.GenearateWallsFromTexture(GameObject.Find("WebCamTextureArena").GetComponent<RawImage>().texture as Texture2D,100,100, false);
	}

	

}
