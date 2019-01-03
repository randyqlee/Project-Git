using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPreview : MonoBehaviour {


	void OnMouseEnter()
	{
		//Debug.Log ("Mouse Enter");
		gameObject.transform.Find("HoverImage").gameObject.SetActive(true);
	
	}

	void OnMouseExit()
	{
		//Debug.Log ("Mouse Exit");
		gameObject.transform.Find("HoverImage").gameObject.SetActive(false);

	
	}


}
