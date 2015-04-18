using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class FloorGenerator : MonoBehaviour {

    int size;

	// Use this for initialization
	void Start () {
        size = GameObject.Find("WebCamTextureArena").GetComponent<RawImage>().texture.width;



	}
	
}
