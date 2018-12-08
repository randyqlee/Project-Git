using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseDefense : Buff {

	

	// Use this for initialization
	void Awake () {

		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Debuff/Decrease Defense");

		//attach buff icon to Hero UI
		this.buffIcon = buff.icon;
		
		//apply effect
		gameObject.GetComponent<HeroManager>().defense -= buff.value;		
	}

		
	// Update is called once per frame
	void Update () {
		
	}


	protected override void OnDestroy(){

		//remove effect
		gameObject.GetComponent<HeroManager>().defense += buff.value;	

		//call parent OnDestroy
		base.OnDestroy();

	}//OnDestroy
}
