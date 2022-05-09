using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject monstre;
    public float interval_spawn = 1f;
    public float interval_next_level = 30f;

    private System.Random random = new System.Random();

    private float last_time, last_level;
    [SerializeField]
    private int m_nombre_spawn = 2;

    private void Start()
    {
        last_level = last_time = Time.time;
    }

    private void FixedUpdate()
    {
        if(last_time + interval_spawn < Time.time)
        {
            last_time = Time.time;

            for (int i = 0; i < m_nombre_spawn; i++)
            {
                GererSpawn(genererPosition());
            }
        }

        if (last_level + interval_next_level < Time.time)
        {
            last_level = Time.time;
            m_nombre_spawn++;
            GameObject.Find("perso").GetComponent<joueur>().annocerNouvelleVague();
            //Debug.Log("NOUVELLE VAGUE !");
        }
    }

    private Vector3 genererPosition()
    {
        
        int numSpawn = random.Next(1, 5);

        switch (numSpawn)
        {
            case 1:
                return new Vector3(random.Next(-9, 10), -7.5f, 0);
            case 2:
                return new Vector3(random.Next(-9, 10), 7, 0);
            case 3:
                return new Vector3(-9.2f, random.Next(-7, 7), 0);
            case 4:
                return new Vector3(9, random.Next(-7, 7), 0);
        }
        Debug.Log("error");
        return Vector3.zero;
    }

    private void GererSpawn(Vector3 position_spawn)
    {
        //Debug.Log("nouveau spawn" + position_spawn);
        Instantiate(monstre, position_spawn, Quaternion.identity);
    }
}
