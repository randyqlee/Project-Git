using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ability : ScriptableObject {

	public GameObject parent;

	public BasicInformation objectInfo;

	public void setParent (GameObject parent)
	{
		this.parent = parent;
	}

}
