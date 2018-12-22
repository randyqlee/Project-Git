using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shield : Buff {

	//include listener for when hero takes damage

	int remainingShield;


	void Awake () {
		//get buff asset
		this.buff = Resources.Load<BuffAsset>("SO Assets/Buff/Shield");

		//attach buf Icon to Hero UI
		this.buffIcon = buff.icon;

		//apply effect

		gameObject.GetComponent<HeroManager>().shield += buff.value;

		gameObject.GetComponent<HeroManager>().e_TakeDamage += CheckShieldValue;

		
		
	}

	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
		
	}

	void CheckShieldValue()
	{
		remainingShield = gameObject.GetComponent<HeroManager>().shield;
		if (remainingShield <= 0)

		OnDestroy();

	}

	protected override void OnDestroy()
	{
		//remove effect
		if (remainingShield > 0)
			gameObject.GetComponent<HeroManager>().shield -= remainingShield;
			gameObject.GetComponent<HeroManager>().maxHealth -= remainingShield;
			gameObject.GetComponent<HeroManager>().UpdateUI();

		gameObject.GetComponent<HeroManager>().e_TakeDamage -= CheckShieldValue;

		//call parent OnDestroy
		base.OnDestroy();
	}

}
