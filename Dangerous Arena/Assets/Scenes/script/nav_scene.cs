using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nav_scene : MonoBehaviour
{
    private void Awake()
    {

        if (PlayerPrefs.GetInt("lancement") == 0)
        {
            PlayerPrefs.SetString("haut", "w");
            PlayerPrefs.SetString("bas", "s");
            PlayerPrefs.SetString("droite", "d");
            PlayerPrefs.SetString("gauche", "a");
            PlayerPrefs.SetInt("lancement", 1);
            PlayerPrefs.SetFloat("volume", 0);
            Debug.Log("premier lancement");
        }
    }

    public void mode_infini()
    {
        SceneManager.LoadScene("mode_normal");
        PlayerPrefs.SetInt("mode", 0);
    }

    public void mode_sans_bonus()
    {
        SceneManager.LoadScene("mode_normal");
        PlayerPrefs.SetInt("mode", 2);
    }

    public void mode_hard()
    {
        SceneManager.LoadScene("mode_normal");
        PlayerPrefs.SetInt("mode", 1);
    }

    public void mode_de_jeu()
    {
        SceneManager.LoadScene("choix_mode_jeu");
    }

    public void bouton_score()
    {
        SceneManager.LoadScene("score");
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
