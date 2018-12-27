using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroIcon : MonoBehaviour {

	public HeroAsset heroAsset;
	public Image heroImage;

	// Use this for initialization
	void Start () {

		heroImage.sprite = heroAsset.image;


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
