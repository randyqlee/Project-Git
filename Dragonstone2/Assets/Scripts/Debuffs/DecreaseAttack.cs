using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DecreaseAttack : Debuff {



//Decrease attack damage by 60	


void Awake (){

	//get debuff asset
	this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Decrease Attack");

	//attach debuff Icon to Hero UI
	this.debuffIcon = debuff.icon;

	//apply effect
	gameObject.GetComponent<HeroManager>().attack -= debuff.value;

	
	}//Awake

	void Update(){}

	void OnEnable(){}
            
	public override void OnDestroy()
	{

		
		//remove effect	
		gameObject.GetComponent<HeroManager>().attack += debuff.value;

		//call parent OnDestroy
		base.OnDestroy();

	}


	

}//Class
