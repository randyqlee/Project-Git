using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class  Judge : Ability {

	float chanceReduction = 5;
	HeroManager hero;
	BuffPanel buffPanel;
	
	public override void UseAbilityPassive(){		

		Sprite permanentImmunity = Resources.Load<Sprite>("BuffIcons/PermanentImmunity");
		Sprite unluckyAkroma = Resources.Load<Sprite>("BuffIcons/UnluckyAkroma");
		
		
		hero = GetComponentInParent<HeroManager>();
		buffPanel = hero.gameObject.GetComponentInChildren<BuffPanel>();
		//apply effect
		hero.hasPermanentImmunity = true;
		//add icon
		buffPanel.AddIcon("PermanentImmunity", permanentImmunity, 0);

		List<HeroManager> enemies =  GameManager.Instance.EnemyHeroList(hero);
		foreach (HeroManager enemy in enemies){
			enemy.chance -= chanceReduction;

			//AddIcon
			BuffPanel buffPanelEnemy = enemy.gameObject.GetComponentInChildren<BuffPanel>();
			buffPanelEnemy.AddIcon("UnluckyAkroma", unluckyAkroma, 0);

		}		
		base.UseAbilityPassive();

	}//UsePassive

	public override void DisableAbilityPassive(){

		buffPanel = hero.gameObject.GetComponentInChildren<BuffPanel>();
		hero = GetComponentInParent<HeroManager>();
		//apply effect
		
		List<HeroManager> enemies =  GameManager.Instance.EnemyHeroList(hero);

		foreach (HeroManager enemy in enemies){		
		enemy.chance += chanceReduction;

		//RemoveIcon
		BuffPanel buffPanelEnemy = enemy.gameObject.GetComponentInChildren<BuffPanel>();
		buffPanelEnemy.RemoveIcon("UnluckyAkroma");	
		}

		buffPanel.RemoveIcon("PermanentImmunity");

		base.DisableAbilityPassive();

	}//Disable

	

}
