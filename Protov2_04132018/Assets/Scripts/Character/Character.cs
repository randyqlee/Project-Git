using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(SkillSystem))]
[RequireComponent(typeof(BuffSystem))]
[RequireComponent(typeof(EventManager))]
[RequireComponent(typeof(AbilitySystem))]
public class Character : MonoBehaviour {

	public Hero hero;
	public GameObject target;

	private HealthSystem healthSystem;
	private SkillSystem skillSystem;
	private BuffSystem buffSystem;
	private AbilitySystem abilitySystem;




	void Awake () {
		hero = Instantiate(hero);
		healthSystem = gameObject.GetComponent<HealthSystem>();
		skillSystem = gameObject.GetComponent<SkillSystem>();
		buffSystem = gameObject.GetComponent<BuffSystem>();
		abilitySystem = gameObject.GetComponent<AbilitySystem>();
		//AddRequiredComponents();
	}

	void Start () {
		ApplyLeaderSkills (gameObject);

	}

	void Update () {

		if (Input.GetKeyDown ("q"))
		{
//			if (target != null)
//			skillSystem.skills[1].Use(target,gameObject);

		}
		if (Input.GetKeyDown ("w"))
		{
			if (target != null)
			Attack (target);
			
		}
		if (Input.GetKeyDown ("e"))
		{
			hero.attributes.baseArmor -= 1;
			Debug.Log ("Armor: " + hero.attributes.baseArmor);
		
			
		}

		if (Input.GetKeyDown ("r"))
		{
//			skillSystem.skills[0].Use();

		}

		if (Input.GetKeyDown ("t"))
		{
		//	gameObject.AddComponent<IncreaseAttack>().New(50,2);

		}

		if (Input.GetKeyDown ("g"))
		{
		//	if (GetComponent<IncreaseAttack>() == null)
		//	gameObject.AddComponent<IncreaseAttack>().attackBonus = 100;
		//	else Debug.Log ("Component exists");
		}

		if (Input.GetKeyDown ("y"))
		{
			Destroy (GetComponent <IncreaseAttack>());

		}
		
	}

	private void AddRequiredComponents ()
	{
		healthSystem = gameObject.AddComponent<HealthSystem>();
		skillSystem = gameObject.AddComponent<SkillSystem>();
		buffSystem = gameObject.AddComponent<BuffSystem>();

	}

	void Attack (GameObject target)
	{
		target.GetComponent<HealthSystem>().TakeDamage(target,gameObject);
	}

	void ApplyLeaderSkills (GameObject source){

//		if (skillSystem.skills.Count != 0)
//		skillSystem.skills[0].Use(source);
	}


}