using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//"Human Transformation: Transform into human form and Amelia gains an extra turn.
//Passive effect:  At the start of your turn gain Increase Defense.

public class HumanTransformation : Ability {

HeroManager hero, oldHero, newHero, heroPrefab;
int[] oldHeroBuffDurations, oldHeroDebuffDurations;
Buff[] oldHeroBuffs, newHeroBuffs;
Debuff[] oldHeroDebuffs, newHeroDebuffs;
	public override void UseAbility(HeroManager attacker, HeroManager defender){

		//Switch Forms - UnicornToHuman
		UnicornToHuman(attacker, attacker);
		
		List<HeroManager> Allies = GameManager.Instance.AllyHeroList(attacker);
		foreach(HeroManager ally in Allies)
		{
			if(ally == attacker || ally.name == "Bastet"){
				ally.hasExtraTurn = true;
			}

			
		}


		GameManager.Instance.ExtraTurn(attacker);
		base.UseAbility(attacker, defender);
	}

	public override void UseAbilityActive(){
		
		//GameManager.Instance.e_PlayerMainPhase += IncreaseDefense;
		//Debug.Log("Subscribed");
	}

	public override void DisableAbilityActive(){

		GameManager.Instance.e_PlayerMainPhase -= IncreaseDefense;

	}

	void OnEnable(){
		GameManager.Instance.e_PlayerMainPhase += IncreaseDefense;
		Debug.Log("Subscribed");
	}

	public void IncreaseDefense(){

		hero = this.GetComponentInParent<HeroManager>();
		//Debug.Log("hero");

		if(hero.GetComponentInParent<Player>().isActive){
			GameManager.Instance.AddBuff("IncreaseDefense", 1, hero, hero);
		}

	}//Increase Defense

	void UnicornToHuman(HeroManager oldHero, HeroManager newHero){

		//Unsubscribe passive ability
		GameManager.Instance.e_PlayerMainPhase -= IncreaseDefense;

		//Create New Hero

		//Init Heroes Routine
		Transform heroLocation = oldHero.gameObject.transform;

		//Transform spawnLocation = spawnLocations.GetComponent<SpawnLocations>().spawn[i].transform;
		newHero =  Instantiate(heroPrefab, heroLocation.position, heroLocation.rotation, transform);


		//Init Hero UI
		//CreateHero Panel

		//Get All existing buffs and debuffs

		//DestroyOldHero


	}//Unicorn to Human
}
