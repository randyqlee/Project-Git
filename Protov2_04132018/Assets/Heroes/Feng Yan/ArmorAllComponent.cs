using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorAllComponent : AttribModifier {

	void Start ()
	{
		GetComponent<Hero>().currAttribs.baseArmor += value;
		Index();
	}

	//may be needed for enable/disable
	void OnEnable ()
	{
		GetComponent<Hero>().currAttribs.baseArmor += value;
	}

	void OnDisable ()
	{
		GetComponent<Hero>().currAttribs.baseArmor -= value;
	}

	public ArmorAllComponent New (float value)
	{
		this.value = value;
		return this;
	}

	public int Index ()
	{
		List<ArmorAllComponent> components = new List<ArmorAllComponent> ( gameObject.GetComponents<ArmorAllComponent>());
		this.index = components.Count;
		return this.index;
	}
}
