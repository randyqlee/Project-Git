using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName = "Skill/ArrowStorm")]
public class ArrowStorm_SO : Skill {
	
	public float[] values;

	public override void Use(GameObject source)
	{
		source.AddComponent<ArrowStorm_Behaviour>().New(cost, values);
	}
}
