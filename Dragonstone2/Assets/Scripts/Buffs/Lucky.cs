using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lucky : Buff {

	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Lucky");

		//attach buf Icon to Hero UI
		this.buffIcon = buff.icon;

		//apply effect
		gameObject.GetComponent<HeroManager>().chance += buff.value;
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	protected override void OnDestroy()
	{
		//remove effect
		gameObject.GetComponent<HeroManager>().chance -= buff.value;

		//call parent OnDestroy
		base.OnDestroy();
	}
}
