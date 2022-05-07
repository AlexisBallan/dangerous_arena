using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi_IA : MonoBehaviour
{
   
    public float vitesse = 5f;

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
    private int point_de_vie = 2;
    private bool m_estMort = false;


    private void Start()
    {
        player = GameObject.Find("perso").GetComponent<Transform>();
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_sprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!m_estMort)
        {
            if (collision.gameObject.tag == ("Joueur"))
            {
                
                collision.gameObject.GetComponent<joueur>().subirDegat();
            }
        }
    }

    public void enleverPointDeVie()
    {
        point_de_vie--;
        if (point_de_vie == 0)
        {
            m_estMort = true;
            m_anim.SetBool("mort", true);
            GameObject.Find("perso").GetComponent<joueur>().ajouterPoint();
            StartCoroutine(mort());
        }
    }

    

    IEnumerator mort()
    {
        yield return new WaitForSeconds(1.5f);
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
