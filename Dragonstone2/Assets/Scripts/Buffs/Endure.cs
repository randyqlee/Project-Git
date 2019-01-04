using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endure : Buff {


	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Endure");

		this.buffIcon = buff.icon;

		gameObject.GetComponent<HeroManager>().hasEndure = true;


	}

	//Skill effect to be applied in CheckHealth
		

	public override void OnDestroy()
	{

		gameObject.GetComponent<HeroManager>().hasEndure = false;
		//call parent OnDestroy
		base.OnDestroy();
	}
}
