using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyResistance : AttribModifier {


	//initial addcomponent
	void Start ()
	{
		GetComponent<Hero>().currAttribs.baseResistance += value;
		Index();
	}

	//may be needed for enable/disable
	void OnEnable ()
	{
		GetComponent<Hero>().currAttribs.baseResistance += value;
	}

	void OnDisable ()
	{
		GetComponent<Hero>().currAttribs.baseResistance -= value;
	}

	public ModifyResistance New (float value)
	{
		this.value = value;
		return this;
	}

	public int Index ()
	{
		List<ModifyResistance> components = new List<ModifyResistance> ( gameObject.GetComponents<ModifyResistance>());
		this.index = components.Count;
		return this.index;
	}

}
