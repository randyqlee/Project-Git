using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    // for Singleton Pattern
    public static BattleManager Instance{ get; private set; }
    public TurnManager tm; 
    public List<HeroLogic> CreaturesCreatedThisGame;
    public Player[] Players;

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
        InitLists();
        StartCoroutine (BattleLoop());      
    }

    void InitLists()
    {
        CreaturesCreatedThisGame.Clear();
        
        Player[] Players = new Player[2]; 
        Players = GameObject.FindObjectsOfType<Player>();       
        
    }

	IEnumerator BattleLoop()
	{
        yield return StartCoroutine(InitBattle());

        yield return StartCoroutine(StartBattle());

        StopCoroutine(InitBattle());

    }

    IEnumerator InitBattle()
    {

        //initialize Heroes (Logic)
        //initialize Visuals
        //set Global values
        //set who starts first

        //clear arrays



        //initialize Players

        

        yield return null;
        StopCoroutine(InitBattle());

    }

    IEnumerator StartBattle()
    {

        //start ATB and TurnManager, other systems


        yield return null;
        StopCoroutine(StartBattle());

    }



}
