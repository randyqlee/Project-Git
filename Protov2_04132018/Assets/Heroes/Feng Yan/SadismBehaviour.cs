using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadismBehaviour : SkillComponent {

	public float atkBarIncrease = 30;
	Hero hero;
	// Use this for initialization
	void Awake()
	{
		e = GetComponent<EventManager>();
		e.e_TakeDamage += IncreaseATB;
		e.e_ModArmor += ModifyAttack;
	}

//should delay this timing, such that Hero and BasicAttack components already loaded.
	void Start () {
		hero = gameObject.GetComponent<Hero>();
		
		ModifyAttack();
		
	}

	public void ModifyAttack()
	{
		
		float addDamage;
		addDamage = (0.5f * hero.currAttribs.baseArmor);
		hero.ModifyDamage(addDamage);

		Debug.Log ("ModifyAttack Damage: " + hero.currAttribs.baseDamage);

	}

	public void IncreaseATB (GameObject source, GameObject target)
	{
		Debug.Log ("Increase ATB");
		GetComponent<ATBTimer>().turn += atkBarIncrease/100;
	}


	public override void UseSkill(GameObject source, GameObject target) {}

	public override void UseSkill ()
	{
		Debug.Log ("Sadism");




	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
