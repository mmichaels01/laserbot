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

	Texture2D rawTexture;
	WebCamTexture webCamTextureBot1;
    WebCamTexture webCamTextureBot2;
    WebCamTexture webCamTextureArena;
    WebCamDevice bot1Cam;
    WebCamDevice bot2Cam;
    WebCamDevice arenaCam;

	void Awake()
	{

	}

	// Use this for initialization
	void Start () {
        foreach (WebCamDevice cam in WebCamTexture.devices)
        {
            print(cam.name);
        }

        
        webCamTextureArena = new WebCamTexture( 128, 128);
        print(webCamTextureArena.width);
        webCamTextureBot1 = new WebCamTexture(laserBotString1,256,256);
        print(webCamTextureBot1.filterMode);
        //webCamTextureArena = new WebCamTexture("LaserBot1", 128, 128);

		//print(webcamTexture.height + " " +  webcamTexture.width);
		webCamTextureArena.Play();
        print(webCamTextureBot1.requestedHeight);
        print(webCamTextureBot1.requestedWidth);
      

	}

	//public static Texture2D LoadTextureFromStream(){

	//    Texture2D texture = new Texture2D(4, 4);
	//    NReco.VideoConverter.ConvertSettings settings = new NReco.VideoConverter.ConvertSettings();

	//    FileStream fStream = new FileStream("stream.jpg", FileMode.Open, FileAccess.Write);
	//    var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
	//    ffMpeg.GetVideoThumbnail(url, fStream);
	//    //ffMpeg.ConvertLiveMedia(url, NReco.VideoConverter.Format.mjpeg, "stream.jpg",NReco.VideoConverter.Format.mjpeg, settings);
	//    fStream.Close();

	//    fStream = new FileStream("stream.jpg", FileMode.Open, FileAccess.Read);
	//    byte[] imageData = new byte[fStream.Length];
	//    fStream.Read(imageData, 0,(int)fStream.Length);
	//    texture.LoadImage(imageData);
	//    fStream.Close();

	//    return texture;

	//}

	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
        
        print(webCamTextureBot1.width);
        print(webCamTextureBot1.height);
		
		Color32[] pixels = webCamTextureArena.GetPixels32();
		//print(pixels.Length);
		Texture2D texture = new Texture2D(webCamTextureArena.width, webCamTextureArena.height, TextureFormat.ARGB32,false);
		texture.SetPixels32(pixels);
		texture.Apply();


		GetComponentInChildren<RawImage>().texture = texture;
		GetComponentInChildren<RawImage>().SetNativeSize();

	}
}
