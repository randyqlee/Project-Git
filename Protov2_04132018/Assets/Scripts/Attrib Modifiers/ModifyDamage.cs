using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyDamage : AttribModifier {

	//initial addcomponent
	void Start ()
	{
		GetComponent<Hero>().currAttribs.baseDamage += value;
		Index(); // set an ID for every AddComponent call for this script
	}

	//may be needed for enable/disable
	void OnEnable ()
	{
		GetComponent<Hero>().currAttribs.baseDamage += value;
	}

	void OnDisable ()
	{
		GetComponent<Hero>().currAttribs.baseDamage -= value;
	}

	public ModifyDamage New (float value)
	{
		this.value = value;
		return this;
	}

	public int Index ()
	{
		List<ModifyDamage> components = new List<ModifyDamage> ( gameObject.GetComponents<ModifyDamage>());
		this.index = components.Count;
		return this.index;
	}


}
