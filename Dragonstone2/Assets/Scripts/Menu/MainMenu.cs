using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public Animator transitionAnim;
	public string sceneName;


	public void PlayGame()
	{
		StartCoroutine (LoadScene("BattleScene"));

	}

	IEnumerator LoadScene (string sceneName)
	{
		transitionAnim.SetTrigger("end");
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene(sceneName);
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
