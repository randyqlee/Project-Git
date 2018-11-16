using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Skill : ScriptableObject {

	public BasicInformation objectInfo;
	public int cost;
	public GameObject skillFX;
	public int currCost;

    public virtual void Use(GameObject source, GameObject target) {}
	public virtual void Use(GameObject source) {}
	public virtual void Use() {}

	void OnEnable()
	{

		currCost = cost;
	}


}
