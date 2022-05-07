using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nav_scene : MonoBehaviour
{
    public void mode_infini()
    {
        SceneManager.LoadScene("game");
    }

    public void bouton_quitter()
    {
        Application.Quit();
    }

    public void mode_tutoriel()
    {
        SceneManager.LoadScene("tutoriel");
    }

    public void bouton_retour()
    {
        SceneManager.LoadScene("ecran_principal");
    }
}
