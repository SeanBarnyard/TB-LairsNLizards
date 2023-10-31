using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public StatSheet empty = new StatSheet(), squire = new StatSheet(), novice = new StatSheet(),
        delinquent = new StatSheet(), sonneteer = new StatSheet(), natureSprite = new StatSheet(), crossBearer = new StatSheet();
    public Button enterGame, party1, party2, party3;
    public TextMeshProUGUI descript, str, dex, wis;
    public Image pSprite1, pSprite2, pSprite3;

    private void Awake()
    {
        MakeClasses();
        Globals.instance.member1 = empty;
        Globals.instance.member2 = empty;
        Globals.instance.member3 = empty;
    }

    private void Update()
    {
        Globals g = Globals.instance;
        if (g.member1 != empty && g.member2 != empty && g.member3 != empty) enterGame.interactable = true;
        else enterGame.interactable = false;

        if (g.member1 != empty)
        {
            pSprite1.sprite = g.member1.sprite;
            pSprite1.color = Color.white;
        }
        else pSprite1.color = new Color(0,0,0,0);
        if (g.member2 != empty)
        {
            pSprite2.sprite = g.member2.sprite;
            pSprite2.color = Color.white;
        }
        else pSprite2.color = new Color(0, 0, 0, 0);
        if (g.member3 != empty)
        {
            pSprite3.sprite = g.member3.sprite;
            pSprite3.color = Color.white;
        }
        else pSprite3.color = new Color(0, 0, 0, 0);


    }

    public void FillSlot(int job)
    {
        StatSheet classSheet = empty;
        if (job == 0) classSheet = squire; else if (job == 1) classSheet = novice; else if (job == 2) classSheet = delinquent;
        else if (job == 3) classSheet = natureSprite; else if (job == 4) classSheet = sonneteer; else if (job == 5) classSheet = crossBearer;

        if (Globals.instance.member1 == empty) Globals.instance.member1 = classSheet;
        else if (Globals.instance.member2 == empty) Globals.instance.member2 = classSheet;
        else if (Globals.instance.member3 == empty) Globals.instance.member3 = classSheet;

        if(classSheet != empty)
        {
            descript.text = classSheet.name;
            str.text = classSheet.baseStr.ToString();
            dex.text = classSheet.baseDex.ToString();
            wis.text = classSheet.baseInt.ToString();
        }
        

    }

    public void EmptySlot(int member)
    {
        if (member == 1) Globals.instance.member1 = empty;
        if (member == 2) Globals.instance.member2 = empty;
        if (member == 3) Globals.instance.member3 = empty;
    }

    public void SceneGoto(string scene)
    {
        Globals.instance.GoToScene(scene);
    }

    void MakeClasses()
    {
        Globals g = Globals.instance;
        //Stats gotta equal a total of 13

        //Empty
        empty.name = "Empty";

        //Squire
        squire.name = "Squire"; squire.vitality = 30;
        squire.baseStr = 6; squire.baseDex = 5; squire.baseInt = 2;
        squire.attack1 = g.basic; squire.attack2 = g.kAbility; squire.attack3 = g.empty; squire.attack4 = g.empty;

        //Novice
        novice.name = "Novice"; novice.vitality = 20;
        novice.baseStr = 2; novice.baseDex = 4; novice.baseInt = 7;
        novice.attack1 = g.basic; novice.attack2 = g.wAbility; novice.attack3 = g.empty; novice.attack4 = g.empty;

        //Delinquent
        delinquent.name = "Delinquent"; delinquent.vitality = 20;
        delinquent.baseStr = 4; delinquent.baseDex = 6; delinquent.baseInt = 3;
        delinquent.attack1 = g.basic; delinquent.attack2 = g.rAbility; delinquent.attack3 = g.empty; delinquent.attack4 = g.empty;

        //Sonneteer
        sonneteer.name = "Sonneteer"; sonneteer.vitality = 20;
        sonneteer.baseStr = 4; sonneteer.baseDex = 4; sonneteer.baseInt = 5;
        sonneteer.attack1 = g.basic; sonneteer.attack2 = g.bAbility; sonneteer.attack3 = g.empty; sonneteer.attack4 = g.empty;

        //NatureSprite
        natureSprite.name = "NatureSprite"; natureSprite.vitality = 20;
        natureSprite.baseStr = 5; natureSprite.baseDex = 4; natureSprite.baseInt = 4;
        natureSprite.attack1 = g.basic; natureSprite.attack2 = g.dAbility; natureSprite.attack3 = g.empty; natureSprite.attack4 = g.empty;

        //CrossBearer
        crossBearer.name = "CrossBearer"; crossBearer.vitality = 20;
        crossBearer.baseStr = 6; crossBearer.baseDex = 1; crossBearer.baseInt = 6;
        crossBearer.attack1 = g.basic; crossBearer.attack2 = g.pAbility; crossBearer.attack3 = g.empty; crossBearer.attack4 = g.empty;

        //Sprites for all, shouuuuuddda just made multiple sprite textues very epic
        Sprite[] sprites = Resources.LoadAll<Sprite>("The Boys");
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == "Squire") squire.sprite = sprites[i];
            if (sprites[i].name == "Novice") novice.sprite = sprites[i];
            if (sprites[i].name == "Delinquent") delinquent.sprite = sprites[i];
            if (sprites[i].name == "Sonneteer") sonneteer.sprite = sprites[i];
            if (sprites[i].name == "NatureSprite") natureSprite.sprite = sprites[i];
            if (sprites[i].name == "CrossBearer") crossBearer.sprite = sprites[i];
        }

    }
}
