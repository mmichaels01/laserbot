using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.IO;

public class WebcamTexture : MonoBehaviour {

	public static string url = "192.168.0.150:8090/test.mjpg";
	Texture2D raw;

	void Awake()
	{

	}

	// Use this for initialization
	void Start () {
		raw = GetComponentInChildren<RawImage>().mainTexture as Texture2D;

		print("starting coroutine");



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
}
