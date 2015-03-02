using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class TextureManager : MonoBehaviour {

	public static string url = "192.168.0.150:8090/test.mjpg";
	Texture2D rawTexture;
	WebCamTexture webcamTexture;
	WebCamDevice webcam;

	void Awake()
	{

	}

	// Use this for initialization
	void Start () {

		WebCamDevice[] webcams = WebCamTexture.devices;
		webcam = webcams[0];
		webcamTexture = new WebCamTexture(160,120);
		print(webcamTexture.height + " " +  webcamTexture.width);
		webcamTexture.Play();
		foreach (var device in webcams)
		{
			print(device.name);
		}
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
		
		Color32[] pixels = webcamTexture.GetPixels32();
		print(pixels.Length);
		Texture2D texture = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.ARGB32,false);
		texture.SetPixels32(pixels);
		texture.Apply();


		GetComponentInChildren<RawImage>().texture = texture;
		GetComponentInChildren<RawImage>().SetNativeSize();

	}
}
