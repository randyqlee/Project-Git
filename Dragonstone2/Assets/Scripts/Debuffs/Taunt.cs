using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : Debuff {

	// Use this for initialization
	
	void Awake () {
		//get buff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Taunt");

		//attach buff Icon to Hero UI
		this.debuffIcon = debuff.icon;

		//apply effect
		gameObject.GetComponent<HeroManager>().hasTaunt = true;
		
	}

	protected override void OnDestroy()
	{

		gameObject.GetComponent<HeroManager>().hasTaunt = false;

		//call parent OnDestroy
		base.OnDestroy();
	}
	
	
	// Update is called once per frame
	
}
