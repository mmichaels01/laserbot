using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class TextureManager : MonoBehaviour {

	public static string laserBotURL1 = "192.168.0.150:8090/test.mjpg";
	public static string laserBotURL2 = "192.168.0.150:8090/test.mjpg";
	public static string arenaURL = "192.168.0.159:8090";

	public static string laserBotString1 = "LaserBot1";
	public static string laserBotString2 = "LaserBot2";
	public static string laserBotArena = "LaserBotArena";
	public static string localCam = "HD Webcam C615";

	WebCamTexture webCamTextureBot1;
	WebCamTexture webCamTextureBot2;
	WebCamTexture webCamTextureArena;
	WebCamDevice bot1Cam;
	WebCamDevice bot2Cam;
	WebCamDevice arenaCam;

	RawImage textureArenaComponent;
	RawImage textureBotComponent;
	Text text;



	void Awake()
	{

	}

	// Use this for initialization
	void Start () {


		
		webCamTextureArena = new WebCamTexture(WebCamTexture.devices[0].name, 128, 128);

		webCamTextureBot1 = new WebCamTexture(laserBotString1,256,256);

		webCamTextureArena.Play();
		webCamTextureBot1.Play();

		textureArenaComponent = GameObject.Find("WebCamTextureArena").GetComponent<RawImage>();
		textureBotComponent = GameObject.Find("WebCamTexturePoV").GetComponent<RawImage>();
		text = GameObject.Find("Text").GetComponent<Text>();

		foreach (WebCamDevice cam in WebCamTexture.devices)
		{
			text.text += cam.name + "\n";
		}

	}

	void FixedUpdate()
	{
		Color32[] pixels = webCamTextureArena.GetPixels32();

		
		Texture2D textureArena = new Texture2D(webCamTextureArena.width, webCamTextureArena.height, TextureFormat.ARGB32,false);
		textureArena.SetPixels32(pixels);
		textureArena.Apply();
		textureArenaComponent.texture = textureArena;




		pixels = webCamTextureArena.GetPixels32();

		Texture2D textureBot = new Texture2D(webCamTextureBot1.width, webCamTextureBot1.height, TextureFormat.ARGB32, false);
		textureArena.SetPixels32(pixels);
		textureArena.Apply();
		textureBotComponent.texture = textureBot;


	}
}
