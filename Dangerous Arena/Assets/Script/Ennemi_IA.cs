using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi_IA : MonoBehaviour
{
    public AudioClip son_mort;
    public float vitesse = 5f;
    public GameObject heart;
    public GameObject mutlidirectionnel;
    public GameObject speed;
    public GameObject coin;
    public GameObject bullets;
    public GameObject gachette;
    public int number_bonus;
    public int nombre_degats = 1;
    public int nombre_point = 1;

    private const int ORIENTATION_BAS = 0;
    private const int ORIENTATION_HAUT = 1;
    private const int ORIENTATION_GAUCHE = 2;
    private const int ORIENTATION_DROITE = 3;

    [SerializeField]
    private Transform player;
    private Rigidbody2D m_rb;
    private Vector2 movement;
    private Animator m_anim;
    private SpriteRenderer m_sprite;
    private float orientation_joueur;
    private bool m_flip_x = false;
    [SerializeField]
    private int point_de_vie = 2;
    private bool m_estMort = false;
    private AudioSource m_audio;

    private void Awake()
    {
        m_audio = GetComponent<AudioSource>();
        player = GameObject.Find("perso").GetComponent<Transform>();
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_sprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!m_estMort)
        {
            if (collision.gameObject.tag == "Joueur")
            {
                collision.gameObject.GetComponent<joueur>()?.subirDegat(nombre_degats);
                GameObject.Find("perso").GetComponent<joueur_histoire>()?.subirDegat(nombre_degats);
            }
        }
    }

    public void enleverPointDeVie()
    {
        point_de_vie--;
        if (point_de_vie <= 0 && !m_estMort)
        {
            m_audio.PlayOneShot(son_mort);
            m_estMort = true;
            m_anim.SetBool("mort", true);
            GameObject.Find("perso").GetComponent<joueur>()?.ajouterPoint(nombre_point);
            GameObject.Find("perso").GetComponent<joueur_histoire>()?.ajouterPoint(nombre_point);
            StartCoroutine(mort());
        }
    }

    private void genererBonus()
    {
        switch (number_bonus)
        {
            case 1:
                Instantiate(heart, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(bullets, transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(coin, transform.position, Quaternion.identity);
                break;
            case 4:
                Instantiate(mutlidirectionnel, transform.position, Quaternion.identity);
                break;
            case 5:
                Instantiate(speed, transform.position, Quaternion.identity);
                break;
            case 6:
                Instantiate(gachette, transform.position, Quaternion.identity);
                break;
        }
    }

    public void modeHardcore()
    {
        vitesse++;
        point_de_vie++;
        nombre_degats++;
        nombre_point = nombre_point * 2;
    }

    public void modeDivin()
    {
        point_de_vie = point_de_vie * 2;
        nombre_degats = nombre_degats * 2;
        nombre_point = nombre_point * 3;
    }

    IEnumerator mort()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
        yield return new WaitForSeconds(1.3f);
        genererBonus();
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (!m_estMort)
        {
            Vector3 diretion = player.position - transform.position;
            //Debug.Log(diretion);
            diretion.Normalize();
            m_sprite.flipX = m_flip_x;
            movement = diretion;
            moveCharacter(movement);
            orientation_joueur = (float)trouver_position_joueur();
            m_anim.SetFloat("position", orientation_joueur);
        }
        
    }

    private void moveCharacter(Vector2 direction)
    {
        m_rb.MovePosition((Vector2)transform.position + (direction * vitesse * Time.deltaTime));
    }

    private int trouver_position_joueur()
    {
        int t_orientation = 1;
        Vector3 worldPosition = player.position;
        Vector3 distance_personnage = worldPosition - m_rb.transform.position;
        //Debug.Log(distance_personnage);

        if (distance_personnage.x > 0)
        {
            //droite
            if (distance_personnage.y > 0)
            {
                //haut 
                if (distance_personnage.x - distance_personnage.y < 0) t_orientation = ORIENTATION_HAUT;
                else
                {
                    t_orientation = ORIENTATION_DROITE;
                    m_flip_x = true;
                }
            }
            else
            {
                //bas
                if (distance_personnage.x - -distance_personnage.y < 0) t_orientation = ORIENTATION_BAS;
                else
                {
                    t_orientation = ORIENTATION_DROITE;
                    m_flip_x = true;
                }
            }
        }
        else
        {
            //gauche
            if (distance_personnage.y > 0)
            {
                //haut 
                if (-distance_personnage.x - distance_personnage.y < 0) t_orientation = ORIENTATION_HAUT;
                else
                {
                    t_orientation = ORIENTATION_GAUCHE;
                    m_flip_x = false;
                }
            }
            else
            {
                //bas
                if (-distance_personnage.x - -distance_personnage.y < 0) t_orientation = ORIENTATION_BAS;
                else
                {
                    t_orientation = ORIENTATION_GAUCHE;
                    m_flip_x = false;
                }
            }
        }
        return t_orientation;
    }
}
