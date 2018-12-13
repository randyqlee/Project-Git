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

	//public List<AbilityAsset> abilityAssets;
	public List<AbilityAsset2> abilityAssets;
	public List<Ability> abilities;

	public Rarity rarity;

	public CapsuleCollider2D col;

	public bool isSelected;
	public Player player;

	public Text healthText;
	public Text attackText;
	public Text defenseText;

	public Text damageText;


		public Image heroPortrait;
	public List<Button> skillsBtn;

	public GameObject skillText;
	public GameObject heroPanel;

	public Image healthBar;

	public int origHealth;

	public int shield;

	public delegate void Event_TakeDamage();
	public event Event_TakeDamage e_TakeDamage = delegate {};

	[Header("Buff Flags")]
	public bool hasCritical;
	public bool hasImmunity;
	public bool hasRevenge;
	public bool hasReflect;
	public bool hasDefender;

	[Header("Debuff Flags")]
	public bool hasCrippledStrike;
	public bool hasEcho;
	public bool hasMalaise;
	public bool hasUnhealable;
	void Awake () {

	col = GetComponent<CapsuleCollider2D>();
	isSelected = false;

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}

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

		heroPanel.SetActive(true);



	}

	public void DeselectHero()
	{
		Debug.Log ("Deselecting " + name);
		isSelected = false;

		heroPanel.SetActive(false);
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
		UpdateHealthBar();
	}

	public void CreateHeroPanel()
	{

		heroPanel = Instantiate(heroPanel);
		//heroPanel.SetActive(true);
		heroPanel.GetComponent<HeroPanel>().hero = this;
		heroPanel.GetComponent<HeroPanel>().CreateHeroPanel();
		
		heroPanel.transform.SetParent(transform);
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
		maxHealth = TotalHealth - damage;

		shield = shield - damage;
		if ( shield < 0 )
			shield = 0;

		e_TakeDamage();
		

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

}
