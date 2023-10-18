using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diceroll : MonoBehaviour
{
    int AmmountOfRoll;

        
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Roll(20,2);
        }
    }

    public int Roll(int Faces,int AmmountOfDices)
    {
        int total = 0;
        for (int i = 0; i < AmmountOfDices ; i++)
        {
             total += Random.Range(1, Faces + 1);
        }
        Debug.Log(total);
        return total;
    }
}
