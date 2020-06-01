using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    // for Singleton Pattern
    public static TurnManager Instance{ get; private set; }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine (BattleLoop());      
    }

	IEnumerator BattleLoop()
	{
        yield return StartCoroutine(InitBattle());

        yield return StartCoroutine(StartBattle());

        StopCoroutine(InitBattle());

    }

    IEnumerator InitBattle()
    {

        yield return null;
        StopCoroutine(InitBattle());

    }

    IEnumerator StartBattle()
    {

        yield return null;
        StopCoroutine(StartBattle());

    }


}
