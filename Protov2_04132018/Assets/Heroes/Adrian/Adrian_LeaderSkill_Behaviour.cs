using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adrian_LeaderSkill_Behaviour : SkillComponent {

	public List<GameObject> targets;
	public List<GameObject> team1;
	public List<GameObject> team2;
	public float multiplier = 2f;

	public float addDamage = 0;

	// Use this for initialization
	void Start () {
		this.skillType = SkillType.LEADER;
		this.isActive = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void UseSkill()
	{
	}

	public override void UseSkill (GameObject source, GameObject target)
	{}

	public override void UseBattlecry()
	{
		Debug.Log("using Leader Skill!");
		e.popupMsg ("Path of the Forest");
	
		GameObject gameManager = GameObject.Find("Game Manager");
		targets = new List<GameObject>(gameManager.GetComponent<GameManager>().all);
		team1 = new List<GameObject>(gameManager.GetComponent<GameManager>().team1);
		team2 = new List<GameObject>(gameManager.GetComponent<GameManager>().team2);

		//Debug.Log (isActive.ToString());

		if (isActive)
		{
			//Debug.Log("using Leader Skill! is Active");

			foreach (GameObject target in targets)
			{
				if ( target.tag == tag)
				{
					target.GetComponent<BuffSystem>().AddBuffComponent<IncreaseAttack>(3,gameObject);

					target.GetComponent<BuffSystem>().AddBuffComponent<IncreaseAtkSpeed>(3,gameObject);

					target.GetComponent<BuffSystem>().AddBuffComponent<IncreaseCritical>(3,gameObject);
				}

			}

			if (tag == "Team1")
			{
				int i = Random.Range(0,team2.Count);
				gameObject.GetComponent<DamageSystem>().BasicAttack(gameObject, team2[i], multiplier, addDamage);
				//Debug.Log("using Leader Skill! tag = team1");

			}

			if (tag == "Team2")
			{
				int i = Random.Range(0,team1.Count);
				gameObject.GetComponent<DamageSystem>().BasicAttack(gameObject, team1[i], multiplier, addDamage);

				//Debug.Log("using Leader Skill! tag = team2");

			}


			ResetSkill();
		}

		else Debug.Log("using Leader Skill! is NOT Active");



	}


}
