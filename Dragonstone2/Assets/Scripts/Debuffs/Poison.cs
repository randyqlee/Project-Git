﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Debuff {

//increase attack damage by 50

	// Use this for initialization
	void Awake () {
		//get buff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Poison");

		//attach buf Icon to Hero UI
		this.debuffIcon = debuff.icon;

		
	}

	public override void DecreaseDuration()
	{
		if (GetComponentInParent<Player>().isActive)
		{
			gameObject.GetComponent<HeroManager>().maxHealth -= debuff.value;
		}

		base.DecreaseDuration();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable()
	{
		//gameObject.GetComponent<HeroManager>().attack += buff.value;
	}

	protected override void OnDestroy()
	{
		//remove effect

		//call parent OnDestroy
		base.OnDestroy();
	}

}
