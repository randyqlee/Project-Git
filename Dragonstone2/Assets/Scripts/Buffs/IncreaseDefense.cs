using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDefense : Buff {


	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Increase Defense");

		//attach buf Icon to Hero UI
		this.buffIcon = buff.icon;

		//apply effect
		gameObject.GetComponent<HeroManager>().defense += buff.value;
		
	}

	// Use this for initialization
	// void Start () {
		
	// }
	
	// Update is called once per frame
	void Update () {
		
	}


	public override void OnDestroy()
	{
		//remove effect
		gameObject.GetComponent<HeroManager>().defense -= buff.value;

		//call parent OnDestroy
		base.OnDestroy();
	}
}
