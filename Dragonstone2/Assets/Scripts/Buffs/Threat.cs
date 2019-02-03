using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Threat : Buff {


	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Threat");

		//attach buf Icon to Hero UI
		this.buffIcon = buff.icon;

		gameObject.GetComponent<HeroManager>().hasThreat = true;


	}
	
	public override void OnDestroy()
	{

		gameObject.GetComponent<HeroManager>().hasThreat = false;

		//call parent OnDestroy
		base.OnDestroy();
	}

}
