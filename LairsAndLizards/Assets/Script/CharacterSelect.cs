using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public StatSheet squire, novice, delinquent, sonneteer, natureSprite, crossBearer;

    private void Awake()
    {
        MakeClasses();
    }


    void MakeClasses()
    {        
        //Stats gotta equal a total of 13

        //Squire
        squire.name = "Squire"; squire.vitality = 30;
        squire.baseStr = 6; squire.baseDex = 5; squire.baseInt = 2;

        //Novice
        novice.name = "Novice"; novice.vitality = 20;
        novice.baseStr = 2; novice.baseDex = 4; novice.baseInt = 7;

        //Delinquent
        delinquent.name = "Delinquent"; delinquent.vitality = 20;
        delinquent.baseStr = 4; delinquent.baseDex = 6; delinquent.baseInt = 3;

        //Sonneteer
        sonneteer.name = "Sonneteer"; sonneteer.vitality = 20;
        sonneteer.baseStr = 4; sonneteer.baseDex = 4; sonneteer.baseInt = 5;

        //NatureSprite
        natureSprite.name = "NatureSprite"; natureSprite.vitality = 20;
        natureSprite.baseStr = 5; natureSprite.baseDex = 4; natureSprite.baseInt = 4;

        //CrossBearer
        crossBearer.name = "CrossBearer"; crossBearer.vitality = 20;
        crossBearer.baseStr = 6; crossBearer.baseDex = 1; crossBearer.baseInt = 6;

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
