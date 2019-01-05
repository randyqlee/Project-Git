using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrostRush : Ability {
	
	int targetCount = 2;

	public override void UseAbility(HeroManager attacker, HeroManager defender)
	{
		
		bool criticalStatus = attacker.hasCritical;
		attacker.hasCritical = true;
		GameManager.Instance.AttackAll(attacker, defender);
		attacker.hasCritical = criticalStatus;

		//if an enemy dies, take an extraTurn
		CheckDeadEnemy(attacker, defender);
		
		base.UseAbilityRandom(attacker, defender, targetCount);
		//base.UseAbility();		
	}

	void CheckDeadEnemy(HeroManager attacker, HeroManager defender){

		//Get enemy list
		List<HeroManager> enemies = GameManager.Instance.EnemyHeroList(attacker);
		//Get allies list
		List<HeroManager> allies = GameManager.Instance.AllyHeroList(attacker);
		
		//Check for dead enemies
		GameManager.Instance.CheckHealth();		
		
		foreach(HeroManager enemy in enemies){

			//store SetActive status of the enemy
			bool temp = enemy.gameObject.activeInHierarchy;

			//temporarily set SetActive to true
			enemy.gameObject.SetActive(true);

			//if Enemy is Dead, take an extra turn
			if(enemy.isDead)
			{
				
			//use static nature of GameManager to store the list in the variable there, as opposed to passing arguements
			GameManager.Instance.extraTurnHeroes = allies;
		    //Select which ally shall have an extra turn
			foreach(HeroManager ally in GameManager.Instance.extraTurnHeroes){
				if(ally == attacker){
					ally.hasExtraTurn = true;
				}//if
			}//foreach

			GameManager.Instance.ExtraTurn();
				
			}
			//restore enemySetActive to original
			enemy.gameObject.SetActive(temp); 
		}

	}

	



}
