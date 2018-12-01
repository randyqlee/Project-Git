using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffAsset : ScriptableObject {

	[Header("Buff Name")]
	public DebuffList debuff;

	public Sprite icon;

	[TextArea(5,10)]
	public string description;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
