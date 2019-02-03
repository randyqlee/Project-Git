using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraTurn : Buff {

	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Extra Turn");

		//attach buf Icon to Hero UI
		this.buffIcon = buff.icon;	

		HeroManager hero = this.GetComponentInParent<HeroManager>();
		

		if(GameManager.Instance.IsChanceSuccess(hero)){
			GameManager.Instance.isTurnPaused = true;
			hero.hasExtraTurn = true;
			
		}

		
		
	}

		
	// Update is called once per frame
	void Update () {
		
	}


	public override void OnDestroy()
	{
		
		//call parent OnDestroy
		base.OnDestroy();
	}
}
