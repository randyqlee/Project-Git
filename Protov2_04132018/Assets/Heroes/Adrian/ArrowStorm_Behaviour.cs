using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowStorm_Behaviour : SkillComponent {

	public int attackCount = 4;
	public float multiplier = 2f;
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
			gameObject.GetComponentInChildren<SelectArrow>().HideArrow();
		
			Debug.Log ("ArrowStorm");
			GameObject gameManager = GameObject.Find("Game Manager");
			List<GameObject> team1 = new List<GameObject>(gameManager.GetComponent<GameManager>().team1);
			List<GameObject> team2 = new List<GameObject>(gameManager.GetComponent<GameManager>().team2);
			if (tag == "Team1")
			{
				StartCoroutine (RandomAttacks (team2));
			}
			if (tag == "Team2")
			{
				StartCoroutine (RandomAttacks (team1));
			}
			gameManager.GetComponent<GlobalATB>().tempTimer = 0;
			gameObject.GetComponentInChildren<SkillPanel>().HidePanel();


			ResetSkill();
			e.popupMsg ("Arrow Storm");
		}
		else e.popupMsg ("Skill not allowed");
	}

	IEnumerator RandomAttacks (List<GameObject> team)
	{		

		for (int i=0; i<attackCount; i++)
		{
			List<GameObject> activeHeroes = new List<GameObject>();
			foreach (GameObject go in team)
			{
				if (!go.GetComponent<Hero>().isDead)
					activeHeroes.Add(go);
			}
			if (activeHeroes.Count > 0)
			{
					int j = Random.Range(0,activeHeroes.Count);
					singleAttack (gameObject, activeHeroes[j]);
					yield return new WaitForSeconds (0.5f);
			}
		}

	}


	void singleAttack (GameObject source, GameObject target)
	{
		source.GetComponent<DamageSystem>().BasicAttack(source, target, multiplier, addDamage);

		if (source.GetComponent<ProbabilitySystem>().IsTrue (source, target, debuffChance))
		{
			Debug.Log ("Debuff");

			target.GetComponent<BuffSystem>().AddBuffComponent<ContinuousDamage> (debuffTurn,gameObject);
		}
	}
}
