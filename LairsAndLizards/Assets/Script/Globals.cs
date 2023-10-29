using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals : MonoBehaviour
{
    public static Globals instance;
    public StatSheet member1, member2, member3;
    public GameObject charecterTurn = null;

    public struct Attacks
    {
        public string name;
        public int id;
    }
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

    private void LateUpdate()
    {
        if (SceneManager.GetActiveScene().name == "StartScreen") GoToScene("PartySelect");
    }

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
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
        empty.name = "Empty"; empty.id = 0;
        //Basic player attacks
        basic.name = "Basic Attack"; basic.id = 1;
        wAbility.name = "FireBolt"; wAbility.id = 2;
        kAbility.name = "Protect"; kAbility.id = 3;
        bAbility.name = "C Major"; bAbility.id = 4;
        rAbility.name = "Weak Point"; rAbility.id = 5;
        pAbility.name = "Smite"; pAbility.id = 6;
        dAbility.name = "FireBolt"; dAbility.id = 7;
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
        tSwipe.name = "Tail Swipe"; tSwipe.id = 16;
        fBreath.name = "Fire Breath"; fBreath.id = 17;
        gasReg.name = "Gas Regurgatate"; gasReg.id = 18;
        sStorm.name = "Sandstorm"; sStorm.id = 19;

    }

}
