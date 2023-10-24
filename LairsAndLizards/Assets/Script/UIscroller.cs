using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIscroller : MonoBehaviour
{
    public List<GameObject>  CharImage,Characters;
    public List<Image> CharIcon;
    public List<Sprite> Charsprites;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent(out Image imig))
            {
                CharImage.Add(transform.GetChild(i).gameObject);
                CharIcon.Add(imig);
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
    // Update is called once per frame
    void Update()
    {

    }
}
