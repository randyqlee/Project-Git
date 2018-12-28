using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCrush : Ability {


	int targetCount = 2;
	public override void UseAbility ()
	{

	}

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{


			Debug.Log ("Using  SuperCrush");

			targetCount = GameManager.Instance.EnemyHeroList(attacker).Count-1;
			if(targetCount <= 0) {
				targetCount = 1;
			}		

			GameManager.Instance.AttackAll (attacker, defender);

			base.UseAbilityRandom (attacker,defender,targetCount);
	

	}
}
