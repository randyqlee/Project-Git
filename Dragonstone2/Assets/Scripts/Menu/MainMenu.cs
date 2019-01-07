using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public Animator transitionAnim;
	public string sceneName;
	public GameObject myCollection;


  private void Awake()
     {
         //Set screen size for Standalone
 #if UNITY_STANDALONE
         Screen.SetResolution(480, 800, false);
         Screen.fullScreen = false;
 #endif
     }

	public HeroAsset[] heroAssets;


	public void PlayGame()
	{
		StartCoroutine (LoadScene("Menu_BattleScene"));

	}


	IEnumerator LoadScene (string sceneName)
	{
		transitionAnim.SetTrigger("start");
		yield return new WaitForSeconds (3f);
		SceneManager.LoadScene(sceneName);
	}
	

	public void MyCollection()
	{
		StartCoroutine (LoadMyCollection());

	}


	IEnumerator LoadMyCollection ()
	{
		Debug.Log("LoadCollection");
		transitionAnim.SetTrigger("start");
		yield return new WaitForSeconds (1.5f);
		//display mycollection screen
		myCollection.SetActive(true);



	}


	public void Options()
	{
		SceneManager.LoadScene("BattleScene");

	}

	public void Quit()
	{

		Application.Quit();

	}





	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
