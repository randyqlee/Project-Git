using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCrush : Ability {


	int targetCount;	

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{


			Debug.Log ("Using  SuperCrush");

			targetCount = GameManager.Instance.EnemyHeroList(attacker).Count;			

			GameManager.Instance.AttackAll (attacker, defender);

			base.UseAbilityRandom (attacker,defender,targetCount);
	

	}
}
