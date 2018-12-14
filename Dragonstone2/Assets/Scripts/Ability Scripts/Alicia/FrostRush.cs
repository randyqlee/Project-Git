using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrostRush : Ability {


	bool heroKilled;
	

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		if (!GameManager.Instance.isTurnPaused)
		{
			GameManager.Instance.e_HeroKilled += AnotherTurn;
			GameManager.Instance.AttackAll (attacker, defender);
			GameManager.Instance.e_HeroKilled -= AnotherTurn;
		}
		else
		{
			GameManager.Instance.isTurnPaused = false;
			GameManager.Instance.AttackAll (attacker, defender);
		}
	}

	void AnotherTurn()
	{
		GameManager.Instance.isTurnPaused = true;
	}



}
