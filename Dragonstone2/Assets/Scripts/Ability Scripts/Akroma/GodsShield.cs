using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class  GodsShield : Ability {

	

	void Start(){

		PassiveSkillInitialization();
	}

	public override void PassiveSkillInitialization(){
		
		HeroManager hero;

		hero = GetComponentInParent<HeroManager>();
		//set permanent Immnunity
		hero.hasPermanentImmunity = true;

		base.PassiveSkillInitialization();

	}

}
