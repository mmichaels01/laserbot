using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshGenerator  {
    public List<Vector3> verts = new List<Vector3>();
    public List<int> tris = new List<int>();
    public List<Vector2> uvs = new List<Vector2>();
    public float texScale;

    MeshFilter filter;
    MeshCollider collider;
    int scale = 1;


    public MeshGenerator(float texScale)
    {
        this.texScale = texScale;

    }

    public MeshGenerator(float texScale,  MeshFilter filter, MeshCollider collider)
    {
        this.texScale = texScale;

        this.filter = filter;
        this.collider = collider;
    }

    public void GenearateWallsFromTexture(Texture2D t, int width, int height, int wallHeight, bool renderCollision = false){

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GenerateCube(x * scale, wallHeight * scale, y * scale, t.GetPixel(x, y),new Color(0f / 256f, 0f / 256f, 0f / 256f));
            }
        }

        GenerateMesh();

    }


    public void GenerateFloor(Texture2D t, int x, int y, int z, bool renderCollision = true, int scale = 1)
    {


            verts.Add(new Vector3(x, y, z));
            verts.Add(new Vector3(x, y, z + scale));
            verts.Add(new Vector3(x + scale, y, z + scale));
            verts.Add(new Vector3(x + scale, y, z));

            GenerateSquareUVs(0, 0, 1);
            GenerateSquareTris();

            GenerateMesh();
        
    }

    public void GenerateCube(int x, int y, int z,Color inputColor, Color targetColor)
    {
        if (WithinColorRange(targetColor, inputColor, 64))
        {

            //Top
            verts.Add(new Vector3(x, y + scale, z));
            verts.Add(new Vector3(x, y + scale, z + scale));
            verts.Add(new Vector3(x + scale, y + scale, z + scale));
            verts.Add(new Vector3(x + scale, y + scale, z));

            GenerateSquareUVs(0, 2, texScale);
            GenerateSquareTris();

            //Front
            verts.Add(new Vector3(x, y, z));
            verts.Add(new Vector3(x, y + scale, z));
            verts.Add(new Vector3(x + scale, y + scale, z));
            verts.Add(new Vector3(x + scale, y, z));

            GenerateSquareUVs(1, 2, texScale);
            GenerateSquareTris();

            //Left
            verts.Add(new Vector3(x, y, z + scale));
            verts.Add(new Vector3(x, y + scale, z + scale));
            verts.Add(new Vector3(x , y + scale, z));
            verts.Add(new Vector3(x, y, z));

            GenerateSquareUVs(1, 2, texScale);
            GenerateSquareTris();

            //Back
            verts.Add(new Vector3(x + scale, y, z + scale));
            verts.Add(new Vector3(x + scale, y + scale, z + scale));
            verts.Add(new Vector3(x, y + scale, z + scale));
            verts.Add(new Vector3(x, y, z + scale));

            GenerateSquareUVs(1, 2, texScale);
            GenerateSquareTris();

            //Right
            verts.Add(new Vector3(x + scale, y, z));
            verts.Add(new Vector3(x + scale, y + scale, z));
            verts.Add(new Vector3(x + scale, y + scale, z + scale));
            verts.Add(new Vector3(x + scale, y, z + scale));

            GenerateSquareUVs(1, 2, texScale);
            GenerateSquareTris();
        }
    }

    void GenerateSquareUVs(float texX, float texY, float texScale)
    {
        uvs.Add(new Vector2(texX * texScale, texY * texScale));
        uvs.Add(new Vector2(texX * texScale, texY * texScale + texScale));
        uvs.Add(new Vector2(texX * texScale + texScale, texY * texScale + texScale));
        uvs.Add(new Vector2(texX * texScale + texScale, texY * texScale));
    }

    void GenerateSquareTris()
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

    void GenerateMesh()
    {
        filter.mesh.Clear();
        filter.mesh.vertices = verts.ToArray();
        filter.mesh.triangles = tris.ToArray();
        filter.mesh.uv = uvs.ToArray();
        filter.mesh.RecalculateNormals();

        verts.Clear();
        tris.Clear();
        uvs.Clear();
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
}
