using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Torrent: Attack an enemy, if your HP is below 250, Ignore enemy defense.  
//Attacks deal additional 35 damage for each 100 HP ragdoll loses.



public class Torrent : Ability {

	int damageBonus = 35;

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{	
		
		if(attacker.maxHealth < 250)
		{
			int defenseStatus = defender.defense;
			defender.defense = 0;

				if(attacker.origHealth - attacker.maxHealth > 0)
				{
					int netHeallth = attacker.origHealth - attacker.maxHealth;
					float bonusDamage = damageBonus*(Mathf.Floor(netHeallth/100));

					int attackStatus = attacker.attack;
					attacker.attack += (int)bonusDamage;
					GameManager.Instance.Attack(attacker, defender);				
					attacker.attack = attackStatus;
				} 
				else 
				{
					GameManager.Instance.Attack(attacker, defender);
				}
			
			defender.defense = defenseStatus;
		} 
		else //if attacker.maxHealth >=250
		{

			if(attacker.origHealth - attacker.maxHealth > 0)
			{
					int netHeallth = attacker.origHealth - attacker.maxHealth;
					float bonusDamage = damageBonus*(Mathf.Floor(netHeallth/100));

					int attackStatus = attacker.attack;
					attacker.attack += (int)bonusDamage;
					GameManager.Instance.Attack(attacker, defender);				
					attacker.attack = attackStatus;
			} 
			else 
			{
				GameManager.Instance.Attack(attacker, defender);
			}
		}		
		
		base.UseAbility(attacker);		

	}

}
