using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCanvas : MonoBehaviour {

	public RectTransform canvas;

	void Awake ()
	{

		//force the Skill Canvas to the bottom right of the screen, regardless of where the parent hero transform
		canvas.GetComponent<RectTransform>().position = new Vector3 ( 16.116f, -10.504f, 0f );

	}

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
