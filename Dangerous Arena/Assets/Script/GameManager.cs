using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int gold = 1000;


    private void Awake()
    {
        Instance = this;
    }
}
