using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName = "Skill/Adrian_LeaderSkill")]
public class Adrian_LeaderSkill : Skill {
	
	//hardcoding for now to TripleHitBehaviour
	public float[] values;
	//public Buffs buffs;
	//public int duration;
	

	public override void Use() {}
	public override void Use(GameObject source)
	{
		source.AddComponent<Adrian_LeaderSkill_Behaviour>().New(cost, values);
	}

	public override void Use(GameObject source, GameObject target) {

	}
}
