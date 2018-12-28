using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlucky : Debuff {

	// Use this for initialization
	void Awake () {

		//get debuff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Unlucky");

		//attach icon to Hero UI
		this.debuffIcon = debuff.icon;

		//apply effect
		gameObject.GetComponent<HeroManager>().chance -= debuff.value;
		
	}
	
	

	public override void OnDestroy(){

		//remove debuff effect
		gameObject.GetComponent<HeroManager>().chance += debuff.value;

		//call parent OnDestroy
		base.OnDestroy();
	}
}
