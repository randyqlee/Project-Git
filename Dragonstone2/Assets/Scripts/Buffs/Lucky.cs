﻿using System.Collections;
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

		
	// Update is called once per frame
	void Update () {
		
	}


	public override void OnDestroy()
	{
		//remove effect
		gameObject.GetComponent<HeroManager>().chance -= buff.value;

		//call parent OnDestroy
		base.OnDestroy();
	}
}
