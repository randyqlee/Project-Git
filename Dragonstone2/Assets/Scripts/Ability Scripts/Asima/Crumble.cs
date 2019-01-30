using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attack and deal critical strike to all enemies.  
//Decrease Defense and Inflict Poison for 2 turns.

public class Crumble : Ability {

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		//Attack and Deal Critical Strike to all enemies
		GameManager.Instance.AttackAllCritical(attacker, defender);

		
		//Deal decrease defense and Poison to all enemies
		List<HeroManager> enemies = GameManager.Instance.EnemyHeroList(attacker);
		int enemiesCount = enemies.Count;
		
		foreach(HeroManager enemy in enemies){
			GameManager.Instance.AddDebuffComponent("DecreaseDefense",2,attacker,defender);
			GameManager.Instance.AddDebuffComponent("Poison",2,attacker,defender);
		}		
		base.UseAbility(attacker);	
	}	
}
