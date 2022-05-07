using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gestion_affichage_point_de_vie : MonoBehaviour
{
    public GameObject pointDevie1, pointDevie2, pointDevie3, pointDevie4, pointDevie5;

    public void gererAffichagePointDeVie(int a_point_de_vie)
    {
        pointDevie1.SetActive(false);
        pointDevie2.SetActive(false);
        pointDevie3.SetActive(false);
        pointDevie4.SetActive(false);
        pointDevie5.SetActive(false);

        switch (a_point_de_vie)
        {
            case 0:
                break;
            case 1:
                pointDevie1.SetActive(true);
                break;
            case 2:
                pointDevie1.SetActive(true);
                pointDevie2.SetActive(true);
                break;
            case 3:
                pointDevie1.SetActive(true);
                pointDevie2.SetActive(true);
                pointDevie3.SetActive(true);
                break;
            case 4:
                pointDevie1.SetActive(true);
                pointDevie2.SetActive(true);
                pointDevie3.SetActive(true);
                pointDevie4.SetActive(true);
                break;
            case 5:
                pointDevie1.SetActive(true);
                pointDevie2.SetActive(true);
                pointDevie3.SetActive(true);
                pointDevie4.SetActive(true);
                pointDevie5.SetActive(true);
                break;
        }
    }
}
