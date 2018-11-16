using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage {

	public float value;
	public DamageType type;
	public bool isCritical;

	public Damage (float value, DamageType type, bool isCritical)
	{
		this.value = value;
		this.type = type;
		this.isCritical = isCritical;
	}


}
