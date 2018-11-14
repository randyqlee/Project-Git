using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strafe_Behaviour : SkillComponent {

	public int attackCount = 3;
	public float multiplier = 1.75f;
	public float debuffChance = 75f;
	public int debuffTurn = 1;

	public float addDamage = 0;
	// Use this for initialization
	void Start () {
		this.skillType = SkillType.ACTIVE;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public override void UseSkill()
	{
		if (IsSkillAllowed() && isActive)
		{
			Debug.Log ("Strafe");

			ShowTargets();
			gameObject.GetComponentInChildren<SelectArrow>().HideArrow();

			StartCoroutine(WaitForMouseButtonDown());

		}

		else e.popupMsg ("Skill not allowed");
	}

	public override void UseSkill (GameObject source, GameObject target)
	{
				Debug.Log (gameObject + " is using skill: " + this);
				e.popupMsg ("Strafe");
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
		source.GetComponent<DamageSystem>().BasicAttack(source, target, multiplier, addDamage);



		if (source.GetComponent<ProbabilitySystem>().IsTrue (source, target, debuffChance))
		{
			Debug.Log ("Debuff");

			target.GetComponent<BuffSystem>().AddBuffComponent<Unrecoverable> (debuffTurn,gameObject);

		}


	}


}
