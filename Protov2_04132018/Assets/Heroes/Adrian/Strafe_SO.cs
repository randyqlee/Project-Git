using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName = "Skill/Strafe")]
public class Strafe_SO : Skill {
	
	public float[] values;

	public override void Use(GameObject source)
	{
		source.AddComponent<Strafe_Behaviour>().New(cost, values);
	}
}
