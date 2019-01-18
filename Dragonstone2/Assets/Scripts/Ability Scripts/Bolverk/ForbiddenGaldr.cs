using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Forbidden Galdr:  Deal 100 damage to an enemy for each knowledge you have and heall all your allies for the same amount. 
//All knowledge will be consumed once you use this skill.


public class ForbiddenGaldr : Ability {

	public int damage;
	public int heal;

	

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	
	{
		LearnKnowledge learnKnowledge = gameObject.GetComponentInParent<HeroManager>().GetComponentInChildren<LearnKnowledge>();
	
		damage = learnKnowledge.knowledgePoints*100;
		heal = damage;

		GameManager.Instance.DealDamage(damage, attacker, defender);

		List<HeroManager> allies = GameManager.Instance.AllyHeroList(attacker);
		foreach(HeroManager ally in allies){
			GameManager.Instance.Heal(ally, heal);
		}

		learnKnowledge.knowledgePoints = 0;

		base.UseAbility(attacker, defender);

	}	

}
