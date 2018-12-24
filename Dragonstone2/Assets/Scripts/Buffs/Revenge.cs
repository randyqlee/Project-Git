using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revenge : Buff {


	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Revenge");

		this.buffIcon = buff.icon;

		gameObject.GetComponent<HeroManager>().hasRevenge = true;

	}
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnDestroy()
	{

		gameObject.GetComponent<HeroManager>().hasRevenge = false;
		//call parent OnDestroy
		base.OnDestroy();
	}


}
