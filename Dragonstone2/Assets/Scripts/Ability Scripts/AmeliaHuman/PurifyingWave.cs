using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Purifying Wave: Remove all debuffs on all allies and increases their Defense for 2 turns.

public class PurifyingWave : Ability 
{
	public override void UseAbility (HeroManager attacker, HeroManager defender)
		{	
			
			List<HeroManager> allies = GameManager.Instance.AllyHeroList(attacker);
			foreach(HeroManager ally in allies){

				//increase defense
				GameManager.Instance.AddBuff("IncreaseDefense", 2, attacker, ally);

				//remove all debuffs
				Debuff[] debuffs = ally.GetComponents<Debuff>();
				if(debuffs.Length > 0){
					foreach(Debuff debuff in debuffs){
						Destroy(debuff);
					}//foreach debuff
				}//if debuffs
								
			}//foreach HeroManager

			base.UseAbility(attacker,defender);

		}//UseAbility

}//class


