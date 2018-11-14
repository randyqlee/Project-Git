using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName = "Skill/TripleHit")]
public class TripleHit : Skill {
	
	//hardcoding for now to TripleHitBehaviour
	public float attackCount;
	//public Buffs buffs;
	//public int duration;
	

	public override void Use() {}
	public override void Use(GameObject source)
	{
		source.AddComponent<TripleHitBehaviour>().New(cost, new float[]{attackCount});
		
	}

	public override void Use(GameObject source, GameObject target) {

	}







}
