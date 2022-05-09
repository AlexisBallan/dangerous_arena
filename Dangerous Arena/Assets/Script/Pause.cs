using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool isPause = false;
    public GameObject m_MenuPrincipal;
    public void Pause_Click()
    {
        if (isPause)
        {
            Time.timeScale = 1f;
            this.isPause = false;
            m_MenuPrincipal.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            this.isPause = true;
            m_MenuPrincipal.SetActive(true);
        }
    }

    public void retour_menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ecran_principal");
    }

    public void reload()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("game");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause_Click();
        }
    }
}
