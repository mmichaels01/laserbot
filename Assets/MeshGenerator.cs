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

    public GameObject obj;
    public int xCoord;
    public int zCoord;
    public int yCoord;

    public float r = .1f;
    public float g = .1f;
    public float b = .1f;

    public float colorComparisonStrength = 50f;
    public bool[,] coordinateStateArray;


    public int chunkWidth;
    public int chunkHeight;
    public int arenaWidth;
    public int arenaHeight;

    #region Constructors
    public MeshGenerator(float texScale)
    {
        this.texScale = texScale;
    }

    public MeshGenerator(float texScale, GameObject obj, bool[,] coordinateStateArray, int chunkWidth, int chunkHeight, int arenaWidth, int arenaHeight)
    {
        this.texScale = texScale;
        this.obj = obj;
        this.filter = obj.GetComponent<MeshFilter>();
        this.collider = obj.GetComponent<MeshCollider>();
        this.xCoord = Mathf.RoundToInt(obj.transform.position.x);
        this.yCoord = Mathf.RoundToInt(obj.transform.position.y);
        this.zCoord = Mathf.RoundToInt(obj.transform.position.z);
        this.coordinateStateArray = coordinateStateArray;

        this.chunkWidth = chunkWidth;
        this.chunkHeight = chunkHeight;

        this.arenaWidth = arenaWidth;
        this.arenaHeight = arenaHeight;
    }

    public MeshGenerator(float texScale,  MeshFilter filter, MeshCollider collider, int arenaWidth, int arenaHeight)
    {
        this.texScale = texScale;
        this.filter = filter;
        this.collider = collider;
        this.arenaHeight = arenaHeight;
        this.arenaWidth = arenaWidth;
    }

    public MeshGenerator(float texScale, MeshFilter filter, MeshCollider collider, GameObject obj)
    {
        this.texScale = texScale;
        this.filter = filter;
        this.collider = collider;

        this.obj = obj;
        this.xCoord = Mathf.RoundToInt(obj.transform.position.x);
        this.yCoord = Mathf.RoundToInt(obj.transform.position.y);
        this.zCoord = Mathf.RoundToInt(obj.transform.position.z);
    }

    #endregion Constructors

    public void GenerateWallsFromTexture(Texture2D t, int width, int height, int wallHeight, bool renderCollision = false){

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GenerateCube(x * scale, wallHeight * scale, y * scale, t.GetPixel(xCoord + x, zCoord + y), new Color(r, g, b));
            }
        }

        GenerateMesh();

    }

    public void GenerateWallsFromBoolArray(int width, int height, bool renderCollision = false)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if(coordinateStateArray[i + xCoord, j + zCoord]){
                    GenerateHorizontalBox(i, 0 , j);
                }
            }
        }

        GenerateMeshNoTexture(renderCollision);
    }




    public void GenerateFloor(Texture2D t, int x, int y, int z, bool renderCollision = true)
    {
            verts.Add(new Vector3(x, y, z));
            verts.Add(new Vector3(x, y, z + arenaHeight));
            verts.Add(new Vector3(x + arenaWidth, y, z + arenaHeight));
            verts.Add(new Vector3(x + arenaWidth, y, z));

            GenerateSquareUVs(0, 0, 1);
            GenerateSquareTris();

            GenerateMesh();  
    }

    //Old individual cube generation
    public void GenerateCube(int x, int y, int z,Color inputColor, Color targetColor)
    {
        if (WithinColorRange(targetColor, inputColor, colorComparisonStrength))
        {

            //Top
            verts.Add(new Vector3(x, y + scale, z));
            verts.Add(new Vector3(x, y + scale, z + scale));
            verts.Add(new Vector3(x + scale, y + scale, z + scale));
            verts.Add(new Vector3(x + scale, y + scale, z));

            GenerateSquareUVs(1, 2, texScale);
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
            verts.Add(new Vector3(x, y + scale, z));
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

    public void GenerateCube(int x, int y, int z)
    {
            //Top
            verts.Add(new Vector3(x, y + scale, z));
            verts.Add(new Vector3(x, y + scale, z + scale));
            verts.Add(new Vector3(x + scale, y + scale, z + scale));
            verts.Add(new Vector3(x + scale, y + scale, z));

            GenerateSquareUVs(1, 2, texScale);
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
            verts.Add(new Vector3(x, y + scale, z));
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

    void GenerateHorizontalBox(int xStart, int yStart, int zStart){
        int xEnd = xStart;
        int yEnd = yStart;
        int zEnd = zStart;


        //128 will "leak" over into neighboring chunks, setting 128 to xStart + chunkSize will contain it to its current chunk
        //This is accomplished because this loop will continue turning off until the edge of the arena rather than the edge of the chunk
        while (xEnd + xCoord < arenaWidth && coordinateStateArray[xEnd + xCoord, zEnd + zCoord])
        {
            coordinateStateArray[xEnd + xCoord, zEnd + zCoord] = false;
            xEnd++;
        }

        //Top
        verts.Add(new Vector3(xStart, yStart + scale, zStart));
        verts.Add(new Vector3(xStart, yStart + scale, zStart + scale));
        verts.Add(new Vector3(xEnd, yStart + scale, zStart + scale));
        verts.Add(new Vector3(xEnd, yStart + scale, zStart));

        GenerateSquareTris();

        //Front
        verts.Add(new Vector3(xStart, yStart, zStart));
        verts.Add(new Vector3(xStart, yStart + scale, zStart));
        verts.Add(new Vector3(xEnd, yStart + scale, zStart));
        verts.Add(new Vector3(xEnd, yStart, zStart));

        GenerateSquareTris();

        //Left
        verts.Add(new Vector3(xStart, yStart, zStart + scale));
        verts.Add(new Vector3(xStart, yStart + scale, zStart + scale));
        verts.Add(new Vector3(xStart, yStart + scale, zStart));
        verts.Add(new Vector3(xStart, yStart, zStart));

        GenerateSquareTris();

        //Back
        verts.Add(new Vector3(xEnd, yStart, zStart + scale));
        verts.Add(new Vector3(xEnd, yStart + scale, zStart + scale));
        verts.Add(new Vector3(xStart, yStart + scale, zStart + scale));
        verts.Add(new Vector3(xStart, yStart, zStart + scale));

        GenerateSquareTris();

        //Right
        verts.Add(new Vector3(xEnd, yStart, zStart));
        verts.Add(new Vector3(xEnd, yStart + scale, zStart));
        verts.Add(new Vector3(xEnd, yStart + scale, zStart + scale));
        verts.Add(new Vector3(xEnd, yStart, zStart + scale));

        GenerateSquareTris();
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

    void GenerateMeshNoTexture(bool useCollider = false)
    {
        var vertsArr = verts.ToArray();
        var trisArr = tris.ToArray();
        filter.mesh.Clear();
        filter.mesh.vertices = vertsArr;
        filter.mesh.triangles = trisArr;
        filter.mesh.RecalculateNormals();

        if(useCollider){
            collider.sharedMesh = null;
            var m = new Mesh();
            m.vertices = vertsArr;
            m.triangles = trisArr;
            m.RecalculateNormals();
            collider.sharedMesh = m;
        }

        verts.Clear();
        tris.Clear();
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
