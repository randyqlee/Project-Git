using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttack : Buff {


	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Counter Attack");


	}
	// Use this for initialization
	void Start () {
		
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
