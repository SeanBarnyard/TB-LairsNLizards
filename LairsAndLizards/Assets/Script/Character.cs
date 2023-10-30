using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Character : MonoBehaviour
{
    public int actorNumber;
    public StatSheet stats = new StatSheet();
    public Equipment item1 = new Equipment(), item2 = new Equipment();
    public List<Modifiers> buffs = new List<Modifiers>();
    public Slider HpBar;
    public int strength, dexterity, intelligence, hp;
    public bool player, atk2Up = true, atk3Up = true, atk4Up = true, dead, targetable;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateStats();
        
    }

    private void Start()
    {
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

                hp -= buffs[i].dot;
                buffs[i].duration -= 1;
            }
        }

        

        strength = Mathf.Clamp(stats.baseStr + item1.strMod + item2.strMod + buffStr, 0, 20);
        dexterity = Mathf.Clamp(stats.baseDex + item1.dexMod + item2.dexMod + buffDex, 0, 20);
        intelligence = Mathf.Clamp(stats.baseInt + item1.intMod + item2.intMod + buffInt, 0, 20);
    }

    private void Update()
    {
        dead = hp <= 0;

        foreach (var buff in buffs)
        {
            if(buff.duration <= 0)
            {
                buffs.Remove(buff);
                return;
            }
        }

    }

    private void LateUpdate()
    {
        spriteRenderer.sprite = stats.sprite;
        HpBar.value = (float)hp / (float)stats.vitality;
    }


}






