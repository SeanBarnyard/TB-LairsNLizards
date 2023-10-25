using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int turn = 0;
    public List<GameObject> objectTurn;
    List<GameObject> actors;

    private void Start()
    {
        foreach (GameObject actor in GameObject.FindGameObjectsWithTag("Actor"))
        {
            actors.Add(actor);
        }
        Initiative();
        
    }

    private void Update()
    {


    }

    void Initiative()
    {
        List<GameObject> objectList = new List<GameObject>();
        List<Character> characterList = new List<Character>();
        List<int> iniRolls = new List<int>();
        for (int i = 0; i < actors.Count; i++)
        {
            objectList.Add(actors[i]);
        }
        for (int i = 0; i < objectList.Count; i++)
        {
            characterList.Add(objectList[i].GetComponent<Character>());
        }
        for (int i = 0; i < objectList.Count; i++)
        {
            int dexRoll = characterList[i].dexterity + Globals.instance.DiceRoll(6, 1);
            Debug.Log(dexRoll);
            iniRolls.Add(dexRoll);
        }

        for (int c = 0; c < characterList.Count; c++)
        {
            int HighestRoll = 0, element = 0;
            for (int i = 0; i < objectList.Count; i++)
            {
                if (iniRolls[i] > HighestRoll)
                {
                    HighestRoll = iniRolls[i];
                    element = i;
                }
            }
            objectTurn.Add(objectList[element]);
            objectList.RemoveAt(element);
            iniRolls.RemoveAt(element);
        }
        

    }


}
