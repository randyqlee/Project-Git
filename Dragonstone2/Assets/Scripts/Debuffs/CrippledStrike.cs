using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrippledStrike : Debuff {

	//Decrease attack damage by 40 and can't implement Debuffs

	// Use this for initialization
	void Awake () {
		
		//get debuff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Crippled Strike");

		//attach debuff Icon to Hero UI
		this.debuffIcon = debuff.icon;

		//apply effect
		gameObject.GetComponent<HeroManager>().attack -=debuff.value;
		gameObject.GetComponent<HeroManager>().hasCrippledStrike = true;

		//Logic implementation is in GameManager
	
	}

	public override void OnDestroy()	{

		gameObject.GetComponent<HeroManager>().attack +=debuff.value;
		gameObject.GetComponent<HeroManager>().hasCrippledStrike = false;

		//call parent OnDestroy
		base.OnDestroy();

	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
