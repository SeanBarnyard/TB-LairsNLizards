using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class TurnManager : MonoBehaviour
{
    public UIscroller Uiscroll;
    public int turn = 0;
    List<StatSheet> Lizard = new List<StatSheet>();
    public List<GameObject> objectTurn;
    public StatSheet StrLiz = new StatSheet(), DexLiz = new StatSheet(), IntLiz = new StatSheet(), defaultlizz = new StatSheet();
    List<GameObject> actors = new List<GameObject>();
    public GameObject turnIndicator;

    public bool selectTargetMode;
    GameObject selectedTarget;
    public Attacks attackToUse = new Attacks();

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

    public void ResetLizards()
    {
        objectTurn.Clear();
        actors.Clear();
        Globals.instance.wave += 1;
        foreach (GameObject actor in GameObject.FindGameObjectsWithTag("Actor"))
        {
            int randomliz = Random.Range(0, Lizard.Count);
            actors.Add(actor);
            if (actor.TryGetComponent(out Character character))
            {
                if (character.actorNumber == 0 || character.actorNumber == 1 || character.actorNumber == 2)
                {
                    Instantiate(Resources.Load("HealEffect"), actor.transform.position, Quaternion.identity);
                    character.hp += 10;
                    character.atk2Up = true;
                    character.atk3Up = true;
                    character.atk4Up = true;
                }
                if (character.actorNumber == 3 || character.actorNumber == 4 || character.actorNumber == 5)
                {
                    character.stats = Lizard[randomliz];
                    character.hp = character.stats.vitality;
                }
                character.UpdateStats();
            }
        }
        Initiative();
        Globals.instance.charecterTurn = objectTurn[0];
        Uiscroll.Characters = objectTurn;
    }

    bool CheckTeamWipe()
    {
        int players = 0, lizards = 0;
        foreach (GameObject actor in GameObject.FindGameObjectsWithTag("Actor"))
        {
            if (actor.TryGetComponent(out Character character))
            {
                if ((character.actorNumber == 0 || character.actorNumber == 1 || character.actorNumber == 2) && !character.dead) players++;
                if ((character.actorNumber == 3 || character.actorNumber == 4 || character.actorNumber == 5) && !character.dead) lizards++;
            }
        }
        if (lizards == 0)
        {           
            ResetLizards();
            return true;
        }
        else if (players == 0)
        {
            Globals.instance.GoToScene("EndScreen");
            return true;
        }
        else return false;
    }


    private void Update()
    {
        Globals.instance.charecterTurn = objectTurn[turn];
        Character characterTurn= null;
        if (objectTurn[turn] != null)
        {
            turnIndicator.transform.position = objectTurn[turn].transform.position;
            characterTurn = objectTurn[turn].GetComponent<Character>();
        }
        SelectTarget();

        if (characterTurn != null)
        {
            if (!characterTurn.player) aiTurn();
        }
       

    }

    float aiTimer = 0;
    void aiTurn()
    {
        aiTimer += Time.deltaTime;
        if (aiTimer < 1) return;
        // Get Attack -----------------------------------------------------------------------------------------------//
        Character characterTurn = objectTurn[turn].GetComponent<Character>();
        List<Attacks> attacks = new List<Attacks>();
        attacks.Add(characterTurn.stats.attack1);
        if (characterTurn.atk2Up && characterTurn.stats.attack2.id != 0) attacks.Add(characterTurn.stats.attack2);
        if (characterTurn.atk3Up && characterTurn.stats.attack3.id != 0) attacks.Add(characterTurn.stats.attack3);
        if (characterTurn.atk4Up && characterTurn.stats.attack4.id != 0) attacks.Add(characterTurn.stats.attack4);
        attackToUse = attacks[Random.Range(0,attacks.Count)];
        //-----------------------------------------------------------------------------------------------------------//
        bool targetPlayerTeam = true;
        if (attackToUse.targetTeam) targetPlayerTeam = false;
        HighlightTargets(targetPlayerTeam);
        // Get Target -----------------------------------------------------------------------------------------------//
        List<GameObject> targets = new List<GameObject>();
        foreach (var actor in actors)
        {
            Character character = actor.GetComponent<Character>();
            if (character.targetable && !character.dead) targets.Add(actor);
        }
        int rng1 = Random.Range(0,2);
        if (!attackToUse.targetGroup)
        {
            GameObject target = null;
            if (rng1 == 0)
            {
                target = targets[Random.Range(0, targets.Count)];
            }
            else if (rng1 == 1)
            {
                int lowestHp = 10000; // cant get infinite as an int :/
                for (int i = 0; i < targets.Count; i++)
                {
                    Character character = targets[i].GetComponent<Character>();
                    if(character.hp < lowestHp)
                    {
                        target = actors[i];
                        lowestHp = character.hp;
                    }
                }
            }
            targets.Clear();
            targets.Add(target);
        }
        //-----------------------------------------------------------------------------------------------------------//
        aiTimer = 0;
        UseAttack(targets);
    }

    void SelectTarget()
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
            if (selectedTarget != null && Input.GetMouseButton(0))
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
                if (TauntingTargets() != null)
                {
                    targets.Clear();
                    targets.Add(TauntingTargets());
                }
                selectTargetMode = false;
                UseAttack(targets);
            }
        }
    }

    public void NextTurn()
    {
        foreach (var actor in actors)
        {
            actor.GetComponent<Character>().targetable = false;
        }
        if (CheckTeamWipe()) return;
        objectTurn[turn].GetComponent<Character>().UpdateStats();
        turn++;
        if(turn >= objectTurn.Count) turn = 0;
        Globals.instance.charecterTurn = objectTurn[turn];
        objectTurn[turn].GetComponent<Character>().NewTurn();
        objectTurn[turn].GetComponent<Character>().UpdateStats();
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
                bool targetPlayerTeam = false;
                if (attack.targetTeam) targetPlayerTeam = true;
                HighlightTargets(targetPlayerTeam);
                attackToUse = attack;
                selectTargetMode = true;
            }
            else
            {
                bool targetPlayerTeam = true;
                if (attack.targetTeam) targetPlayerTeam = false;
                HighlightTargets(targetPlayerTeam);
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
        Globals g = Globals.instance;
        Character c = g.charecterTurn.GetComponent<Character>();
        
        foreach (GameObject actor in targets)
        {
            Debug.Log("Working");
            bool hit = true;
            int strRoll = g.DiceRoll(6,1), dexRoll = g.DiceRoll(6, 1), intRoll = g.DiceRoll(6, 1);
            Character character = actor.GetComponent<Character>();
            int damage = attackToUse.baseDamage;
            if (attackToUse.usesStr) damage += c.strength;
            if (attackToUse.usesDex) damage += c.dexterity;
            if (attackToUse.usesInt) damage += c.intelligence;
            if (attackToUse.strRoll) damage += strRoll;
            if (attackToUse.dexRoll) damage += dexRoll;
            if (attackToUse.intRoll) damage += intRoll;

            if (!attackToUse.neverMiss && (dexRoll + c.dexterity <= 5)) hit = false;

            if (hit)
            {
                Character targetChar = actor.GetComponent<Character>();
                //damage = Mathf.Clamp(damage, 0, 100);
                targetChar.hp -= damage;
                if (damage > 0) Instantiate(Resources.Load("HitEffect"), actor.transform.position, Quaternion.identity);
                if (damage < 0) Instantiate(Resources.Load("HealEffect"), actor.transform.position, Quaternion.identity);
                if (targetChar.hp <= 0) targetChar.dead = true;
                if (attackToUse.mods.Count > 0)
                {
                    bool save = true;
                    if (targetChar.strength + g.DiceRoll(4, 1) < attackToUse.strSave) save = false;
                    if (targetChar.dexterity + g.DiceRoll(4, 1) < attackToUse.dexSave) save = false;
                    if (targetChar.intelligence + g.DiceRoll(4, 1) < attackToUse.intSave) save = false;

                    if (!save)
                    {
                        foreach (Modifiers mod in attackToUse.mods)
                        {
                            targetChar.buffs.Add(mod);
                        }
                    }
                    else
                    {
                        Instantiate(Resources.Load("SaveEffect"), actor.transform.position, Quaternion.identity);
                        Debug.Log("Saved");
                    }
                }
                targetChar.UpdateStats();
            }
            else
            {
                Instantiate(Resources.Load("MissEffect"), actor.transform.position, Quaternion.identity);
                Debug.Log("You missed");
            }
        }

        attackToUse = null;
        NextTurn();

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

    GameObject TauntingTargets()
    {
        GameObject taunter = null;
        foreach (GameObject actor in actors)
        {
            Character character = actor.GetComponent<Character>();
            bool attackerIsPlayer = objectTurn[turn].GetComponent<Character>().player;
            bool actorIsPlayer = character.player;

            if (attackerIsPlayer && !actorIsPlayer && character.taunting) taunter = actor;
            if (!attackerIsPlayer && actorIsPlayer && character.taunting) taunter = actor;

        }

        return taunter;
    }

    void MakeLizard()
    {
        
        //Str Lizard
        StrLiz.name = "Bricked up Benny"; StrLiz.vitality = 20;
        StrLiz.baseStr = 4; StrLiz.baseDex = 3; StrLiz.baseInt = 3;
        StrLiz.attack1 = Globals.instance.basic; StrLiz.attack2 = Globals.instance.tSwipe;
        StrLiz.attack3 = Globals.instance.empty; StrLiz.attack4 = Globals.instance.empty;
        Lizard.Add(StrLiz);

        //Dex Lizard
        DexLiz.name = "Fast and Lizard"; DexLiz.vitality = 15;
        DexLiz.baseStr = 2; DexLiz.baseDex = 5; DexLiz.baseInt = 3;
        DexLiz.attack1 = Globals.instance.basic; DexLiz.attack2 = Globals.instance.sStorm;
        DexLiz.attack3 = Globals.instance.empty; DexLiz.attack4 = Globals.instance.empty;
        Lizard.Add(DexLiz);

        //Int Lizard
        IntLiz.name = "Demetrius Demarcus Bartholomew James The Third"; IntLiz.vitality = 10;
        IntLiz.baseStr = 2; IntLiz.baseDex = 4; IntLiz.baseInt = 5;
        IntLiz.attack1 = Globals.instance.basic; IntLiz.attack2 = Globals.instance.fBreath;
        IntLiz.attack3 = Globals.instance.empty; IntLiz.attack4 = Globals.instance.empty;
        Lizard.Add(IntLiz);

        //DefaultLizard
        defaultlizz.name = "Default Lizard Dance"; defaultlizz.vitality = 15;
        defaultlizz.baseStr = 3; defaultlizz.baseDex = 3; defaultlizz.baseInt = 3;
        defaultlizz.attack1 = Globals.instance.basic; defaultlizz.attack2 = Globals.instance.gasReg;
        defaultlizz.attack3 = Globals.instance.empty; defaultlizz.attack4 = Globals.instance.empty;
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

}
