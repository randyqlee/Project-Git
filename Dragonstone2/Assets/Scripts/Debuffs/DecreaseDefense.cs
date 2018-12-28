using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseDefense : Debuff {

	

	// Use this for initialization
	void Awake () {

		//get buff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Decrease Defense");

		//attach buff icon to Hero UI
		this.debuffIcon = debuff.icon;
		
		//apply effect
		gameObject.GetComponent<HeroManager>().defense -= debuff.value;		
	}

		
	// Update is called once per frame
	void Update () {
		
	}


	public override void OnDestroy(){

		//remove effect
		gameObject.GetComponent<HeroManager>().defense += debuff.value;	

		//call parent OnDestroy
		base.OnDestroy();

	}//OnDestroy
}
