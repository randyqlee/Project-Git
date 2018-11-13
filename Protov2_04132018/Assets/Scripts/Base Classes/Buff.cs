using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff {

	public Buffs buff;
	public int duration;

	public Buff(Buffs buffs, int duration)
	{
		this.buff = buffs;
		this.duration = duration;
	}


}
