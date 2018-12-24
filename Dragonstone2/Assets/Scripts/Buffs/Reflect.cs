using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : Buff {


	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Reflect");

		this.buffIcon = buff.icon;

		gameObject.GetComponent<HeroManager>().hasReflect = true;


	}
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnDestroy()
	{

		gameObject.GetComponent<HeroManager>().hasReflect = false;
		//call parent OnDestroy
		base.OnDestroy();
	}
}
