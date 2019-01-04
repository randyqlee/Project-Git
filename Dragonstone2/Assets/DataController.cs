using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class DataController : MonoBehaviour {

	private string gameDataProjectFilePath = "/StreamingAssets/data.json";

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //public void SaveGameData(List<HeroAsset> heroAsset)
	public void SaveGameData(HeroAsset heroAsset)
    {
	/*	
		HeroAsset[] heroArray = new HeroAsset[4];
		for (int i=0; i<heroAsset.Count; i++)
		{
			heroArray[i]=heroAsset[i];
		}
		*/



        string dataAsJson = JsonUtility.ToJson (heroAsset);
		//string dataAsJson = JsonHelper.ToJson (heroArray);

		Debug.Log(dataAsJson);

        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText (filePath, dataAsJson);

    }
}
