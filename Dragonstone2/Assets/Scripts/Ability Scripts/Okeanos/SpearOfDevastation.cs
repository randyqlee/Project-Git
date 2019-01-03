using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Critical Attack an enemy, remove a buff, and set all of its skills to MAX cooldown.


public class SpearOfDevastation : Ability {

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		List<HeroManager> allies = GameManager.Instance.AllyHeroList(attacker);

		foreach(HeroManager ally in allies ){
			//recover HP to each ally 
			GameManager.Instance.Heal(ally, attacker.attack);

			//Destroy debuffs on all allies
			Debuff[] debuffs = ally.GetComponents<Debuff>();
			foreach(Debuff debuff in debuffs){				
				debuff.OnDestroy();
			}//foreach debuff
		}//foreach ally

		base.UseAbility();

		

	}

}
