using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class MeshGenerator : MonoBehaviour {
	Texture2D heightMap;
	public Texture2D tileMap;


	public int height;
	public int width;

	MeshFilter filter;
	MeshCollider collider;

	List<Vector3> verts = new List<Vector3>();
	List<int> tris = new List<int>();
	List<Vector2> uvs = new List<Vector2>();

	float texScale = .125f;

	void Start()
	{
		heightMap = GameObject.Find("WebCamTextureArena").GetComponent<RawImage>().texture as Texture2D;
		filter = GetComponent<MeshFilter>();
		collider = GetComponent <MeshCollider>();

	}

	void FixedUpdate()
	{

		heightMap = GameObject.Find("WebCamTextureArena").GetComponent<RawImage>().texture as Texture2D;

		for (int x = 0; x < width; x++)
		{
			for (int z = 0; z < height; z++)
			{
				GenSquare(x, z, heightMap.GetPixel(x, z), new Color(0f / 256f, 0f / 256f, 0f / 256f));
			}
		}

		print("h - " + height + " w - " + width);
		print(verts.Count);
		GenMesh();

	}

	bool WithinColorRange(Color inputColor, Color targetColor, float strength)
	{
		if (Mathf.Abs(targetColor.r - inputColor.r) < strength / 256f
			&& Mathf.Abs(targetColor.g - inputColor.g) < strength / 256f
			&& Mathf.Abs(targetColor.b - inputColor.b) < strength / 256f)
		{
			return true;
		}

		return false;
	}


	void GenSquare(int x, int z, Color inputColor, Color targetColor)
	{
		//Extrude upward
		if (WithinColorRange(targetColor, inputColor, 64))
		{

			verts.Add(new Vector3(x, 1, z));
			verts.Add(new Vector3(x, 1, z + 1));
			verts.Add(new Vector3(x + 1, 1, z + 1));
			verts.Add(new Vector3(x + 1, 1, z));

			GenSquareUVs(0, 0);
			GenSquareTris();
		}
	}

	void GenSquareTris()
	{
		int index = verts.Count - 4;

		//First tri
		tris.Add(index);
		tris.Add(index + 1);
		tris.Add(index + 2);

		//Second tri
		tris.Add(index);
		tris.Add(index + 2);
		tris.Add(index + 3);
	}

	void GenSquareUVs(int texX, int texY)
	{
		uvs.Add(new Vector2(texX * texScale, texY * texScale));
		uvs.Add(new Vector2(texX * texScale, texY * texScale + texScale));
		uvs.Add(new Vector2(texX * texScale + texScale, texY * texScale + texScale));
		uvs.Add(new Vector2(texX * texScale + texScale, texY * texScale));
	}

	void GenMesh()
	{
		filter.mesh.Clear();
		filter.mesh.vertices = verts.ToArray();
		filter.mesh.triangles = tris.ToArray();
		filter.mesh.uv = uvs.ToArray();
		filter.mesh.RecalculateNormals();

		//print("uv" + uvs.Count);
		//print("vert" + verts.Count);

		verts.Clear();
		tris.Clear();
		uvs.Clear();
	}
}
