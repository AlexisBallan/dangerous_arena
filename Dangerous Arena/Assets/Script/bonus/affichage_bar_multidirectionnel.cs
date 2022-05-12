using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichage_bar_multidirectionnel : MonoBehaviour
{
    public GameObject mutli1, mutli2, mutli3;


    public void gererAffichage(int number_missile)
    {
        if (number_missile > 3) number_missile = 3;

        mutli1.SetActive(false);
        mutli2.SetActive(false);
        mutli3.SetActive(false);

        switch (number_missile)
        {
            case 1:
                mutli1.SetActive(true);
                break;
            case 2:
                mutli1.SetActive(true);
                mutli2.SetActive(true);
                break;
            case 3:
                mutli1.SetActive(true);
                mutli2.SetActive(true);
                mutli3.SetActive(true);
                break;
        }
    }
}
