using System.Collections;
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

	public List<AbilityAsset> abilityAssets;
	public List<Ability> abilities;

	public Rarity rarity;

	public CapsuleCollider2D col;

	public bool isSelected;
	public Player player;

	public Text healthText;
	public Text attackText;
	public Text defenseText;

	public Text damageText;


	void Awake () {
//		if (heroAsset != null)
//			ReadHeroFromAsset();

	col = GetComponent<CapsuleCollider2D>();
	isSelected = false;


	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
/* 		HeroPanel heroPanel;

		if (GameObject.FindGameObjectWithTag("HeroPanel") != null && isSelected)
			{
				heroPanel = GameObject.FindGameObjectWithTag("HeroPanel").GetComponentInChildren<HeroPanel>();
				heroPanel.heroPortrait.sprite = image;
			}
*/


	}

	void ReadHeroFromAsset()
	{
/* 
		heroName = heroAsset.heroName;
		image = heroAsset.image;
		maxHealth = heroAsset.maxHealth;
		attack = heroAsset.attack;
		defense = heroAsset.defense;
		chance = heroAsset.chance;

		rarity = heroAsset.rarity;
*/
	}
/*
	void OnMouseDown ()
	{
		if (player.isActive)
		{
			Debug.Log("Hero clicked: " + heroName);
			GameManager.Instance.DeselectAllHeroes();
			SelectHero();
		}
	}
*/
	public void SelectHero()
	{
		Debug.Log("Hero: " + heroName);
		Debug.Log("Health: " + maxHealth);
		Debug.Log("Attack: " + attack);
		Debug.Log("Defense: " + defense);
		Debug.Log("chance: " + chance);
		GameManager.Instance.DeselectAllHeroes();
		isSelected = true;
		glow.GetComponent<Image>().color = new Color32 (26, 255, 53, 255);

		player.heroPanel.SetActive(true);
		//player.heroPanel.GetComponent<HeroPanel>().heroPortrait = image;
		player.heroPanel.GetComponent<HeroPanel>().hero = this;
		player.heroPanel.GetComponent<HeroPanel>().UpdateUI();

	//	player.heroPanel.GetComponent<HeroPanel>().heroPortrait.GetComponentInChildren<Image>().sprite = image;

				

	}

	public void DeselectHero()
	{
		Debug.Log ("Deselecting " + name);
		isSelected = false;
		player.heroPanel.SetActive(false);
		glow.GetComponent<Image>().color = new Color32 (195, 71, 91, 255);
	}

	public void DisplayHero()
	{
		Debug.Log("Hero: " + heroName);
		Debug.Log("Health: " + maxHealth);
		Debug.Log("Attack: " + attack);
		Debug.Log("Defense: " + defense);
		Debug.Log("chance: " + chance);
	}

	public void HeroStats()
	{

	}

	public void UpdateUI ()
	{
		healthText.text = maxHealth.ToString();
		attackText.text = attack.ToString();
		defenseText.text = defense.ToString();


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


}
