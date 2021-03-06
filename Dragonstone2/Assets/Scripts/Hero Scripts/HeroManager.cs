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
	public bool isActive;
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

	
	public int origAttack, origDefense, origShield;
	
	public float origChance;

	

	public delegate void Event_TakeDamage();
	public event Event_TakeDamage e_TakeDamage = delegate {};

	[Header("Buff Flags")]
	public bool hasCritical;
	public bool hasImmunity;
	public bool hasRevenge;
	public bool hasReflect;
	public bool hasThreat;
	public bool hasDefend;
	public bool hasTaunt;
	public bool hasRecovery;
	public bool hasEndure;
	public bool hasReduceCooldown;
	public bool hasProtectSoul;
	
	

	[Header("Debuff Flags")]
	public bool hasCrippledStrike;
	public bool hasEcho;
	public bool hasMalaise;
	public bool hasUnhealable;
	public bool hasAntiBuff;
	public bool hasBrand;
	public bool hasSilence;

	//[HideInInspector]
	public bool hitByCritical;
	//[HideInInspector]
	public HeroManager criticalSource;

	public delegate void Event_PopupMsg(string message);
	public event Event_PopupMsg e_PopupMSG = delegate {};

	//Passive Skills
	[HideInInspector]
	public bool hasPermanentImmunity;

	//[HideInInspector]
	public bool hasExtraTurn;

	//tracks last damage dealt
	[HideInInspector]
	public int lethalDamage;

	void Awake () {

	col = GetComponent<CapsuleCollider2D>();
	isSelected = false;
	

	}
	// Use this for initialization
	void OnEnable () {
		isDead = false;	
	}
	
	void OnDisable(){
		
		Buff[] buffs = this.GetComponents<Buff>();
		
			foreach(Buff buff in buffs){					
				Destroy(buff);				
			}

		//Destroy all debuffs
		Debuff[] debuffs = this.GetComponents<Debuff>();
		
			foreach(Debuff debuff in debuffs){					
				Destroy(debuff);					
			}		

			//Disable Passive Abilities		
		this.transform.Find("HeroPanel(Clone)").gameObject.SetActive(true);	
		
		Ability[] abilities = this.GetComponentsInChildren<Ability>();
		
			foreach(Ability ability in abilities){
					
					ability.DisableAbilityPassive();
					ability.DisableAbilityActive();
					
				}
		this.transform.Find("HeroPanel(Clone)").gameObject.SetActive(false);
	}
	
	
	// Update is called once per frame
	void Update () {


	}

	public void SelectHero()
	{
		GameManager.Instance.DeselectAllHeroes();
		isSelected = true;
		isActive = true;
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

	public void DeselectHeroPanel()
	{
		
		heroPanel.SetActive(false);	
	}

	public void DisplayHero()
	{
		
		
		GameManager.Instance.DeselectAllHeroPanels();
		heroPanel.SetActive(true);
		
		
		
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

		//Display 0 instead of a negative number
		if(attack < 0){
			attackText.text = "0";			
		} else {			
			attackText.text = attack.ToString();
		}
		
		//Display 0 instead of a negative number
		if(defense < 0){
			defenseText.text = "0";
		} else {
			defenseText.text = defense.ToString();
		}

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
			abilitys[i].remainingCooldown = 0;

			abilitys[i].GetComponentInChildren<Text>().text = abilitys[i].remainingCooldown.ToString();
		}
		heroPanel.SetActive(false);		

	}

	public void DisplayDamageText (int damage)
	{
		if(this.gameObject.activeSelf == true)
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

	public IEnumerator TakeDamageCoroutine(int damage, HeroManager source)
	{
		
		if(damage < 0)
			damage = 0;			

		if (shield <=0){
			shield = 0;
			maxHealth -=damage;
			
			//e_PopupMSG(damage.ToString());
			yield return StartCoroutine(PopUpMsgEvent(damage));

			//e_TakeDamage();
			yield return StartCoroutine(TakeDamageEvent());
			
			
			

		}else{			
			shield-= damage;

			//e_PopupMSG(damage.ToString());
			yield return StartCoroutine(PopUpMsgEvent(damage));
			
			//e_TakeDamage();
			yield return StartCoroutine(TakeDamageEvent());
		
			
			
			if(shield < 0){
				int netDamage = shield;
				shield = 0;
				maxHealth +=netDamage;
				
				//e_PopupMSG(damage.ToString());
				yield return StartCoroutine(PopUpMsgEvent(damage));
			
				//e_TakeDamage();
				yield return StartCoroutine(TakeDamageEvent());
			
				
				
			}//shield
		}							
				
		//GameManager.Instance.CheckHealth();
		yield return StartCoroutine(CheckHealthMethod());

		yield return null;
	}//TakeDamage1

	public void TakeDamage(int damage, HeroManager source)
	{
		
		if(damage < 0)
			damage = 0;			

		if (shield <=0){
			shield = 0;
			maxHealth -=damage;
			
			//e_PopupMSG(damage.ToString());
			e_PopupMSG(damage.ToString());

			//e_TakeDamage();
			e_TakeDamage();
			
			
			

		}else{			
			shield-= damage;

			//e_PopupMSG(damage.ToString());
			e_PopupMSG(damage.ToString());
			
			//e_TakeDamage();
			e_TakeDamage();
		
			
			
			if(shield < 0){
				int netDamage = shield;
				shield = 0;
				maxHealth +=netDamage;
				
				//e_PopupMSG(damage.ToString());
				e_PopupMSG(damage.ToString());
			
				//e_TakeDamage();
				e_TakeDamage();
			
				
				
			}//shield
		}							
				
		//GameManager.Instance.CheckHealth();
		GameManager.Instance.CheckHealth();

		
	}//TakeDamage1

	public IEnumerator PopUpMsgEvent(int damage){

		e_PopupMSG(damage.ToString());
		yield return null;
	}

	public IEnumerator TakeDamageEvent(){

		e_TakeDamage();
		yield return null;
	}

	public IEnumerator CheckHealthMethod(){

		GameManager.Instance.CheckHealth();
		yield return null;
	}


	public void Healhero (HeroManager target, int healValue)
	{
		if (!target.hasUnhealable)
		{
	
			target.maxHealth += healValue;
			if(target.maxHealth > target.origHealth){
				target.maxHealth = target.origHealth;
				e_PopupMSG("Heal: " +healValue.ToString());
				target.UpdateUI();
				
			}

		} else {
			Debug.Log("Target is Unhealable");
		}
	}//Heal

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
