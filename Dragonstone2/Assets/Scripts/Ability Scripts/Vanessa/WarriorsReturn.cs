using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class  WarriorsReturn : Ability {
	
	HeroManager hero;
	HeroAsset heroAsset;	
	
	public override void UseAbilityPassive(){	

		GameManager.Instance.e_PlayerStartPhase += WarriorsReturnAbility;
	}//UsePassive

	public override void DisableAbilityPassive(){	

		GameManager.Instance.e_PlayerStartPhase -= WarriorsReturnAbility;
	}//Disable

	public void WarriorsReturnAbility()
	{		
		hero = GetComponentInParent<HeroManager>();
		Player allyPlayer = hero.GetComponentInParent<Player>();
		int deadHeroesCount = allyPlayer.deadHeroes.Count;

		if(hero.GetComponentInParent<Player>().isActive)
		{
			if(remainingCooldown == 0)
			{
				canUseAbility = true;
				
				if(deadHeroesCount > 0)
				{
					
					GameObject revivedHero = allyPlayer.deadHeroes[Random.Range(0,deadHeroesCount)];
					allyPlayer.deadHeroes.Remove(revivedHero);
					HeroManager heroRevived = revivedHero.GetComponent<HeroManager>();

					GameManager.Instance.ReviveHero(heroRevived);
					heroRevived.maxHealth = 400;
	
					GameManager.Instance.AddBuff("IncreaseAttack", 2, hero, heroRevived);

					heroRevived.UpdateUI();					

					ResetCooldown();
					
				}				
			}//if cooldown = 0				
		}
	}	

	}//Warriors Return Ability
	


