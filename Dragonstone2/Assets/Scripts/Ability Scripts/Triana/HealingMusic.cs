using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Healing Music: Remove all debuffs on an ally target then recover the HP of all allies by 150 and grants them immunity for 1 turn. 

public class HealingMusic : Ability {

public int healValue = 150;
	
public override void UseAbility(HeroManager attacker, HeroManager defender)
	{		
		//Destroy debuff of ally target
		Debuff[] debuffs = defender.GetComponents<Debuff>();
		if(debuffs.Length > 0){
			foreach(Debuff debuff in debuffs){
				Destroy(debuff);
			}
		}


		//Recover HP by 150 and grant immunity to all allies
		List<HeroManager> allies = GameManager.Instance.AllyHeroList(attacker);
		//inflict poison for 2 turns
		foreach(HeroManager ally in allies){

			GameManager.Instance.Heal(ally, healValue);
			//Debug.Log("Target Poisoned: " +enemy);
			GameManager.Instance.AddBuff("Immunity", 1, attacker, ally);			
		}			

		//chance to Stun all		
		base.UseAbility(attacker, defender);				

		

	}

}
