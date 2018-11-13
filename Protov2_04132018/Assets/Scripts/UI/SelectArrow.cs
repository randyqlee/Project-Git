using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectArrow : MonoBehaviour {

	public Image image;

	// Use this for initialization
	void Awake ()
	{
		image.gameObject.SetActive(false);
		GlobalATB.e_EndTurn += HideArrow;	
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowArrow()
	{
		image.gameObject.SetActive(true);
	
	}

	public void HideArrow()
	{
		image.gameObject.SetActive(false);
	}
}
