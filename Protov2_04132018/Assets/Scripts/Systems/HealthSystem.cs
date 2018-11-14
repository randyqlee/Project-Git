using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour {

	public float currHealth;
	public Hero hero;
	EventManager e;

	[SerializeField] Image healthBar;


	void Start () {
		currHealth = GetComponent<Hero>().attributes.baseHP;
		hero = GetComponent<Hero>();
		e= GetComponent<EventManager>();
		
		
	}

	public void TakeDamage (float damage)
	{
		//insert code to compute final damage and trigger other abilities when damage is taken

		//boost damage by 25pct if hero has Brand
		if (hero.hasBrand)
			damage = damage * 1.25f;

		int damageInt = Mathf.RoundToInt(damage);

		Debug.Log (gameObject + " is Taking Damage: " + damageInt);





		if (!hero.isInvincible)
		{	
			if (damageInt < currHealth)
			{
				currHealth -= damageInt;
			}
			else
			{
				if (hero.isImmortal)
				{
					currHealth = 1;
					Debug.Log ("Hero is Immortal");
				}
				else
				{
					currHealth = 0;
					
					//revive hero with protectsould
					if (hero.hasProtectSoul)
					{
						float hpFactor = 1f;
						float atb = 0f;
						Revive (hpFactor,atb);
					}

					else
					{
						Die();
					}
				}
			}

			if(hero.isSleep)
			{
				hero.isSleep = false;
			}

			e.popupMsg("-" + damageInt.ToString());

		}
		else
		{
			Debug.Log ("Hero is Invincible");
			e.popupMsg("-0");
		}

		UpdateCurrAttribBaseHP();
		UpdateHealthBar ();


		
		
			
		
		//else if ()
	}

	public void UpdateCurrAttribBaseHP()
	{

		hero.currAttribs.baseHP = currHealth;
	}

	public void UpdateHealthBar ()
	{
		healthBar.fillAmount = hero.currAttribs.baseHP/hero.attributes.baseHP;

	}

	public void Die ()
	{
		Debug.Log ("Hero is Dead");
	

		//deactivate Hero
		gameObject.GetComponent<Hero>().isDead = true;

		//remove buffs
		gameObject.GetComponent<BuffSystem>().RemoveAll();

		//reset skills
		gameObject.GetComponent<SkillSystem>().ResetSkills();

		//pause ATBTimer
		gameObject.GetComponent<ATBTimer>().isPaused = true;

		//hide sprite
		gameObject.GetComponent<SpriteRenderer>().enabled = false;

		//hide Hero UI
		gameObject.transform.Find("UI").gameObject.SetActive(false);

			e.heroDie();

			

	}

	public void Revive (float hpFactor, float atb)
	{
		//remove buffs
		gameObject.GetComponent<BuffSystem>().RemoveAll();

		//reset skills
		gameObject.GetComponent<SkillSystem>().ResetSkills();

		//reset values
		hero.currAttribs.baseHP = hero.attributes.baseHP * hpFactor;
		gameObject.GetComponent<ATBTimer>().turn = atb;

	}






	public void TakeDamage (Damage damage, GameObject source, GameObject target)
	{

		e.takeDamage(target, source);

		float netArmor = 0;
		switch (damage.type)
		{
			case DamageType.PHYSICAL:
			{
				netArmor = ComputeDefFlatPhysical() * (1 + ComputeDefPctPhysical()/100);

				float netDamage = damage.value * (1 - (netArmor / (netArmor + 100)));


				TakeDamage (netDamage);
				
			}
			break;

			case DamageType.MAGIC:
			{

			}
			break;
		}

	}

	float ComputeDefFlatPhysical ()
	{
		float defFlat = 0;
		defFlat = GetComponent<Hero>().currAttribs.baseArmor;
		Debug.Log ("Flat Armor: " + defFlat);
		return defFlat;
	}

	float ComputeDefPctPhysical ()
	{
		float defPct = 0;
	/*

		IncreaseDefense[] incDef;
		incDef = GetComponents<IncreaseDefense>();
		if (incDef != null)
			foreach (IncreaseDefense increaseDefense in incDef)
			defPct += increaseDefense.value;
		
		DecreaseDefense[] decDef;
		decDef = GetComponents<DecreaseDefense>();
		if (decDef != null)
			foreach (DecreaseDefense decreaseDefense in decDef)
			defPct += decreaseDefense.value;		
		Debug.Log ("Pct Armor: " + defPct);
	*/
		return defPct;
	}	

	


	public void TakeDamage (GameObject target, GameObject attacker)
	{
		//insert code to compute final damage and trigger other abilities when damage is taken
		TakeDamage(attacker.GetComponent<Character>().hero.attributes.baseDamage);

		//announce event to listeners
		EventManager e = GetComponent<EventManager>();
		e.takeDamage(target, attacker);
	}

	public void Heal (float points)
	{
		if (!hero.isImmuneToPosEffects || !hero.isUnrecoverable)
		{
			currHealth += points;
			e.popupMsg("Heal +" + points);
			UpdateCurrAttribBaseHP();
			UpdateHealthBar ();
		}
	}


	

}
