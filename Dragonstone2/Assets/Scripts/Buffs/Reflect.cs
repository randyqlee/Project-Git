using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : Buff {


	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Reflect");

		this.buffIcon = buff.icon;


	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnDestroy()
	{

		//call parent OnDestroy
		base.OnDestroy();
	}
}
