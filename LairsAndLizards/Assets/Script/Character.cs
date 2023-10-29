using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int actorNumber;
    public StatSheet stats = new StatSheet();
    public Equipment item1 = new Equipment(), item2 = new Equipment();
    public List<Modifiers> buffs = new List<Modifiers>();
    public int strength, dexterity, intelligence, hp;
    public bool player, atk2Up = true, atk3Up = true, atk4Up = true;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateStats();
        hp = stats.vitality;
    }

    public void UpdateStats()
    {
        int buffStr = 0, buffDex = 0, buffInt = 0;
        if(buffs.Count > 0)
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                buffStr += buffs[i].str;
                buffDex += buffs[i].dex;
                buffInt += buffs[i].wis;
            }
        }
        

        strength = Mathf.Clamp(stats.baseStr + item1.strMod + item2.strMod + buffStr, 0, 20);
        dexterity = Mathf.Clamp(stats.baseDex + item1.dexMod + item2.dexMod + buffDex, 0, 20);
        intelligence = Mathf.Clamp(stats.baseInt + item1.intMod + item2.intMod + buffInt, 0, 20);
    }

    private void Update()
    {
        if (Globals.instance.charecterTurn == gameObject)
        {


        }
    }

    private void LateUpdate()
    {
        spriteRenderer.sprite = stats.sprite;
    }


}

public class StatSheet
{
    public string name = "nothing? Why nothing?";
    public Sprite sprite = null;
    public int vitality = 10;
    public int baseStr = 3, baseDex = 3, baseInt = 3;
    public Globals.Attacks attack1 = Globals.instance.empty,
        attack2 = Globals.instance.empty,
        attack3 = Globals.instance.empty,
        attack4 = Globals.instance.empty;
}

public class Modifiers
{
    public string name = "Empty, we made an oopsie";
    public int duration = 0, dot = 0, str = 0, dex = 0, wis = 0; //Dot if made negative makes a heal over time, int already exists :(
    public bool taunt = false;
    public bool silence = false;


}





