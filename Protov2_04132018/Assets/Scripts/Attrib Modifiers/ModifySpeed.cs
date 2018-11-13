using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifySpeed : AttribModifier {


	//initial addcomponent
	void Start ()
	{
		GetComponent<Hero>().currAttribs.baseSpeed += value;
		Index(); // set an ID for every AddComponent call for this script
	}

	//may be needed for enable/disable
	void OnEnable ()
	{
		GetComponent<Hero>().currAttribs.baseSpeed += value;
	}

	void OnDisable ()
	{
		GetComponent<Hero>().currAttribs.baseSpeed -= value;
	}

	public ModifySpeed New (float value)
	{
		this.value = value;
		return this;
	}

	public int Index ()
	{
		List<ModifySpeed> components = new List<ModifySpeed> ( gameObject.GetComponents<ModifySpeed>());
		this.index = components.Count;
		return this.index;
	}


}
