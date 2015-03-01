using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TerrainGenerator : MonoBehaviour {
	public Texture2D heightMap;
	public Texture2D tileMap;

	private int height;
	private int width;

	MeshFilter filter;
	MeshCollider collider;

	List<Vector3> verts = new List<Vector3>();
	List<int> tris = new List<int>();
	List<Vector2> uvs = new List<Vector2>();

	float texScale = .125f;

	void Start()
	{
		filter = GetComponent<MeshFilter>();
		collider = GetComponent <MeshCollider>();
		width = heightMap.width;
		height = heightMap.height;
	}

	void FixedUpdate()
	{
		for (int x = 0; x < width; x++)
		{
			for (int z = 0; z < height; z++)
			{
				GenSquare(x, z, heightMap.GetPixel(x, z));
			}
		}

		GenMesh();
	}

	bool WithinColorRange(Color targetColor, Color inputColor)
	{
		if (Mathf.Abs(targetColor.r - inputColor.r) < 40f / 256f
			&& Mathf.Abs(targetColor.g - inputColor.g) < 40f/ 256f
			&& Mathf.Abs(targetColor.b - inputColor.b) < 40f/ 256f)
		{
			return true;
		}

		return false;
	}


	void GenSquare(int x, int z, Color c)
	{
		verts.Add(new Vector3(x, 0, z));
		verts.Add(new Vector3(x, 0, z+1));
		verts.Add(new Vector3(x+1, 0, z+1));
		verts.Add(new Vector3(x+1, 0 ,z));
		int texX = 0;
		int texY = 0;
		GenSquareUVs(texX,  texY);
		GenSquareTris();

		//Extrude upward
		if (WithinColorRange(new Color(161f / 256f ,161f / 256f,161f / 256f), c))
		{

			verts.Add(new Vector3(x, 1, z));
			verts.Add(new Vector3(x, 1, z + 1));
			verts.Add(new Vector3(x + 1, 1, z + 1));
			verts.Add(new Vector3(x + 1, 1, z));

			GenSquareUVs(4, 1);
			GenSquareTris();
		}

		if (WithinColorRange(new Color(70f / 256f, 44f / 256f, 105f / 256f), c))
		{
			verts.Add(new Vector3(x, 1, z));
			verts.Add(new Vector3(x, 1, z + 1));
			verts.Add(new Vector3(x + 1, 1, z + 1));
			verts.Add(new Vector3(x + 1, 1, z));

			GenSquareUVs(5, 1);
			GenSquareTris();
		}

        if (WithinColorRange(new Color(25f / 256f, 25f / 256f, 25f / 256f), c))
        {
            verts.Add(new Vector3(x, 1, z));
            verts.Add(new Vector3(x, 1, z + 1));
            verts.Add(new Vector3(x + 1, 1, z + 1));
            verts.Add(new Vector3(x + 1, 1, z));

            GenSquareUVs(6, 1);
            GenSquareTris();
        }

        if (WithinColorRange(new Color(255f / 256f, 230f / 256f, 3f / 256f), c))
        {
            verts.Add(new Vector3(x, 1, z));
            verts.Add(new Vector3(x, 1, z + 1));
            verts.Add(new Vector3(x + 1, 1, z + 1));
            verts.Add(new Vector3(x + 1, 1, z));

            GenSquareUVs(7, 1);
            GenSquareTris();
        }

        if (WithinColorRange(new Color(100f / 256f, 100f / 256f, 100f / 256f), c))
        {
            verts.Add(new Vector3(x, 1, z));
            verts.Add(new Vector3(x, 1, z + 1));
            verts.Add(new Vector3(x + 1, 1, z + 1));
            verts.Add(new Vector3(x + 1, 1, z));

            GenSquareUVs(3, 1);
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
