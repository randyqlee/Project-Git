using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiBuff : Debuff {

	// Use this for initialization
	
	void Awake () {
		//get buff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/AntiBuff");

		//attach buff Icon to Hero UI
		this.debuffIcon = debuff.icon;

		//apply effect
		gameObject.GetComponent<HeroManager>().hasAntiBuff = true;
		
	}

	public override void OnDestroy()
	{

		gameObject.GetComponent<HeroManager>().hasAntiBuff = false;

		//call parent OnDestroy
		base.OnDestroy();
	}
	
	
	// Update is called once per frame
	
}
