using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    public List<Equipment> pool;

    private void Awake()
    {
        //Epmty Slot
        Equipment newItem = new Equipment();
        pool.Add(newItem);

        //Bracelet of Jim Bro
        newItem = new Equipment();
        newItem.flavor = "Increases Strength by 2";
        newItem.name = "Bracelet of Jim Bro"; newItem.strMod = 2;
        pool.Add(newItem);

        //Ring of the Nimbler
        newItem = new Equipment();
        newItem.flavor = "Increases Dexterity by 2";
        newItem.name = "Ring of the Nimbler"; newItem.dexMod = 2;
        pool.Add(newItem);

        //Circlet of Big think
        newItem = new Equipment();
        newItem.flavor = "Increases Intelligence by 2";
        newItem.name = "Circlet of Big think"; newItem.intMod = 2;
        pool.Add(newItem);

    }

}

public class Equipment
{
    public string name = "Empty slot", flavor = "Words words words words";
    public int strMod = 0, dexMod = 0, intMod = 0;
}
