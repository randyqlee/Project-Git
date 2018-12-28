using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unhealable : Debuff {

	// Use this for initialization
	void Awake () {
		//get buff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Unhealable");

		//attach buff Icon to Hero UI
		this.debuffIcon = debuff.icon;

		//apply effect
		gameObject.GetComponent<HeroManager>().hasUnhealable = true;		
		
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnDestroy()
	{

		gameObject.GetComponent<HeroManager>().hasUnhealable = false;
		//call parent OnDestroy
		base.OnDestroy();
	}
}
