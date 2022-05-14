using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public Text meileur_score,
        mode_normal,
        mode_hardcore,
        mode_sans_bonus,
        total_item,
        total_missile,
        total_vitesse,
        total_ricochet,
        total_gachette,
        vague_max;


    private void Start()
    {
        meileur_score.text = "Meilleur score : " + PlayerPrefs.GetInt("score");
        mode_normal.text = "Mode normal : " + PlayerPrefs.GetInt("mode_normal");
        mode_hardcore.text = "Mode hardcore : " + PlayerPrefs.GetInt("mode_hardcore");
        mode_sans_bonus.text = "Mode sans bonus : ";
        total_item.text = "Total bonus : " + (PlayerPrefs.GetInt("missile") + PlayerPrefs.GetInt("vitesse") + PlayerPrefs.GetInt("ricochet"));
        total_missile.text = "Total missile : " + PlayerPrefs.GetInt("missile");
        total_vitesse.text = "Total vitesse : " + PlayerPrefs.GetInt("vitesse");
        total_ricochet.text = "Total ricochet : " + PlayerPrefs.GetInt("ricochet");
        total_gachette.text = "Total gachette : " + PlayerPrefs.GetInt("gachette");
        vague_max.text = "Vague maximal atteinte : " + PlayerPrefs.GetInt("vague");
    }
}
