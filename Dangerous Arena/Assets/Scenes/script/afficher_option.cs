using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class afficher_option : MonoBehaviour
{
    public bool isPause = false;
    public GameObject menu_option;

    
    public void Pause_Click()
    {
        if (isPause)
        {
            this.isPause = false;
            menu_option.SetActive(false);
        }
        else
        {
            this.isPause = true;
            menu_option.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause_Click();
    }
}
