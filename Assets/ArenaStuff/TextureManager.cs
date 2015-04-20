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
	WebCamTexture webCamTexturePlayer;
	WebCamDevice bot1Cam;
	WebCamDevice bot2Cam;
	WebCamDevice arenaCam;


	public GameObject arenaTextureObject;
	public GameObject bot1TextureObject;
	public GameObject bot2TextureObject;
	public GameObject cameraTextObject;
	public GameObject playerTextureObject;

	RawImage textureArenaComponent;
	RawImage textureBot1Component;
	RawImage textureBot2Component;
	RawImage texturePlayerComponent;
	Text text;


	// Use this for initialization
	void Start () {



        webCamTextureArena = new WebCamTexture("Arena", 160, 120);
        //webCamTextureBot1 = new WebCamTexture("LaserBot1", 256, 256);
		//webCamTextureBot2 = new WebCamTexture(WebCamTexture.devices[2].name, 256, 256);
		//webCamTexturePlayer = new WebCamTexture(WebCamTexture.devices[2].name, 256, 256);

		webCamTextureArena.Play();
        //webCamTextureBot1.Play();
		//webCamTextureBot2.Play();
		//webCamTexturePlayer.Play();

		textureArenaComponent = arenaTextureObject.GetComponent<RawImage>();
		textureBot1Component = bot1TextureObject.GetComponent<RawImage>();
		textureBot2Component = bot2TextureObject.GetComponent<RawImage>();
		texturePlayerComponent = playerTextureObject.GetComponent<RawImage>();
		text = cameraTextObject.GetComponent<Text>();


		foreach (WebCamDevice cam in WebCamTexture.devices)
		{
			text.text += cam.name + "\n";
		}
	}

	void FixedUpdate()
	{
		//Color32[] pixelsArena = webCamTextureArena.GetPixels32();
		//Texture2D textureArena = new Texture2D(webCamTextureArena.width, webCamTextureArena.height, TextureFormat.ARGB32, false);
		//textureArena.SetPixels32(pixelsArena);
		//textureArena.Apply();
		//textureArenaComponent.texture = textureArena;

        textureBot1Component.texture = webCamTextureBot1;
		textureArenaComponent.texture = webCamTextureArena;

		//textureBot1Component.texture = webCamTextureBot1;
		//textureBot2Component.texture = webCamTextureBot2;
		//texturePlayerComponent.texture = webCamTexturePlayer;
	}
}
