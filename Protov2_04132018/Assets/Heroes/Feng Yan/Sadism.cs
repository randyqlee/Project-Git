using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName = "Skill/Sadism")]
public class Sadism : Skill {

	
    public override void Use(GameObject target, GameObject source) {}
	public override void Use(GameObject source) {

		source.AddComponent<SadismBehaviour>();
	}
	public override void Use() {}
}
