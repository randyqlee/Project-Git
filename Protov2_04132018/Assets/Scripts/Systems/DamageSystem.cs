using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour {

	EventManager e;

	void Awake()
	{
		e = GetComponent<EventManager>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Damage ComputeDamage (GameObject source, GameObject target, Damage damage)
	{
		float grossDamage = 0;
		float finalDamage = 0;
		switch (damage.type)
		{
			case DamageType.PHYSICAL:
			{
				grossDamage = ComputeAtkFlatPhysical(source) * (1 + ComputeAtkPctPhysical(source)/100) * ClassTypeBonus(source,target);
				//finalDamage = ComputeCritical (source, target, grossDamage);
				finalDamage = grossDamage;
			}
			break;

			case DamageType.MAGIC:
			{

			}
			break;
		}
		return new Damage(finalDamage, damage.type, damage.isCritical);
		
	}

	float ComputeAtkFlatPhysical (GameObject source)
	{
		float atkFlat = 0;
		atkFlat = source.GetComponent<Hero>().currAttribs.baseDamage;
		return atkFlat;
	}

	float ComputeAtkPctPhysical (GameObject source)
	{
	
		float atkPct = 0;

	/*	
		IncreaseAttack[] incAtk;
		incAtk = source.GetComponents<IncreaseAttack>();
		if (incAtk != null)
			foreach (IncreaseAttack increaseAttack in incAtk)
			atkPct += increaseAttack.value;

		DecreaseAttack[] decAtk;
		decAtk = source.GetComponents<DecreaseAttack>();
		if (decAtk != null)
			foreach (DecreaseAttack decreaseAttack in decAtk)
			atkPct += decreaseAttack.value;	
	*/

		return atkPct;	
	}


	float ClassTypeBonus (GameObject source, GameObject target)
	{
		float classTypeBonus = 0;
		

		switch (source.GetComponent<Hero>().heroClass)
		{
			case HeroClass.TANK:
			{
				switch (target.GetComponent<Hero>().heroClass)
				{
					case HeroClass.TANK:
					{
						classTypeBonus = 1.0f;
					}
					break;
					case HeroClass.ATTACK:
					{
						classTypeBonus = 1.2f;
					}
					break;
					case HeroClass.SUPPORT:
					{
						classTypeBonus = 0.8f;
					}
					break;										
				}

			}
			break;

			case HeroClass.ATTACK:
			{
				switch (target.GetComponent<Hero>().heroClass)
				{
					case HeroClass.TANK:
					{
						classTypeBonus = 0.8f;
					}
					break;
					case HeroClass.ATTACK:
					{
						classTypeBonus = 1.0f;
					}
					break;
					case HeroClass.SUPPORT:
					{
						classTypeBonus = 1.2f;
					}
					break;
				}												

			}
			break;

			case HeroClass.SUPPORT:
			{
				switch (target.GetComponent<Hero>().heroClass)
				{
					case HeroClass.TANK:
					{
						classTypeBonus = 1.2f;
					}
					break;
					case HeroClass.ATTACK:
					{
						classTypeBonus = 0.8f;
					}
					break;
					case HeroClass.SUPPORT:
					{
						classTypeBonus = 1.0f;
					}
					break;
				}												

			}
			break;

		}
		return classTypeBonus;

	}

	float ComputeCritical (GameObject source, GameObject target, float grossDamage)
	{

		float finalDamage = 0;
		float sourceCritical = source.GetComponent<Hero>().currAttribs.criticalRate;
		
		if (!target.GetComponent<Hero>().hasCriticalResist)
		{
			if ((1-Random.value) < sourceCritical/100)
				finalDamage = grossDamage * 2f;
			else
				finalDamage = grossDamage;
		}
		else
		{
			if ((1-Random.value) < 50/100)
			{
				if ((1-Random.value) < sourceCritical/100)
				finalDamage = grossDamage * 2f;
				else
					finalDamage = grossDamage;
			}
			else
				finalDamage = grossDamage;
		}





		return finalDamage;



	}

	public void BasicAttack (GameObject source, GameObject target, float multiplier, float addDamage)
	{
		float damageValue = (multiplier * source.GetComponent<Hero>().currAttribs.baseDamage) + addDamage;

		//check if attacking hero has critical

		if(source.GetComponent<Hero>().hasCritical)

			if (source.GetComponent<ProbabilitySystem>().IsTrue (source, target, source.GetComponent<Hero>().currAttribs.criticalRate))
			{
				//if critical chance is true, double damageValue
				damageValue = 2 * damageValue;

				e.criticalHit();
			}

		

		if (source.GetComponent<Hero>().hasGlancing)
		{
			if (source.GetComponent<ProbabilitySystem>().IsTrue (source, target, 50f))
			{
				damageValue -= 0.3f*damageValue;
			}
		}

		else if (ClassTypeBonus (source, target) == 0.8f)
		{
			if (source.GetComponent<ProbabilitySystem>().IsTrue (source, target, 50f))
			{
				damageValue -= 0.5f*damageValue;
				e.popupMsg("Superiority");
				
			}
		}


		//if original target has Defend buff, change the target to the ally source of Defend
		if (target.GetComponent<Defend>())
			target = target.GetComponent<Defend>().source;

		//if target has Reflect, split damage

		if (target.GetComponent<Hero>().hasReflect)
			{				
				source.GetComponent<HealthSystem>().TakeDamage (new Damage(0.75f*damageValue,DamageType.PHYSICAL,false) , target, source);
				damageValue -= 0.75f*damageValue;
			}			

		Damage damage = source.GetComponent<DamageSystem>().ComputeDamage(source, target, new Damage(damageValue, DamageType.PHYSICAL, false));
		Debug.Log ("Attacking with damage: " + damage.value + " ,target is " + target);
		//pass value of Damage to target's Healthsystem
		target.GetComponent<HealthSystem>().TakeDamage(damage, source, target);
	}

}
