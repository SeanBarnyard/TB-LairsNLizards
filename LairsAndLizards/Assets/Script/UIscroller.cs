using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIscroller : MonoBehaviour
{
    public List<GameObject>  CharImage,Characters;
    public List<Image> CharIcon;
    public List<Sprite> Charsprites;

    public Button atk1, atk2, atk3, atk4;


    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent(out Image imig))
            {
                if (imig.CompareTag("TopIcon"))
                {
                    CharImage.Add(transform.GetChild(i).gameObject);
                    CharIcon.Add(imig);
                }
                
            } 
        }
        foreach (var Character in GameObject.FindGameObjectsWithTag("Actor"))
        {
            Characters.Add(Character);
        }
        foreach (var Charact in Characters)
        {
            Charsprites.Add(Charact.GetComponent<SpriteRenderer>().sprite);
        }
        for (int i = 0; i < CharIcon.Count; i++)
        {
            for (int b = 0; b < Charsprites.Count; b++)
            {
                CharIcon[i].sprite = Charsprites[b];

            }
        }
        
    }

    void Update()
    {
        if(Globals.instance.charecterTurn != null)
        {
            if (Globals.instance.charecterTurn.TryGetComponent(out Character character))
            {
                if (character.player)
                {
                    atk1.interactable = true;
                    if (character.atk2Up && character.stats.attack2.id != 0) atk2.interactable = true; else atk2.interactable = false;
                    if (character.atk3Up && character.stats.attack3.id != 0) atk3.interactable = true; else atk3.interactable = false;
                    if (character.atk4Up && character.stats.attack4.id != 0) atk4.interactable = true; else atk4.interactable = false;
                }
                else
                {
                    atk1.interactable = false;
                    atk2.interactable = false;
                    atk3.interactable = false;
                    atk4.interactable = false;
                }

            }
        }

    }
}
