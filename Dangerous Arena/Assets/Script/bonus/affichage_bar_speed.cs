using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichage_bar_speed : MonoBehaviour
{
    public GameObject speed1, speed2, speed3;


    public void gererAffichage(int number_missile)
    {
        if (number_missile > 3) number_missile = 3;

        speed1.SetActive(false);
        speed2.SetActive(false);
        speed3.SetActive(false);

        switch (number_missile)
        {
            case 1:
                speed1.SetActive(true);
                break;
            case 2:
                speed1.SetActive(true);
                speed2.SetActive(true);
                break;
            case 3:
                speed1.SetActive(true);
                speed2.SetActive(true);
                speed3.SetActive(true);
                break;
        }
    }
}
