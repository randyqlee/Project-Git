using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatesEnd_Behaviour : SkillComponent {

	// Use this for initialization
	void Start () {
		
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
				e.popupMsg ("Fate's end");
				target.GetComponent<BuffSystem>().AddBuffComponent<ContinuousDamage>(2, source);
				target.GetComponent<BuffSystem>().AddBuffComponent<Bomb>(2, source);
	}



}
