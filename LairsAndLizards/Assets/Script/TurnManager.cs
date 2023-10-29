using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int turn = 0;
    public List<GameObject> objectTurn;
    List<GameObject> actors = new List<GameObject>();

    private void Awake()
    {
        foreach (GameObject actor in GameObject.FindGameObjectsWithTag("Actor"))
        {
            actors.Add(actor);
            if(actor.TryGetComponent(out Character character))
            {
                if (character.actorNumber == 0) character.stats = Globals.instance.member1;
                if (character.actorNumber == 1) character.stats = Globals.instance.member2;
                if (character.actorNumber == 2) character.stats = Globals.instance.member3;
                character.UpdateStats();
            }
        }
        Initiative();
        Globals.instance.charecterTurn = objectTurn[0];
        
    }

    public void NextTurn()
    {
        turn++;
        if(turn >= objectTurn.Count) turn = 0;
        Globals.instance.charecterTurn = objectTurn[turn];
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
