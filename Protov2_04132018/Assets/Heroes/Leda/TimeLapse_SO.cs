using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName = "Skill/TimeLapse")]
public class TimeLapse_SO : Skill {

	public float[] values;

	public override void Use(GameObject source)
	{
		source.AddComponent<TimeLapse_Behaviour>().New(cost, values);
	}
}
