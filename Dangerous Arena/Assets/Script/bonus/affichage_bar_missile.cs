using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichage_bar_missile : MonoBehaviour
{
    public GameObject missile1, missile2, missile3;


    public void gererAffichage(int number_missile)
    {
        if (number_missile > 3) number_missile = 3;

        missile1.SetActive(false);
        missile2.SetActive(false);
        missile3.SetActive(false);

        switch (number_missile)
        {
            case 1:
                missile1.SetActive(true);
                break;
            case 2:
                missile1.SetActive(true);
                missile2.SetActive(true);
                break;
            case 3:
                missile1.SetActive(true);
                missile2.SetActive(true);
                missile3.SetActive(true);
                break;
        }
    }
}
