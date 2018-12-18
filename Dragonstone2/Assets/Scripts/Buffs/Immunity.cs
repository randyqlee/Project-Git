using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immunity : Buff {


	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Immunity");

		this.buffIcon = buff.icon;

		gameObject.GetComponent<HeroManager>().hasImmunity = true;
		
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	protected override void OnDestroy()
	{

		gameObject.GetComponent<HeroManager>().hasImmunity = false;
		//call parent OnDestroy
		base.OnDestroy();
	}

}
