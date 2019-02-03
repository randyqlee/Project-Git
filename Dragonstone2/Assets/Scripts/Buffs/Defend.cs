using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : Buff {


	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Defend");

		//attach buf Icon to Hero UI
		this.buffIcon = buff.icon;

		gameObject.GetComponent<HeroManager>().hasDefend = true;


	}
	
	public override void OnDestroy()
	{

		gameObject.GetComponent<HeroManager>().hasDefend = false;

		//call parent OnDestroy
		base.OnDestroy();
	}

}
