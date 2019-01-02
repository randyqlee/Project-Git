using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Brand : Debuff {

//Deal additional 30 damage whenever target takes damage.

void Awake (){

	//get debuff asset
	this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Brand");

	//attach debuff Icon to Hero UI
	this.debuffIcon = debuff.icon;

	//apply effect
	gameObject.GetComponent<HeroManager>().hasBrand = true;

	
	}//Awake

	void Update(){}

	void OnEnable(){}
            
	public override void OnDestroy()
	{

		
		//remove effect	
		gameObject.GetComponent<HeroManager>().hasBrand = false;

		//call parent OnDestroy
		base.OnDestroy();

	}


	

}//Class
