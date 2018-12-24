using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Buff {


	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Defender");

		//attach buf Icon to Hero UI
		this.buffIcon = buff.icon;

		gameObject.GetComponent<HeroManager>().hasDefender = true;


	}
	
	public override void OnDestroy()
	{

		gameObject.GetComponent<HeroManager>().hasDefender = false;

		//call parent OnDestroy
		base.OnDestroy();
	}

}
