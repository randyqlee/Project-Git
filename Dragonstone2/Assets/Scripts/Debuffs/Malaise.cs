using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malaise : Debuff {


	// Use this for initialization
	void Awake () {
		//get buff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Malaise");

		//attach buff Icon to Hero UI
		this.debuffIcon = debuff.icon;

		//apply effect
		gameObject.GetComponent<HeroManager>().hasMalaise = true;		
		
	}

		
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnDestroy()
	{

		gameObject.GetComponent<HeroManager>().hasMalaise = false;
		//call parent OnDestroy
		base.OnDestroy();
	}
}
