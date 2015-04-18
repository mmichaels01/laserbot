using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
	int gridWidth = 8;
	int gridHeight = 6;

	int size = 20;

	int showPath = 1;

	List<GameObject> spaces = new List<GameObject> ();

	void Start ()
	{
		for (int i = 0; i < gridWidth; i++)
		{
			if (i == 4) continue;
			for (int z = 0; z < gridHeight; z++)
			{
				spaces.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
				spaces[spaces.Count-1].transform.position = new Vector3(size * i + size / 2, 0.5f, size * z + size / 2);
				spaces[spaces.Count-1].transform.localScale = new Vector3(size, 0.5f, size);
				spaces[spaces.Count-1].AddComponent<GridSpace>();
			}
		}
	}
	void FixedUpdate ()
	{
		if (showPath == 0)
		{
			for (int i = 0; i < spaces.Count; i++) {
				GridSpace temp = (GridSpace)spaces[i].gameObject.GetComponent (typeof(GridSpace));
				temp.Hide();
			}
		}
		else
		{
			showPath++;
			if (showPath > 200)
				showPath = 0;
		}
	}
}