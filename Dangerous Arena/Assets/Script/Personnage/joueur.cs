using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joueur : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Animator m_anim;
    private SpriteRenderer m_sprite;
    private float orientation_joueur;
    private bool m_flip_x = false;
    [SerializeField]
    private float m_vitesse_bullet = 2f;

    private const int ORIENTATION_BAS = 0;
    private const int ORIENTATION_HAUT = 1;
    private const int ORIENTATION_GAUCHE = 2;
    private const int ORIENTATION_DROITE = 3;

    public GameObject fusil_bas;
    public GameObject fusil_haut;
    public GameObject fusil_cote;
    public GameObject bullet_horizontal;
    public GameObject bullet_vertical;

    public float maxSpeed = 3f;
    public int point_de_vie = 5;


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_sprite = GetComponent<SpriteRenderer>();

        fusil_bas.SetActive(false);
        fusil_haut.SetActive(false);
        fusil_cote.SetActive(false);
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal") * maxSpeed;
        float y = Input.GetAxis("Vertical") * maxSpeed;

        m_rb.velocity = new Vector2(x, y);

        float t_speed = Mathf.Abs(m_rb.velocity.x + m_rb.velocity.y);
        m_anim.SetFloat("speed", t_speed);

        m_sprite.flipX = m_flip_x;

        if (Input.GetMouseButtonDown(0)) gererToucheAttaque();
    }

    private void gererToucheAttaque()
    {
        //Debug.Log("attaque");
        switch (orientation_joueur)
        {
            case ORIENTATION_BAS :
                fusil_bas.SetActive(true);
                GameObject bullet_bas = Instantiate(bullet_vertical, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
                bullet_bas.GetComponent<Bullet>().addVelocity(new Vector3(0, -1 * m_vitesse_bullet, 0));
                break;
            case ORIENTATION_HAUT:
                fusil_haut.SetActive(true);
                GameObject bullet_haut = Instantiate(bullet_vertical, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
                bullet_haut.GetComponent<Bullet>().addVelocity(new Vector3(0, 1 * m_vitesse_bullet, 0));
                break;
            case ORIENTATION_DROITE :
                fusil_cote.transform.localScale = new Vector3(-1, 1, 1);
                fusil_cote.SetActive(true);
                GameObject bullet_droite = Instantiate(bullet_horizontal, new Vector3(transform.position.x + 1f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                bullet_droite.GetComponent<Bullet>().addVelocity(new Vector3(1 * m_vitesse_bullet, 0, 0));
                break;
            case ORIENTATION_GAUCHE:
                fusil_cote.SetActive(true);
                GameObject bullet_gauche = Instantiate(bullet_horizontal, new Vector3(transform.position.x - 1f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                bullet_gauche.GetComponent<Bullet>().addVelocity(new Vector3(-1 * m_vitesse_bullet, 0, 0));
                break;
        }

        StartCoroutine(resetAffichage());
    }

    public void subirDegat()
    {
        point_de_vie--;
        if (point_de_vie <= 0)
        {
            Debug.Log("mort");
        }
    }

    IEnumerator resetAffichage()
    {
        yield return new WaitForSeconds(.2f);
        fusil_bas.SetActive(false);
        fusil_haut.SetActive(false);
        fusil_cote.transform.localScale = new Vector3(1, 1, 1);
        fusil_cote.SetActive(false);
    }

    private void FixedUpdate()
    {
        orientation_joueur = (float)trouver_position_joueur();
        m_anim.SetFloat("position", orientation_joueur);
    }

    private int trouver_position_joueur()
    {
        int t_orientation = 1;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 distance_personnage = worldPosition - m_rb.transform.position;
        //Debug.Log(distance_personnage);

        if (distance_personnage.x > 0)
        {
            //droite
            if(distance_personnage.y > 0)
            {
                //haut 
                if (distance_personnage.x - distance_personnage.y < 0) t_orientation = ORIENTATION_HAUT;
                else
                {
                    t_orientation = ORIENTATION_DROITE;
                    m_flip_x = true;
                }
                    
            } else
            {
                //bas
                if (distance_personnage.x - -distance_personnage.y < 0) t_orientation = ORIENTATION_BAS;
                else
                {
                    t_orientation = ORIENTATION_DROITE;
                    m_flip_x = true;
                }
            }
        } else
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
