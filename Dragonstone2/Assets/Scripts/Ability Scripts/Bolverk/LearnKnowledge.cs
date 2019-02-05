using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Learn Knowledge:  At the start of your turn, gain knowledge equivalent to all buffs in play.  
//If there are no buffs in play, gain 1 knowledge.  You can have up to 5 Knowledge.


public class LearnKnowledge : Ability {

public HeroManager hero;
//public Buff[] buffs;
public int knowledgePoints = 0;	

	public override void UseAbilityPassive(){
		
		GameManager.Instance.e_PlayerStartPhase += GainKnowledge;
		GetComponentInChildren<Text>().enabled = true;		
	}

	public override void DisableAbilityPassive(){

		GameManager.Instance.e_PlayerStartPhase -= GainKnowledge;
		GetComponentInChildren<Text>().enabled = false;
	}

	
	void OnDisable(){
		GameManager.Instance.e_PlayerStartPhase -= GainKnowledge;
		GetComponentInChildren<Text>().enabled = false;
	}	


	public void GainKnowledge(){		
		
		hero = this.GetComponentInParent<HeroManager>();
		
		List<HeroManager> allies = GameManager.Instance.AllyHeroList(hero);
		List<HeroManager> enemies = GameManager.Instance.EnemyHeroList(hero);

		List<HeroManager> heroes = new List<HeroManager>();
		heroes.AddRange(allies);
		heroes.AddRange(enemies);
		
		//Get all buffs from each Player
		if(hero.GetComponentInParent<Player>().isActive){							
			foreach(HeroManager hero1 in heroes){
				
				Buff[] buffs1 = hero1.GetComponents<Buff>();				
				foreach(Buff buff in buffs1){					
					
					knowledgePoints++;
					
				}//foreach Buff in buffs
			}//foreach Hero Manager

			if(knowledgePoints < 1){
				knowledgePoints = 1;
			} else if(knowledgePoints > 5){
				knowledgePoints = 5;
			}

			GetComponentInChildren<Text>().text = knowledgePoints.ToString();
			
			//Debug.Log("Knowledge Points Total: " +knowledgePoints);
			
			
			
		}//if hero is Active		
		
	}//Gain Knowledge
}//class
