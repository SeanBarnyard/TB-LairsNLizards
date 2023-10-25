using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public StatSheet stats = new StatSheet();
    public Equipment item1 = new Equipment(), item2 = new Equipment();
    public int strength, dexterity, intelligence, hp;
    public bool player;

    private void Awake()
    {
        UpdateStates();
        hp = stats.vitality;
    }

    public void UpdateStates()
    {
        strength = stats.baseStr + item1.strMod + item2.strMod;
        dexterity = stats.baseDex + item1.dexMod + item2.dexMod;
        intelligence = stats.baseInt + item1.intMod + item2.intMod;
    }

    private void Update()
    {
        
    }


}

public class StatSheet
{
    public string name = "nothing? Why nothing?";
    public Sprite sprite = null;
    public int vitality = 10;
    public int baseStr = 3, baseDex = 3, baseInt = 3;
}

public class Modifiers
{
    public string name = "Empty, we made an oopsie";
    public int duration = 0, dot = 0, str; //Dot if made negative makes a heal over time
    public bool taunt = false;


}





