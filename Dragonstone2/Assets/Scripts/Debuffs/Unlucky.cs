using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlucky : Debuff {

	// Use this for initialization
	void Start () {

		//get debuff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Unlucky");

		//apply effect
		gameObject.GetComponent<HeroManager>().chance -= debuff.value;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnDestroy(){

		//remove debuff effect
		gameObject.GetComponent<HeroManager>().chance += debuff.value;

		//call parent OnDestroy
		base.OnDestroy();
	}
}
