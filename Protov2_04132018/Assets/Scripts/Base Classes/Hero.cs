using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hero : MonoBehaviour {

	public BasicInformation objectInfo;
	public Attributes attributes;
	public HeroClass heroClass;
	public Rarity rarity;
	public List<Skill> skills;
	public List<Ability> abilities;

	public Attributes currAttribs;
	public GameObject tagHero;


	public bool hasTaunt = false;
	public bool hasCritical = false;
	public bool hasHealing = false;

	public bool isImmuneToNegEffects = false;
	public bool isImmuneToPosEffects = false;
	public bool isInvincible = false;
	public bool isImmortal = false;
	public bool hasCounterAttack = false;
	public bool hasProtectSoul = false;
	public bool hasReflect = false;
	public bool hasCriticalResist = false;
	public bool hasBrand = false;
	public bool isUnrecoverable = false;
	public bool hasOblivion = false;
	public bool hasGlancing = false;
	public bool isFreeze = false;
	public bool isStun = false;
	public bool isSleep = false;
	public bool hasSilence = false;

	public bool isActive = false;

	public bool isDead = false;

	public bool isTargeted = false;

	EventManager e;

	void Awake ()
	{
		//currAttribs.accuracy = attributes.accuracy;
		currAttribs.baseArmor = attributes.baseArmor;
		currAttribs.baseDamage = attributes.baseDamage;
		currAttribs.baseHP = attributes.baseHP;
		currAttribs.baseResistance = attributes.baseResistance;
		currAttribs.baseSpeed = attributes.baseSpeed;



		
		if (heroClass == HeroClass.TANK)
			hasTaunt = true;
		else if (heroClass == HeroClass.ATTACK)	
			{
				hasCritical = true;
				currAttribs.criticalRate = 20f;
			}
		//else if (heroClass == HeroClass.SUPPORT)
		//	hasHealing = true;	


		
		e = GetComponent<EventManager>();

	}

	void Start ()
	{

			if (tagHero != null)
			{




			}

	}


	public void ModifyArmor(float value)
	{
		currAttribs.baseArmor += value;
		e.modArmor();

	}

	public void ModifyDamage(float value)
	{
		currAttribs.baseDamage += value;
		e.modDamage();

	}

	public void ModifyAcurracy(float value)
	{
		currAttribs.accuracy += value;
		e.modAccuracy();

	}

	public void ModifyHP(float value)
	{
		currAttribs.baseHP += value;
		e.modHP();

	}

	public void ModifyResistance(float value)
	{
		currAttribs.baseResistance += value;
		e.modResistance();

	}

	public void ModifySpeed(float value)
	{
		currAttribs.baseSpeed += value;
		e.modSpeed();

	}

	public void ModifyCR(float value)
	{
		currAttribs.criticalRate += value;
		e.modCR();

	}

	public void SetActive ()
	{
		isActive = true;
		e.resetATB();
		Debug.Log (gameObject + " Is Active");
	}

	public void SetInactive ()
	{
		isActive = false;
		Debug.Log (gameObject + " Is Inactive");
	}

	public bool IsActive
	{
		get
		{
			return isActive;
		}
		set
		{	//if (value == true)
			//e.resetATB();
			
			isActive = value;
			//Debug.Log (gameObject + " IsActive: " + isActive);
							
		}
	}



}
