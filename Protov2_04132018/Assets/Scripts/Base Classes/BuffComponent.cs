using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffComponent : MonoBehaviour {

	public int duration;
	public GameObject source;
	public new string tag;

	public string buffName;
	public Sprite buffIcon;

	


	EventManager e;

	public BuffComponent New (int duration, GameObject source)
	{
		this.duration = duration;
		this.source = source;
		return this;

	}

	public void DecreaseDuration ()
	{
		this.duration--;
		if (this.duration == 0)
			Destroy(this);
	} 

	public void DecreaseStunDuration ()
	{
		this.duration--;
		if (this.duration == 0)
			Destroy(this);
	} 

	void Start()
	{
		if (e = GetComponent<EventManager>())
		{
			e.e_ResetATB += DecreaseDuration;
			e.e_ResetStun += DecreaseStunDuration;
			e.addBuff(this.buffName, this.buffIcon);
		}
	}

	void OnDisable ()
	{

	}

	protected virtual void OnDestroy ()
	{

		Debug.Log ("Removing Buff: " + this.ToString());
		//GetComponentInChildren<BuffPanel>().RemoveIcon(this.buffName);
		if (e = GetComponent<EventManager>())
		{
			e = GetComponent<EventManager>();
			e.removeBuff(this.buffName);
		}
	}

	
}
