﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Passing Time:  Attack an enemy with a chance for critical strike.  
//Gain lucky for 2 turns if you land a critical strike.

public class PassingTime : Ability {

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	
	{
		if(GameManager.Instance.IsChanceSuccess(attacker)){

			//chance to critical
			GameManager.Instance.AttackCritical(attacker, defender);

			//gain lucky
			GameManager.Instance.AddBuff("Lucky", 2, attacker, attacker);
		}

		base.UseAbility(attacker);		

	}	

}
