using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Debuff : MonoBehaviour{

	public DebuffAsset debuff;
	public Sprite debuffIcon;
	public int duration;
	public GameObject source;

	public BuffPanel buffPanel;


	public Debuff New (int duration, GameObject source)
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
		GameManager.Instance.e_PlayerEndPhase += DecreaseDuration;

		buffPanel = gameObject.GetComponentInChildren<BuffPanel>();
		AddIcon();

		gameObject.GetComponent<HeroManager>().UpdateUI();

	}

	public virtual void DecreaseDuration ()
	{
		if (GetComponentInParent<Player>().isActive)
		{
			this.duration--;
			if (this.duration == 0)
				Destroy (this);
			else
				buffPanel.UpdateBuffIconCD(debuff.debuff.ToString(), this.duration);
		}

	}

	public virtual void OnDestroy()
	{
		gameObject.GetComponent<HeroManager>().UpdateUI();
		RemoveIcon();
		//remove from HeroManager
		//unsubscribe
		GameManager.Instance.e_PlayerEndPhase -= DecreaseDuration;
		Destroy(this);
		
	}

	public void AddIcon()
	{
		buffPanel.AddIcon (debuff.debuff.ToString(), debuffIcon, duration);

	}

	public void RemoveIcon()
	{
		if(buffPanel != null)
		buffPanel.RemoveIcon (debuff.debuff.ToString());
	}




}
