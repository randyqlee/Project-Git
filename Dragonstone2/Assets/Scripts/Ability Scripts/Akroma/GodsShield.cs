using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class  GodsShield : Ability {

	float chanceReduction = 5;
	HeroManager hero;

	// void Start(){

	// 	UseAbilityPassive();
	// }

	public override void UseAbilityPassive(){		
		

		hero = GetComponentInParent<HeroManager>();
		//apply effect
		hero.hasPermanentImmunity = true;

		List<HeroManager> enemies =  GameManager.Instance.EnemyHeroList(hero);
		foreach (HeroManager enemy in enemies){
			enemy.chance -= chanceReduction;
		}


		base.UseAbilityPassive();

	}

	public override void DisableAbilityPassive(){

		hero = GetComponentInParent<HeroManager>();
		//apply effect
		
		List<HeroManager> enemies =  GameManager.Instance.EnemyHeroList(hero);
		foreach (HeroManager enemy in enemies){
		
			enemy.chance += chanceReduction;

		base.DisableAbilityPassive();

		}

	}

}
