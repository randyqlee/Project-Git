using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DecreaseAttack : Buff {



//Decrease attack damage by 60	
int temp;

void Awake (){

	//get debuff asset
	this.buff = Resources.Load<BuffAsset>("SO Assets/Debuff/Decrease Attack");

	//attach debuff Icon to Hero UI
	this.buffIcon = buff.icon;

	//apply effect
	gameObject.GetComponent<HeroManager>().attack -= buff.value;

	
	}//Awake

	void Update(){}

	void OnEnable(){}
            
	protected override void OnDestroy()
	{

		
		//remove effect	
		gameObject.GetComponent<HeroManager>().attack += buff.value;

		//call parent OnDestroy
		base.OnDestroy();

	}


	

}//Class
