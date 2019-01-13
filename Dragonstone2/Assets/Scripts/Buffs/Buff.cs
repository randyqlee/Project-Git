using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Buff : MonoBehaviour{

	public BuffAsset buff;
	public Sprite buffIcon;
	public int duration;
	public GameObject source;

	public BuffPanel buffPanel;


	public Buff New (int duration, GameObject source)
	{
		this.duration = duration;
		this.source = source;
		return this;

	}

	public int Duration
	{
		get{ return this.duration;}
		set{ this.duration = value;}
	}


	void Start ()
	{
		GameManager.Instance.e_PlayerStartPhase += DecreaseDuration;
		
		buffPanel = gameObject.GetComponentInChildren<BuffPanel>();
		AddIcon();

		gameObject.GetComponent<HeroManager>().UpdateUI();

	}

	public void DecreaseDuration ()
	{
		if (GetComponentInParent<Player>().isActive)
		{
			this.duration--;

			if(this.duration<0)
			this.duration = 0;

			if (this.duration == 0)
				Destroy (this);
			else
				buffPanel.UpdateBuffIconCD(buff.buff.ToString(), this.duration);
		}

	}

	public virtual void OnDestroy()
	{
		gameObject.GetComponent<HeroManager>().UpdateUI();
		RemoveIcon();
		//remove from HeroManager
		//unsubscribe
		GameManager.Instance.e_PlayerStartPhase -= DecreaseDuration;
		Destroy(this);		
	}

	public void AddIcon()
	{
		buffPanel.AddIcon (buff.buff.ToString(), buffIcon, duration);

	}

	public void RemoveIcon()
	{
		buffPanel.RemoveIcon (buff.buff.ToString());
	}




}
