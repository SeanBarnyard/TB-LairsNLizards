using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals : MonoBehaviour
{
    public static Globals instance;
    public StatSheet member1, member2, member3;
    public GameObject charecterTurn = null;
    public int wave = 0;
    //public struct Attacks
    //{
    //    public string name;
    //    public int id;
    //}
    public List<Attacks> attackPool = new List<Attacks>();
    public Attacks empty = new Attacks(), basic = new Attacks(), kAbility = new Attacks(), wAbility = new Attacks(),
        rAbility = new Attacks(), bAbility = new Attacks(), pAbility = new Attacks(), dAbility = new Attacks(),
        pSand = new Attacks(), cleanse = new Attacks(), flail = new Attacks(), charge = new Attacks(), riddle = new Attacks(),
        boomer = new Attacks(), cMinor = new Attacks(), regen = new Attacks(), siphon = new Attacks(),
        tSwipe = new Attacks(), fBreath = new Attacks(), gasReg = new Attacks(), sStorm = new Attacks();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            MakeAttacks();
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
    }

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void QUITGAME()
    {
        Application.Quit();
    }
    public int DiceRoll(int face, int amount)
    {
        int total = 0;
        for (int i = 0; i < amount; i++)
        {
            total += Random.Range(1, face+1);
        }
        return total;
    }

    void MakeAttacks()
    {
        Modifiers mod = new Modifiers();
        empty.name = "Empty"; empty.id = 0;

        //Basic player attacks
        basic.name = "Basic Attack"; basic.id = 1; basic.usesStr = true; basic.baseDamage = 1; basic.description = "Smacks the opponent";

        wAbility.name = "FireBolt"; wAbility.id = 2; wAbility.intSave = 8; wAbility.usesInt = true; wAbility.description = "Launches a firebolt that can appliy a dot on enemies for 3 turns";
        mod.dot = 5; mod.duration = 3; wAbility.mods.Add(mod); mod = new Modifiers();

        kAbility.name = "Taunt"; kAbility.id = 3; kAbility.neverMiss = true; kAbility.strSave = 100; kAbility.targetTeam = true; kAbility.description = "Makes the enemy attack focused on you";
        mod.taunt = true; mod.duration = 2; kAbility.mods.Add(mod); mod = new Modifiers();

        bAbility.name = "C Major"; bAbility.id = 4; bAbility.neverMiss = true; bAbility.targetTeam = true; bAbility.baseDamage = -5; bAbility.strSave = 100; bAbility.description = "Heals an entity and buffs their stats";
        mod.str = 2; mod.dex = 2; mod.wis = 2; mod.duration = 2; bAbility.mods.Add(mod); mod = new Modifiers();

        rAbility.name = "Weak Point"; rAbility.id = 5; rAbility.neverMiss = true; rAbility.strRoll = true; rAbility.dexRoll = true; rAbility.intRoll = true; rAbility.description = "A heavy attack that never misses";

        pAbility.name = "Smite"; pAbility.id = 6; pAbility.neverMiss = true; pAbility.usesInt = true; pAbility.strRoll = true; pAbility.intRoll = true; pAbility.description = "A ray of Holy light that damages the target";

        dAbility.name = "Regen"; dAbility.id = 7; dAbility.neverMiss = true; dAbility.targetTeam = true; dAbility.targetGroup = true; dAbility.baseDamage = -1; dAbility.description = "A group heal that applies a healing dot";
        dAbility.strSave = 100;
        mod.dot = -5; mod.duration = 2; dAbility.mods.Add(mod); mod = new Modifiers();

        //Additional lootable player attacks
        pSand.name = "Pocket Sand"; pSand.id = 8; attackPool.Add(pSand);

        cleanse.name = "Cleanse"; cleanse.id = 9; attackPool.Add(cleanse);

        flail.name = "Flail"; flail.id = 7; attackPool.Add(flail);

        charge.name = "Reckless Charge"; charge.id = 10; attackPool.Add(charge);

        riddle.name = "Riddle of Immolation"; riddle.id = 11; attackPool.Add(riddle);

        boomer.name = "Boomerang"; boomer.id = 12; attackPool.Add(boomer);

        cMinor.name = "C Minor"; cMinor.id = 13; attackPool.Add(cMinor);

        regen.name = "Regen"; regen.id = 14; attackPool.Add(regen);

        siphon.name = "Siphon"; siphon.id = 15; attackPool.Add(siphon);

        //Enemy Attacks
        tSwipe.name = "Tail Swipe"; tSwipe.id = 16; tSwipe.baseDamage = 2; tSwipe.strRoll = true; tSwipe.targetGroup = true; tSwipe.description = "An aoe that deals dmg to the enemy team";

        fBreath.name = "Fire Breath"; fBreath.id = 17; fBreath.baseDamage = 1; fBreath.targetGroup = true; fBreath.intSave = 7; fBreath.description = "Breaths fire in an aoe to apply a dot";
        mod.dot = 4; mod.duration = 3; fBreath.mods.Add(mod); mod = new Modifiers();

        gasReg.name = "Gas Regurgatate"; gasReg.id = 18; gasReg.baseDamage = -3; gasReg.targetTeam = true; gasReg.dexSave = 12; gasReg.description = "Vomits on allies and heals them";
        mod.dot = -3; mod.duration = 2; dAbility.mods.Add(mod); mod = new Modifiers();

        sStorm.name = "Sandstorm"; sStorm.id = 19; sStorm.neverMiss = true; sStorm.dexRoll = true; sStorm.description = "Blinds an oppoonent with sand"; sStorm.dexSave = 100;
        mod.dex = -2; mod.duration = 1; sStorm.mods.Add(mod); mod = new Modifiers();

    }


}

public class Modifiers
{
    public string name = "Empty, we made an oopsie";
    public int duration = 0, dot = 0, str = 0, dex = 0, wis = 0; //Dot if made negative makes a heal over time, int already exists :(
    public bool taunt = false;
    public bool silence = false;
    public bool stun = false;

}

public class Attacks
{
    public string name = "empty", description = "Does a thing";
    public int id = -1;
    public int baseDamage = 0, strSave = 0, dexSave = 0, intSave = 0;
    public bool targetGroup = false, targetTeam = false, neverMiss = false,
        usesStr = false, usesDex = false, usesInt = false, 
        strRoll = false, dexRoll = false, intRoll = false;
    public List<Modifiers> mods = new List<Modifiers>();

}

public class StatSheet
{
    public string name = "nothing? Why nothing?";
    public Sprite sprite = null;
    public int vitality = 10;
    public int baseStr = 3, baseDex = 3, baseInt = 3;
    public Attacks attack1 = new Attacks(),
        attack2 = new Attacks(),
        attack3 = new Attacks(),
        attack4 = new Attacks();
}


