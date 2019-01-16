using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//"Human Transformation: Transform into human form and Amelia gains an extra turn.
//Passive effect:  At the start of your turn gain Increase Defense.

public class HumanTransformation : Ability {

HeroManager hero, oldHero, newHero, heroPrefab;
int[] oldHeroBuffDurations, oldHeroDebuffDurations;
Buff[] oldHeroBuffs, newHeroBuffs;
Debuff[] oldHeroDebuffs, newHeroDebuffs;

HeroAsset heroAsset;

	public override void UseAbility(HeroManager attacker, HeroManager defender){

		//Switch Forms - CreateHero
		AmeliaHuman(attacker);
		
		List<HeroManager> Allies = GameManager.Instance.AllyHeroList(attacker);
		foreach(HeroManager ally in Allies)
		{
			if(ally == attacker || ally.name == "AmeliaHuman"){
				ally.hasExtraTurn = true;
			}			
		}

		GameManager.Instance.ExtraTurn(attacker);
		base.UseAbility(attacker, defender);
	}

	public override void UseAbilityActive(){
		
		GameManager.Instance.e_PlayerMainPhase += IncreaseDefense;
		//Debug.Log("Subscribed");
	}

	public override void DisableAbilityActive(){

		GameManager.Instance.e_PlayerMainPhase -= IncreaseDefense;
		

	}

	// void OnEnable(){

	// 	GameManager.Instance.e_PlayerMainPhase += IncreaseDefense;
	// 	//Debug.Log("Subscribed");
	// }

	

	public void IncreaseDefense(){

		
		hero = this.GetComponentInParent<HeroManager>();
		//Debug.Log("hero");

		if(hero.GetComponentInParent<Player>().isActive){
			GameManager.Instance.AddBuff("IncreaseDefense", 1, hero, hero);
		}

	}//Increase Defense

	

	void AmeliaHuman(HeroManager oldHero){

		//Unsubscribe passive ability
		GameManager.Instance.e_PlayerMainPhase -= IncreaseDefense;
		//DisableAbilityActive();			

		//Create New Hero

		//Get AmeliaHiman
		heroAsset = Resources.Load<HeroAsset>("SO Assets/Hero/Final/AmeliaHuman");
		heroPrefab = Resources.Load<HeroManager>("Prefabs/Hero");

		//Init Heroes Routine		
		Transform heroLocation = oldHero.gameObject.transform;
		
		newHero =  Instantiate(heroPrefab, heroLocation.position, heroLocation.rotation, transform);
		HeroManager heroManager = newHero.GetComponent<HeroManager>();

		heroManager.heroName = heroAsset.heroName;
		heroManager.image = heroAsset.image;
		heroManager.maxHealth = heroAsset.maxHealth;
		heroManager.attack = heroAsset.attack;
		heroManager.defense = heroAsset.defense;
		heroManager.chance = heroAsset.chance;
		heroManager.rarity = heroAsset.rarity;

		heroManager.player = this.GetComponentInParent<Player>();
		heroManager.tag = this.GetComponentInParent<Player>().tag;

		newHero.GetComponentInChildren<Image>().sprite = heroAsset.image;
		newHero.name = heroManager.heroName;
		newHero.GetComponentInChildren<OverheadText>().FloatingText(newHero.name.ToString());

		for (int j = 0; j < heroAsset.abilityAsset2.Count; j++)
		{
			string spellScriptName = heroAsset.abilityAsset2[j].abilityEffect;
			if (spellScriptName != null) 
			{
				heroManager.abilityAssets.Add(heroAsset.abilityAsset2[j]);
			}					
		}	

				heroManager.origHealth = heroManager.maxHealth;
				heroManager.origAttack = heroManager.attack;
				heroManager.origDefense = heroManager.defense;
				heroManager.origChance = heroManager.chance;
				heroManager.origShield = heroManager.shield;
	//END OF INITHEROES Routine

			//Init Hero UI
				heroManager.transform.Find("HeroUI").gameObject.transform.Find("Health").gameObject.SetActive(true);
				heroManager.transform.Find("HeroUI").gameObject.transform.Find("Attack").gameObject.SetActive(true);
				heroManager.transform.Find("HeroUI").gameObject.transform.Find("Defense").gameObject.SetActive(true);
				heroManager.UpdateUI();
				
			//CreateHero Panel
				heroManager.CreateHeroPanel();

			//Set Glow
			var image = heroManager.glow.GetComponent<Image>().color;
			image.a = 1f;
			heroManager.glow.GetComponent<Image>().color = image;


		//Trigger Automatic Effects or Passive/Active effects
		newHero.gameObject.transform.Find("HeroPanel(Clone)").gameObject.SetActive(true);
								
				Ability[] abilities = newHero.GetComponentsInChildren<Ability>();
				foreach(Ability ability in abilities){					

					ability.remainingCooldown = ability.abilityCooldown;					
					//ability.remainingCooldown = 0;

					ability.GetComponentInChildren<Text>().text = ability.remainingCooldown.ToString();

					if(ability.skillType == Type.Passive){
						ability.UseAbilityPassive();
					}

					//For active skills with Passive
					if(ability.skillType == Type.Active){
						ability.UseAbilityActive();
					}					
				}

		newHero.gameObject.transform.Find("HeroPanel(Clone)").gameObject.SetActive(false);	

		//Fix UI
		newHero.gameObject.transform.SetParent(oldHero.GetComponentInParent<Player>().gameObject.transform);
		newHero.gameObject.transform.localScale = oldHero.gameObject.transform.localScale;		
		newHero.gameObject.transform.Find("HeroPanel(Clone)").transform.localScale = oldHero.gameObject.transform.Find("HeroPanel(Clone)").transform.localScale;
		newHero.gameObject.transform.Find("HeroPanel(Clone)").transform.position = oldHero.gameObject.transform.Find("HeroPanel(Clone)").transform.position;

		//need to revisit if there will be HP effects 
		newHero.maxHealth = oldHero.maxHealth;				
		newHero.UpdateUI();
		
		//Transfer All existing buffs and debuffs
		Buff[] buffs = oldHero.GetComponents<Buff>();
		foreach(Buff buff in buffs){
			GameManager.Instance.AddBuff(buff.buff.buff.ToString(), buff.duration, oldHero, newHero);			
		}

		Debuff[] debuffs = oldHero.GetComponents<Debuff>();
		foreach(Debuff debuff in debuffs){
			GameManager.Instance.AddDebuff(debuff.debuff.debuff.ToString(), debuff.duration, oldHero, newHero);			
		}			
				
		Destroy(oldHero.gameObject.transform.Find("HeroPanel(Clone)").gameObject);
		Destroy(oldHero.gameObject);
		

		//oldHero.enabled = false;
	}//Create Hero
}
