using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroIcon : MonoBehaviour {

	public HeroAsset heroAsset;

	// Use this for initialization
	void Start () {

		this.GetComponent<Image>().sprite = heroAsset.image;


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
