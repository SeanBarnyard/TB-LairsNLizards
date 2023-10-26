using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static Globals instance;
    public StatSheet member1, member2, member3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
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

}
