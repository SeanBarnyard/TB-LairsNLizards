using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public StatSheet stats = new StatSheet(); 
    public int strength, dexterity, intelligence;
    


}

public class StatSheet
{
    public int vitality=10, hp=10;
    public int baseStr = 3, baseDex = 3, baseInt = 3;
}

public class Equipment
{

}



