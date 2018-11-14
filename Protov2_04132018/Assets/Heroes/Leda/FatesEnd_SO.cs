using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName = "Skill/FatesEnd")]
public class FatesEnd_SO : Skill {

	public float[] values;

	public override void Use(GameObject source)
	{
		source.AddComponent<FatesEnd_Behaviour>().New(cost, values);
	}

}
