using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName = "Skill/PathOfTheForest")]
public class PathOfTheForest_SO : Skill {
	
	public float[] values;

	public override void Use(GameObject source)
	{
		source.AddComponent<PathOfTheForest_Behaviour>().New(cost, values);
	}
}