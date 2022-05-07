using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class joueur : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Animator m_anim;
    private SpriteRenderer m_sprite;
    private float orientation_joueur;
    private bool m_flip_x = false;
    [SerializeField]
    private float m_vitesse_bullet = 2f;
    private float m_cooldown_degat = 2f;
    private float last_degat;
    private Text m_text_point;
    private int m_nb_point = 0;
    private bool m_estMort = false;

    private const int ORIENTATION_BAS = 0;
    private const int ORIENTATION_HAUT = 1;
    private const int ORIENTATION_GAUCHE = 2;
    private const int ORIENTATION_DROITE = 3;
    private GameObject m_barre_de_vie;


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
        m_text_point = GameObject.Find("nb_points").GetComponent<Text>();
        m_text_point.text = "Points : " + m_nb_point;
        //m_barre_de_vie = GameObject.Find("barre_de_vie");
        GameObject.Find("barre_de_vie").GetComponent<gestion_affichage_point_de_vie>().gererAffichagePointDeVie(point_de_vie);

        fusil_bas.SetActive(false);
        fusil_haut.SetActive(false);
        fusil_cote.SetActive(false);

        last_degat = Time.time;
    }

    private void Update()
    {
        if (!m_estMort)
        {
            float x = Input.GetAxis("Horizontal") * maxSpeed;
            float y = Input.GetAxis("Vertical") * maxSpeed;

            m_rb.velocity = new Vector2(x, y);

            float t_speed = Mathf.Abs(m_rb.velocity.x + m_rb.velocity.y);
            m_anim.SetFloat("speed", t_speed);

            m_sprite.flipX = m_flip_x;

            if (Input.GetMouseButtonDown(0)) gererToucheAttaque();
        }
    }

    public void ajouterPoint()
    {
        m_nb_point++;
        m_text_point.text = "Points : " + m_nb_point;
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
        if(m_cooldown_degat + last_degat < Time.time)
        {
            last_degat = Time.time;
            point_de_vie--;
            GameObject.Find("barre_de_vie").GetComponent<gestion_affichage_point_de_vie>().gererAffichagePointDeVie(point_de_vie);
            if (point_de_vie <= 0)
            {
                //Debug.Log("mort");
                m_estMort = true;
                m_rb.velocity = new Vector2(0, 0);
                m_anim.SetBool("estMort", true);
                StartCoroutine(mort());
            }
        }
    }

    IEnumerator mort()
    {
        yield return new WaitForSeconds(1.3f);
        Time.timeScale = 0f;
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
