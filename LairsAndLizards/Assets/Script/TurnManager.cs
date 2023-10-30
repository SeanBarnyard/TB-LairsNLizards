using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public UIscroller Uiscroll;
    public int turn = 0;
    List<StatSheet> Lizard = new List<StatSheet>();
    public List<GameObject> objectTurn;
    public StatSheet StrLiz = new StatSheet(), DexLiz = new StatSheet(), IntLiz = new StatSheet(), defaultlizz = new StatSheet();
    List<GameObject> actors = new List<GameObject>();

    bool selectTargetMode;
    [SerializeField]GameObject selectedTarget;
    Attacks attackToUse = new Attacks();

    private void Awake()
    {
        MakeLizard();
        foreach (GameObject actor in GameObject.FindGameObjectsWithTag("Actor"))
        {
            int randomliz = Random.Range(0,Lizard.Count);
            actors.Add(actor);
            if(actor.TryGetComponent(out Character character))
            {
                if (character.actorNumber == 0) character.stats = Globals.instance.member1;
                if (character.actorNumber == 1) character.stats = Globals.instance.member2;
                if (character.actorNumber == 2) character.stats = Globals.instance.member3;
                if (character.actorNumber == 3) character.stats = Lizard[randomliz];
                if (character.actorNumber == 4) character.stats = Lizard[randomliz];
                if (character.actorNumber == 5) character.stats = Lizard[randomliz];
                character.UpdateStats();
            }
        }
        Initiative();
        Globals.instance.charecterTurn = objectTurn[0];
        Uiscroll.Characters = objectTurn;
    }

    
    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D col = Physics2D.OverlapPoint(mousePos, LayerMask.GetMask("Actor"));
        if (col != null)
        {
            selectedTarget = col.gameObject;
        }
        else selectedTarget = null;

        if (selectTargetMode)
        {
            if(selectedTarget != null && Input.GetMouseButton(0))
            {
                List<GameObject> targets = new List<GameObject>(); 
                if (attackToUse.targetGroup)
                {
                    foreach (GameObject item in actors)
                    {
                        Character character = item.GetComponent<Character>();
                        if (character.targetable) targets.Add(item);
                    }
                        
                }
                else
                {
                    targets.Add(selectedTarget);
                }
                selectTargetMode = false;
                UseAttack(targets);
            }

        }

    }

    public void NextTurn()
    {
        turn++;
        if(turn >= objectTurn.Count) turn = 0;
        Globals.instance.charecterTurn = objectTurn[turn];
        Uiscroll.imageShift = true;
        if (Globals.instance.charecterTurn.GetComponent<Character>().dead) NextTurn();
    }

    public void SelectAttack(int slot)
    {
        if (selectTargetMode == false)
        {
            Character actor = Globals.instance.charecterTurn.GetComponent<Character>();
            StatSheet ss = actor.stats;
            Attacks attack = new Attacks();
            if (slot == 1) attack = ss.attack1;
            else if (slot == 2) { attack = ss.attack2; actor.atk2Up = false; }
            else if (slot == 2) { attack = ss.attack3; actor.atk2Up = false; }
            else if (slot == 3) { attack = ss.attack4; actor.atk2Up = false; }
            if (actor.player)
            {
                bool targetTeam = false;
                if (attack.targetTeam) targetTeam = true;
                HighlightTargets(targetTeam);
                attackToUse = attack;
                selectTargetMode = true;
            }
            else
            {
                bool targetTeam = false;
                if (attack.targetTeam) targetTeam = true;
                HighlightTargets(targetTeam);
                attackToUse = attack;
                selectTargetMode = true;
            }


        }

    }

    void HighlightTargets(bool playerTeam)
    {
        
        foreach (GameObject item in actors)
        {
            Character chars = item.GetComponent<Character>();
            if (playerTeam && chars.actorNumber <= 2) chars.targetable = true;
            else if (!playerTeam && chars.actorNumber > 2) chars.targetable = true;
        }
        
    }

    public void UseAttack(List<GameObject> targets)
    {
        foreach (GameObject actor in targets)
        {
            Character character = actor.GetComponent<Character>();
            int damage = 0;


        }
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
            //Debug.Log(dexRoll);
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
    void MakeLizard()
    {
        
        //Str Lizard
        StrLiz.name = "Bricked up Benny"; StrLiz.vitality = 20;
        StrLiz.baseStr = 4; StrLiz.baseDex = 2; StrLiz.baseInt = 3;
        StrLiz.attack1 = Globals.instance.basic; StrLiz.attack2 = Globals.instance.tSwipe;
        Lizard.Add(StrLiz);

        //Dex Lizard
        DexLiz.name = "Fast and Lizard"; DexLiz.vitality = 15;
        DexLiz.baseStr = 2; DexLiz.baseDex = 4; DexLiz.baseInt = 3;
        DexLiz.attack1 = Globals.instance.basic; DexLiz.attack2 = Globals.instance.sStorm;
        Lizard.Add(DexLiz);

        //Int Lizard
        IntLiz.name = "Demetrius Demarcus Bartholomew James The Third"; IntLiz.vitality = 10;
        IntLiz.baseStr = 2; IntLiz.baseDex = 3; IntLiz.baseInt = 4;
        IntLiz.attack1 = Globals.instance.basic; IntLiz.attack2 = Globals.instance.fBreath;
        Lizard.Add(IntLiz);

        //DefaultLizard
        defaultlizz.name = "Default Lizard Dance"; defaultlizz.vitality = 15;
        defaultlizz.baseStr = 3; defaultlizz.baseDex = 3; defaultlizz.baseInt = 3;
        defaultlizz.attack1 = Globals.instance.basic; defaultlizz.attack2 = Globals.instance.gasReg;
        Lizard.Add(defaultlizz);


        Sprite[] sprites = Resources.LoadAll<Sprite>("Lizardos");
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == "STRLizard") StrLiz.sprite = sprites[i];
            if (sprites[i].name == "DexLizard") DexLiz.sprite = sprites[i];
            if (sprites[i].name == "IntLizard") IntLiz.sprite = sprites[i];
            if (sprites[i].name == "Lizard") defaultlizz.sprite = sprites[i];
        }
    }

    //void Death()
    //{
    //    foreach (GameObject actor in actors)
    //    {
    //        if (actor.TryGetComponent(out Character character))
    //        {
    //            if(character.stats.vitality <= 0)
    //            {
    //                character.dead = true;
    //            }
    //        }
    //    }
    //}

    void ApplyDebuff()
    {
        //stun


        //dot

        //statmod

        //silenced
    }

}
