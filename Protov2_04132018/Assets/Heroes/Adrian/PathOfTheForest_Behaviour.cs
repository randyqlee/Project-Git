using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathOfTheForest_Behaviour : SkillComponent {

	public List<GameObject> targets;
	public List<GameObject> team1;
	public List<GameObject> team2;
	public float multiplier = 1f;
	public int attackCount = 1;
	public int turn = 3;

	public float addDamage = 0;

	// Use this for initialization
	void Start () {
		this.skillType = SkillType.LEADER;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void UseSkill()
	{

		if (IsSkillAllowed() && isActive)
		{
			Debug.Log ("Path of the Forest");


			GameObject gameManager = GameObject.Find("Game Manager");
			targets = new List<GameObject>(gameManager.GetComponent<GameManager>().all);

			foreach (GameObject target in targets)
			{
				if ( target.tag == tag)
				{
					target.GetComponent<BuffSystem>().AddBuffComponent<IncreaseAttack>(turn,gameObject);

					target.GetComponent<BuffSystem>().AddBuffComponent<IncreaseAtkSpeed>(turn,gameObject);

					target.GetComponent<BuffSystem>().AddBuffComponent<IncreaseCritical>(turn,gameObject);
				}

			}


			ShowTargets();
			gameObject.GetComponentInChildren<SelectArrow>().HideArrow();

			StartCoroutine(WaitForMouseButtonDown());
			ResetSkill();
			e.popupMsg ("Path of the Forest");
		}
		else e.popupMsg ("Skill not allowed");	
	}

	public override void UseSkill (GameObject source, GameObject target)
	{
				Debug.Log (gameObject + " is using skill: " + this);
				StartCoroutine (DelayedAttacks (source, target));
	}

	IEnumerator DelayedAttacks(GameObject source, GameObject target)
	{
		for (int i=0; i<attackCount; i++)
		{
			singleAttack (source, target);
			yield return new WaitForSeconds (0.5f);
		}
	}

	void singleAttack (GameObject source, GameObject target)
	{
		source.GetComponent<DamageSystem>().BasicAttack(source, target, multiplier, addDamage);

	}



}
