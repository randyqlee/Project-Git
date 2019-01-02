using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Attack an Enemy and Heal the damage dealt

public class VampireBat : Ability {	

	//FeastOfBlood feastOfBlood;

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		GameManager.Instance.Attack (attacker, defender);
		
		Button[] button = attacker.GetComponentsInChildren<Button>();
		Ability ability = button[2].GetComponent<Ability>();
		ability.UseAbility(attacker, defender);
		
		int damageHeal = GameManager.Instance.atk_damage;
		Debug.Log("Cadiz Vampiric Bat damage: " +damageHeal);

		GameManager.Instance.Heal(attacker, damageHeal);

		

		base.UseAbility(attacker, defender);		

	}
}
