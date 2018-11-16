using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName = "Skill/Restoration")]
public class Restoration : Skill {

	public float healingPct = 15f;

	
    public override void Use(GameObject target, GameObject source) {}
	public override void Use(GameObject source)
	{
		source.AddComponent<RestorationBehaviour>().New(cost, new float[]{healingPct});
	
	}
	public override void Use() {}
}
