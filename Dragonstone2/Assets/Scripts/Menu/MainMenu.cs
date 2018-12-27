using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public Animator transitionAnim;
	public string sceneName;
	public GameObject myCollection;


	public void PlayGame()
	{
		StartCoroutine (LoadScene("BattleScene"));

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
