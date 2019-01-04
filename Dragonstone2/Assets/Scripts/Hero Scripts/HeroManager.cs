﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// Gets the Hero SO values
public class HeroManager : MonoBehaviour
{
//	public HeroAsset heroAsset;

	public string heroName;
	public Sprite image;

	public Image glow;
	
	public int maxHealth;
	public int attack;
	public int defense;
	public float chance;
	public int shield;

	//public List<AbilityAsset> abilityAssets;
	public List<AbilityAsset2> abilityAssets;
	public List<Ability> abilities;

	public Rarity rarity;

	public CapsuleCollider2D col;

	public bool isSelected;
	public Player player;
	public bool isDead;

	public Text healthText;
	public Text attackText;
	public Text defenseText;
	public Text shieldText;

	public Text damageText;


		public Image heroPortrait;
	public List<Button> skillsBtn;

	public GameObject skillText;
	public GameObject heroPanel;

	public Image healthBar;

	public int origHealth;

	[HideInInspector]
	public int origAttack, origDefense, origShield;
	[HideInInspector]
	public float origChance;

	

	public delegate void Event_TakeDamage();
	public event Event_TakeDamage e_TakeDamage = delegate {};

	[Header("Buff Flags")]
	public bool hasCritical;
	public bool hasImmunity;
	public bool hasRevenge;
	public bool hasReflect;
	public bool hasDefender;
	public bool hasTaunt;
	public bool hasRecovery;
	public bool hasEndure;

	[Header("Debuff Flags")]
	public bool hasCrippledStrike;
	public bool hasEcho;
	public bool hasMalaise;
	public bool hasUnhealable;
	public bool hasAntiBuff;
	public bool hasBrand;

	public delegate void Event_PopupMsg(string message);
	public event Event_PopupMsg e_PopupMSG = delegate {};

	//Passive Skills
	[HideInInspector]
	public bool hasPermanentImmunity;


	void Awake () {

	col = GetComponent<CapsuleCollider2D>();
	isSelected = false;
	

	}
	// Use this for initialization
	void OnEnable () {
		isDead = false;	
	}
	
	// Update is called once per frame
	void Update () {


	}

	public void SelectHero()
	{
		GameManager.Instance.DeselectAllHeroes();
		isSelected = true;
		glow.GetComponent<Image>().color = new Color32 (26, 255, 53, 255);

		heroPanel.SetActive(true);



	}

	public void DeselectHero()
	{
		//Debug.Log ("Deselecting " + name);
		isSelected = false;

		heroPanel.SetActive(false);
		glow.GetComponent<Image>().color = new Color32 (195, 71, 91, 255);
	}

	public void DisplayHero()
	{
		// Debug.Log("Hero: " + heroName);
		// Debug.Log("Health: " + maxHealth);
		// Debug.Log("Attack: " + attack);
		// Debug.Log("Defense: " + defense);
		// Debug.Log("chance: " + chance);
	}

	public void HeroStats()
	{

	}

	public void UpdateUI ()
	{
		healthText.text = maxHealth.ToString();
		attackText.text = attack.ToString();
		defenseText.text = defense.ToString();
		shieldText.text = shield.ToString();
		UpdateHealthBar();
	}

	public void CreateHeroPanel()
	{

		heroPanel = Instantiate(heroPanel);
		//heroPanel.SetActive(true);
		heroPanel.GetComponent<HeroPanel>().hero = this;
		heroPanel.GetComponent<HeroPanel>().CreateHeroPanel();
		
		heroPanel.transform.SetParent(transform);


		Ability[] abilitys = heroPanel.GetComponentsInChildren<Ability>();
		for(int i=0; i < abilitys.Length; i++) {
			abilitys[i].skillType = heroPanel.GetComponentInParent<HeroManager>().abilityAssets[i].skillType;
			
			//all abilities available at the start of the turn
			abilitys[i].remainingCooldown = 1;
		}


		heroPanel.SetActive(false);		

	}


	public void DisplayDamageText (int damage)
	{
		StartCoroutine (DisplayDamage (damage));
	}

	IEnumerator DisplayDamage (int damage)
	{
		damage = -1 * damage;

		damageText.text = damage.ToString();
		damageText.enabled = true;
		yield return new WaitForSeconds (1f);
		damageText.enabled = false;

		yield return null;
	}

	public void UpdateHealthBar()
	{
		healthBar.fillAmount = ((float) maxHealth)/origHealth;
	}

	public void TakeDamage(int damage, HeroManager source)
	{

		
		if(hasBrand){

			DebuffAsset debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Brand");
			int brandDamage = debuff.value;
			damage += brandDamage;			
			Debug.Log("Brand Damage: " +brandDamage);
		}

		if (shield <=0){
			shield = 0;
			maxHealth -=damage;
			e_TakeDamage();
			e_PopupMSG(damage.ToString());

		}else{			
			shield-= damage;
			e_TakeDamage();
			e_PopupMSG(damage.ToString());
			
		if(shield < 0){
			int netDamage = shield;
			shield = 0;
			maxHealth +=netDamage;
			e_TakeDamage();
			e_PopupMSG(damage.ToString());
		}//shield
		}	
		
		
	}

	public int TotalHealth
	{

		get
		{
			return (maxHealth + shield);
		}

		set
		{

		}
	}

	public void E_PopupMSG (string message)
	{
		e_PopupMSG (message);
	}

}
