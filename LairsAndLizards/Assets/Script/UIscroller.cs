using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIscroller : MonoBehaviour
{
    public List<GameObject>  CharImage,Characters;
    public List<Image> CharIcon;
    public TurnManager turnmanager;
    public Button atk1, atk2, atk3, atk4;
    public bool imageShift;
    public List<float> position = new List<float>();

    bool getList = true;
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Image imig))
            {
                if (imig.CompareTag("TopIcon"))
                {
                    if (!position.Contains(imig.rectTransform.localPosition.x)) position.Add(imig.rectTransform.localPosition.x);
                }

            }
        }
    }
    public void GetDeezChars()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent(out Image imig))
            {
                if (imig.CompareTag("TopIcon"))
                {
                    if(!CharImage.Contains(transform.GetChild(i).gameObject))CharImage.Add(transform.GetChild(i).gameObject);
                    if(!CharIcon.Contains(imig))CharIcon.Add(imig);
                }
                
            } 
        }
        for (int i = 0; i < Characters.Count; i++)
        {
           CharIcon[i].sprite = Characters[i].GetComponent<SpriteRenderer>().sprite;
        }

    }

    void Update()
    {
        if (imageShift)
        {
            Nextturnimagescroll();
        }
        if (!getList)
        {
            if (Globals.instance.charecterTurn != null)
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
                    atk1.GetComponentInChildren<TextMeshProUGUI>().text = character.stats.attack1.name;
                    atk2.GetComponentInChildren<TextMeshProUGUI>().text = character.stats.attack2.name;
                    atk3.GetComponentInChildren<TextMeshProUGUI>().text = character.stats.attack3.name;
                    atk4.GetComponentInChildren<TextMeshProUGUI>().text = character.stats.attack4.name;
                }
            }
            for (int i = 0; i < Characters.Count; i++)
            {
                if (Characters[i].GetComponent<Character>().dead)
                {
                    Characters[i].GetComponent<SpriteRenderer>().color = Color.red;
                    CharIcon[i].color = Color.red;
                }
                else if (Characters[i].GetComponent<Character>().targetable)
                {
                    Characters[i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0.5f, 1f);
                    CharIcon[i].color = new Color(0,0,0.5f,1f);
                }
                else
                {
                    Characters[i].GetComponent<SpriteRenderer>().color = Color.white;
                    CharIcon[i].color = Color.white;
                }

            }
        }
        
    }

    private void LateUpdate()
    {
        if (getList)
        {        
            getList = false;
        }
        GetDeezChars();
    }

    public void Nextturnimagescroll()
    {
        
        if (turnmanager.turn == 0)
        {
            CharIcon[0].rectTransform.localPosition = Vector2.MoveTowards(CharIcon[0].rectTransform.localPosition,new Vector2(position[5], CharIcon[0].rectTransform.localPosition.y), 100 * Time.deltaTime);
        }
       
    }
}
