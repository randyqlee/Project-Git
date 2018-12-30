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
	}

	void CheckDeadEnemy(HeroManager attacker, HeroManager defender){

		//Get enemy list
		List<HeroManager> enemies = GameManager.Instance.EnemyHeroList(attacker);
		
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
				GameManager.Instance.ExtraTurn();
				Debug.Log("Frost Rush EXTRA TURN");
				//reduce cooldown of ally heroes
			}
			//restore enemySetActive to original
			enemy.gameObject.SetActive(temp); 
		}

	}

	



}
