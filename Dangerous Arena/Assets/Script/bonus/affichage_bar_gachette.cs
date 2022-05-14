using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichage_bar_gachette : MonoBehaviour
{
    public GameObject gachette1, gachette2, gachette3, gachette4, gachette5;


    public void gererAffichage(int number_missile)
    {
        if (number_missile > 5) number_missile = 5;

        gachette1.SetActive(false);
        gachette2.SetActive(false);
        gachette3.SetActive(false);
        gachette4.SetActive(false);
        gachette5.SetActive(false);

        switch (number_missile)
        {
            case 1:
                gachette1.SetActive(true);
                break;
            case 2:
                gachette1.SetActive(true);
                gachette2.SetActive(true);
                break;
            case 3:
                gachette1.SetActive(true);
                gachette2.SetActive(true);
                gachette3.SetActive(true);
                break;
            case 4:
                gachette1.SetActive(true);
                gachette2.SetActive(true);
                gachette3.SetActive(true);
                gachette4.SetActive(true);
                break;
            case 5:
                gachette1.SetActive(true);
                gachette2.SetActive(true);
                gachette3.SetActive(true);
                gachette4.SetActive(true);
                gachette5.SetActive(true);
                break;
        }
    }
}
