using UnityEngine;
using System.Collections;

public class GridSpace : MonoBehaviour {
	Renderer rend;

	void Start () {
		rend = GetComponent<Renderer>();
		rend.enabled = true;
	}

	public void Hide()
	{
		GetComponent<Renderer> ().material.color = Color.black;
	}

	void Show()
	{
	}
}