using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLapse_Behaviour : SkillComponent {

	public int attackCount = 2;
	public float recoverATBfactor = 0.15f;
	public float damageFactor = 0.1f;
	public int debuffTurn = 2;
	
	public float debuffChance = 1;
	public float addDamage;
	public float multiplier = 1;


	// Use this for initialization
	void Start () {
		
		this.skillType = SkillType.ACTIVE;
		
	}

	void RecoverATB()
	{

		gameObject.GetComponent<ATBTimer>().modifyATB(recoverATBfactor);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void UseSkill()
	{
		
		if (IsSkillAllowed() && isActive)
		{
			Debug.Log (this.name.ToString());

			ShowTargets();
			gameObject.GetComponentInChildren<SelectArrow>().HideArrow();

			StartCoroutine(WaitForMouseButtonDown());

		}

		else e.popupMsg ("Skill not allowed");
		
	}

	public override void UseSkill (GameObject source, GameObject target)
	{

				Debug.Log (gameObject + " is using skill: " + this);
				e.popupMsg ("TimeLapse");
				StartCoroutine (DelayedAttacks (source, target));
	}

	IEnumerator DelayedAttacks(GameObject source, GameObject target)
	{
		for (int i=0; i<attackCount; i++)
		{
			singleAttack (source, target);
			yield return new WaitForSeconds (0.5f);
		}
					ResetSkill();
			
	}

	void singleAttack (GameObject source, GameObject target)
	{
		e.e_CriticalHit += RecoverATB;

		addDamage = damageFactor * source.GetComponent<Hero>().currAttribs.baseSpeed;
		source.GetComponent<DamageSystem>().BasicAttack(source, target, multiplier, addDamage);



		if (source.GetComponent<ProbabilitySystem>().IsTrue (source, target, debuffChance))
		{
			Debug.Log ("Debuff");

			target.GetComponent<BuffSystem>().AddBuffComponent<Glancing> (debuffTurn,gameObject);

		}

		e.e_CriticalHit -= RecoverATB;
		


	}


}