using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScript : MonoBehaviour
{
    public TextMeshProUGUI wavesTxt;


    public void SceneGoto(string scene)
    {
        Globals.instance.GoToScene(scene);
        Destroy(Globals.instance);
    }

    public void ExitGame()
    {
        Globals.instance.QUITGAME();
    }


    private void Start()
    {
        wavesTxt.text = "You made it to wave: " + Globals.instance.wave.ToString();
    }
}
