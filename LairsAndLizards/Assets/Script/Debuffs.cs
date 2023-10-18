using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuffs : MonoBehaviour
{
    [SerializeField]List<int> Priority;
    [SerializeField]testchar[] Peopleindaroom;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        Peopleindaroom = GameObject.FindObjectsOfType<testchar>();
        if(Peopleindaroom != null)
        {
            foreach(var Stat in Peopleindaroom)
            {
                Stat.TryGetComponent<testchar>(out testchar DexStat);
                Priority.Add(DexStat.dex);

          

            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
