using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oblivion : Debuff {

	// Use this for initialization
	
	void Awake () {
		//get buff asset
		this.debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Oblivion");

		//attach buff Icon to Hero UI
		this.debuffIcon = debuff.icon;

		HeroManager hero = GetComponentInParent<HeroManager>();
		//apply effect
		hero.transform.Find("HeroPanel(Clone)").gameObject.SetActive(true);
								
				Ability[] abilities = hero.GetComponentsInChildren<Ability>();
				foreach(Ability ability in abilities){
					
					// if(ability.skillType == Type.Active)					
					//  ability.DisableAbilityActive();

					// if(ability.skillType == Type.Passive)					
					//  ability.DisableAbilityPassive();

					 ability.DisableAbilityActive();
					 ability.DisableAbilityPassive();
				}
				
			hero.transform.Find("HeroPanel(Clone)").gameObject.SetActive(false);
		
		
	}

	public override void OnDestroy()
	{

		HeroManager hero = GetComponentInParent<HeroManager>();
		//apply effect
		hero.transform.Find("HeroPanel(Clone)").gameObject.SetActive(true);
								
				Ability[] abilities = hero.GetComponentsInChildren<Ability>();
				foreach(Ability ability in abilities){
					
					if(ability.skillType == Type.Passive)	 
					 ability.UseAbilityPassive();

					if(ability.skillType == Type.Active)	
					 ability.UseAbilityActive();
				}
				
			hero.transform.Find("HeroPanel(Clone)").gameObject.SetActive(false);

		//call parent OnDestroy
		base.OnDestroy();
	}
	
	
	// Update is called once per frame
	
}
