using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Unicorn Transformation:  Transform into unicorn form and Amelia gains an extra turn. 
//Passive effect:  At the start of your turn, grant a random ally immunity for 1 turn.
public class UnicornTransformation : Ability {

HeroManager hero;
	public override void UseAbility(HeroManager attacker, HeroManager defender){

		//Switch Forms
		
		List<HeroManager> Allies = GameManager.Instance.AllyHeroList(attacker);
		foreach(HeroManager ally in Allies)
		{
			if(ally == attacker){
				ally.hasExtraTurn = true;
			}
		}

		GameManager.Instance.ExtraTurn(attacker);
		base.UseAbility(attacker);
	}

	public override void UseAbilityActive(){
		
		GameManager.Instance.e_PlayerStartPhase += RandomAllyImmunity;
		//Debug.Log("Subscribed");
	}

	public override void DisableAbilityActive(){

		GameManager.Instance.e_PlayerStartPhase -= RandomAllyImmunity;

	}

	public void RandomAllyImmunity(){

		hero = this.GetComponentInParent<HeroManager>();

		if(hero.GetComponentInParent<Player>().isActive){

			//Debug.Log("hero");

			List<HeroManager> allies = GameManager.Instance.AllyHeroList(hero);
			HeroManager randomAlly = allies[Random.Range(0,allies.Count)];

			GameManager.Instance.AddBuff("Immunity", 1, hero, randomAlly);

		}//if hero is Active		

	}//RandomAllyImmunity

}
