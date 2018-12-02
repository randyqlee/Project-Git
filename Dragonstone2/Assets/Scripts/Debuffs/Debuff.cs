﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Debuff : MonoBehaviour{

	public DebuffAsset debuff;
	public Sprite debuffIcon;
	public int duration;
	public GameObject source;



	//public Buff(BuffAsset buff, int duration)
	//{
	//	this.buff = buff;
	//	this.duration = duration;
	//}

	public Debuff New (int duration, GameObject source)
	{
		this.duration = duration;
		this.source = source;
		return this;

	}

	void Start ()
	{
		GameManager.Instance.e_NextTurn += DecreaseDuration;

	}

	public virtual void DecreaseDuration ()
	{
		if (GetComponentInParent<Player>().isActive)
		{
			this.duration--;
			if (this.duration == 0)
				Destroy (this);
		}

	}

	protected virtual void OnDestroy()
	{
		//remove from HeroManager
		//unsubscribe
	}




}