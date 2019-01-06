using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Remove all debuffs on all allies and 
//recover HP to each ally equivalent to your attack power.

public class GirlsPrayer : Ability {

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

		base.UseAbility(attacker);

		

	}

}
