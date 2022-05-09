using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public Text meileur_score, vague_max;


    private void Start()
    {
        meileur_score.text = "Meilleur score : " + PlayerPrefs.GetInt("score");
        vague_max.text = "Meilleur vague : " + PlayerPrefs.GetInt("vague");
    }

}
