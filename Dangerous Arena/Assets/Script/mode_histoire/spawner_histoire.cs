using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner_histoire : MonoBehaviour
{
    public GameObject monstre;
    public GameObject Sprinter;
    public GameObject Mastodonte;
    public float interval_spawn = 1f;
    public float interval_next_level = 30f;
    public bool m_mode_hardcore = false;
    public bool m_mode_divin = false;
    public int MIN_LUCK = 1;
    public int MAX_LUCK = 56;

    private System.Random random = new System.Random();
    private int nombre_vague = 1;
    private float last_time, last_level;
    [SerializeField]
    private int m_nombre_spawn = 2;
    private Vector3 player;
    private float différenceX;

    private void Start()
    {
        last_level = last_time = Time.time;
    }

    public void modeHardcore()
    {
        m_mode_hardcore = true;
        interval_spawn = 2f;
    }

    public void modeDivin()
    {
        m_mode_divin = true;
        interval_spawn = 1f;
        m_nombre_spawn = 1;
        interval_next_level = 5f;
    }

    private void Awake()
    {
        player = GameObject.Find("perso").GetComponent<Transform>().transform.position;

        différenceX = transform.position.x - player.x;
    }

    private void Update()
    {
        player = GameObject.Find("perso").GetComponent<Transform>().transform.position;
        transform.position = new Vector3(player.x + différenceX, transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (last_time + interval_spawn < Time.time)
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
            if (m_nombre_spawn < 10)
                m_nombre_spawn++;
            nombre_vague++;
            GameObject.Find("perso").GetComponent<joueur>()?.annocerNouvelleVague();
            GameObject.Find("perso").GetComponent<joueur_histoire>()?.annocerNouvelleVague();
            //Debug.Log("NOUVELLE VAGUE !");
        }
    }

    private Vector3 genererPosition()
    {
        return new Vector3(random.Next((int)transform.position.x, (int)transform.position.x + 10), random.Next((int)transform.position.y - 16, (int)transform.position.y), 0);
    }

    private int genererNombre(int min, int max)
    {
        return random.Next(min, max);
    }
    private GameObject genererMonstre()
    {
        int t_nombreSpawn = 1;
        GameObject t_monstre = monstre;

        if (nombre_vague > 4)
        {
            t_nombreSpawn = genererNombre(1, 6);

            if (t_nombreSpawn == 2)
                t_monstre = Sprinter;
            else if (t_nombreSpawn == 3)
                t_monstre = Mastodonte;

        }
        else if (nombre_vague > 2)
        {
            t_nombreSpawn = genererNombre(1, 4);
            if (t_nombreSpawn == 2)
                t_monstre = Sprinter;
        }
        return t_monstre;
    }

    private void GererSpawn(Vector3 position_spawn)
    {
        //Debug.Log("nouveau spawn" + position_spawn);
        GameObject Orc = Instantiate(genererMonstre(), position_spawn, Quaternion.identity);
        Orc.GetComponent<Ennemi_IA>().number_bonus = genererNombre(MIN_LUCK, MAX_LUCK);
        if (m_mode_hardcore)
            Orc.GetComponent<Ennemi_IA>().modeHardcore();
        if (m_mode_divin)
            Orc.GetComponent<Ennemi_IA>().modeDivin();
    }
}
