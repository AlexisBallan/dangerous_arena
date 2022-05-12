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
    private AudioSource m_AudioSource;
    private int m_nouvelle_vague = 1;
    private int m_nombre_missile = 1;
    private int m_multidirection = 1;
    private bool m_mode_hardcore = false;


    public GameObject fusil_bas;
    public GameObject fusil_haut;
    public GameObject fusil_cote;
    public GameObject bullet_horizontal;
    public GameObject bullet_vertical;
    public GameObject menu_mort;
    public AudioClip tire;
    public AudioClip take_damage;
    public AudioClip death_sound;
    public AudioClip mode_hardcore;
    public GameObject modeHardcoreCanvas;

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
        m_AudioSource = GetComponent<AudioSource>();

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
            if(Time.timeScale != 0f)
                if (Input.GetMouseButtonDown(0)) gererToucheAttaque();
        }
    }

    public void ajouterPoint(int point)
    {
        m_nb_point += point;
        m_text_point.text = "Points : " + m_nb_point;
        if(m_nb_point >= 1000 && !m_mode_hardcore)
        {
            m_mode_hardcore = true;
            modeHardcoreCanvas.SetActive(true);
            GameObject.Find("Spawner").GetComponent<Spawner>().m_mode_hardcore = true;
            GameObject.Find("effet_sonore").GetComponent<AudioSource>().PlayOneShot(mode_hardcore);
        }
    }

    private void gererToucheAttaque()
    {
        //Debug.Log("attaque");
        for (int i = 1; i <= m_nombre_missile; i++){
            switch (orientation_joueur)
            {
                case ORIENTATION_BAS:
                    fusil_bas.SetActive(true);
                    GameObject bullet_bas = Instantiate(bullet_vertical, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
                    bullet_bas.GetComponent<Bullet>().addVelocity(new Vector3(0, -1 * m_vitesse_bullet, 0));
                    
                    break;
                case ORIENTATION_HAUT:
                    fusil_haut.SetActive(true);
                    GameObject bullet_haut = Instantiate(bullet_vertical, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
                    bullet_haut.GetComponent<Bullet>().addVelocity(new Vector3(0, 1 * m_vitesse_bullet, 0));
                    break;
                case ORIENTATION_DROITE:
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
        }

        switch (m_multidirection)
        {
            case 2:
                switch (orientation_joueur)
                {
                    case ORIENTATION_BAS:
                        GameObject bullet_haut = Instantiate(bullet_vertical, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
                        bullet_haut.GetComponent<Bullet>().addVelocity(new Vector3(0, 1 * m_vitesse_bullet, 0));
                        break;
                    case ORIENTATION_HAUT:
                        GameObject bullet_bas = Instantiate(bullet_vertical, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
                        bullet_bas.GetComponent<Bullet>().addVelocity(new Vector3(0, -1 * m_vitesse_bullet, 0));
                        break;
                    case ORIENTATION_DROITE:
                        GameObject bullet_gauche = Instantiate(bullet_horizontal, new Vector3(transform.position.x - 1f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        bullet_gauche.GetComponent<Bullet>().addVelocity(new Vector3(-1 * m_vitesse_bullet, 0, 0));
                        break;
                    case ORIENTATION_GAUCHE:
                        GameObject bullet_droite = Instantiate(bullet_horizontal, new Vector3(transform.position.x + 1f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        bullet_droite.GetComponent<Bullet>().addVelocity(new Vector3(1 * m_vitesse_bullet, 0, 0));
                        break;
                }
                break;
            case 3:
                switch (orientation_joueur)
                {
                    case ORIENTATION_BAS:
                        GameObject bullet_haut = Instantiate(bullet_vertical, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
                        bullet_haut.GetComponent<Bullet>().addVelocity(new Vector3(0, 1 * m_vitesse_bullet, 0));
                        GameObject bullet_haut_gauche = Instantiate(bullet_horizontal, new Vector3(transform.position.x - 1f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        bullet_haut_gauche.GetComponent<Bullet>().addVelocity(new Vector3(-1 * m_vitesse_bullet, 0, 0));
                        break;
                    case ORIENTATION_HAUT:
                        GameObject bullet_bas = Instantiate(bullet_vertical, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
                        bullet_bas.GetComponent<Bullet>().addVelocity(new Vector3(0, -1 * m_vitesse_bullet, 0));
                        GameObject bullet_bas_droite = Instantiate(bullet_horizontal, new Vector3(transform.position.x + 1f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        bullet_bas_droite.GetComponent<Bullet>().addVelocity(new Vector3(1 * m_vitesse_bullet, 0, 0));
                        break;
                    case ORIENTATION_DROITE:
                        GameObject bullet_gauche = Instantiate(bullet_horizontal, new Vector3(transform.position.x - 1f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        bullet_gauche.GetComponent<Bullet>().addVelocity(new Vector3(-1 * m_vitesse_bullet, 0, 0));
                        GameObject bullet_gauche_haut = Instantiate(bullet_vertical, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
                        bullet_gauche_haut.GetComponent<Bullet>().addVelocity(new Vector3(0, 1 * m_vitesse_bullet, 0));
                        break;
                    case ORIENTATION_GAUCHE:
                        GameObject bullet_droite = Instantiate(bullet_horizontal, new Vector3(transform.position.x + 1f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        bullet_droite.GetComponent<Bullet>().addVelocity(new Vector3(1 * m_vitesse_bullet, 0, 0));
                        GameObject bullet_dorite_bas = Instantiate(bullet_vertical, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
                        bullet_dorite_bas.GetComponent<Bullet>().addVelocity(new Vector3(0, -1 * m_vitesse_bullet, 0));
                        break;
                }
                break;
            case 4:
                switch (orientation_joueur)
                {
                    case ORIENTATION_BAS:
                        GameObject bullet_haut = Instantiate(bullet_vertical, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
                        bullet_haut.GetComponent<Bullet>().addVelocity(new Vector3(0, 1 * m_vitesse_bullet, 0));
                        GameObject bullet_haut_gauche = Instantiate(bullet_horizontal, new Vector3(transform.position.x - 1f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        bullet_haut_gauche.GetComponent<Bullet>().addVelocity(new Vector3(-1 * m_vitesse_bullet, 0, 0));
                        GameObject bullet_haut_droite = Instantiate(bullet_horizontal, new Vector3(transform.position.x + 1f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        bullet_haut_droite.GetComponent<Bullet>().addVelocity(new Vector3(1 * m_vitesse_bullet, 0, 0));
                        break;
                    case ORIENTATION_HAUT:
                        GameObject bullet_bas = Instantiate(bullet_vertical, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
                        bullet_bas.GetComponent<Bullet>().addVelocity(new Vector3(0, -1 * m_vitesse_bullet, 0));
                        GameObject bullet_bas_droite = Instantiate(bullet_horizontal, new Vector3(transform.position.x + 1f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        bullet_bas_droite.GetComponent<Bullet>().addVelocity(new Vector3(1 * m_vitesse_bullet, 0, 0));
                        GameObject bullet_bas_gauche = Instantiate(bullet_horizontal, new Vector3(transform.position.x - 1f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        bullet_bas_gauche.GetComponent<Bullet>().addVelocity(new Vector3(-1 * m_vitesse_bullet, 0, 0));
                        break;
                    case ORIENTATION_DROITE:
                        GameObject bullet_gauche = Instantiate(bullet_horizontal, new Vector3(transform.position.x - 1f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        bullet_gauche.GetComponent<Bullet>().addVelocity(new Vector3(-1 * m_vitesse_bullet, 0, 0));
                        GameObject bullet_gauche_haut = Instantiate(bullet_vertical, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
                        bullet_gauche_haut.GetComponent<Bullet>().addVelocity(new Vector3(0, 1 * m_vitesse_bullet, 0));
                        GameObject bullet_droite_bas = Instantiate(bullet_vertical, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
                        bullet_droite_bas.GetComponent<Bullet>().addVelocity(new Vector3(0, -1 * m_vitesse_bullet, 0));
                        break;
                    case ORIENTATION_GAUCHE:
                        GameObject bullet_droite = Instantiate(bullet_horizontal, new Vector3(transform.position.x + 1f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        bullet_droite.GetComponent<Bullet>().addVelocity(new Vector3(1 * m_vitesse_bullet, 0, 0));
                        GameObject bullet_dorite_bas = Instantiate(bullet_vertical, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
                        bullet_dorite_bas.GetComponent<Bullet>().addVelocity(new Vector3(0, -1 * m_vitesse_bullet, 0));
                        GameObject bullet_droite_haut = Instantiate(bullet_vertical, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
                        bullet_droite_haut.GetComponent<Bullet>().addVelocity(new Vector3(0, 1 * m_vitesse_bullet, 0));
                        break;
                }
                break;
        }
        m_AudioSource.PlayOneShot(tire);

        StartCoroutine(resetAffichage());
    }

    public void annocerNouvelleVague()
    {
        m_nouvelle_vague++;
        GameObject.Find("vague").GetComponent<Text>().text = "Vague : " + m_nouvelle_vague;
    }

    public void subirDegat(int nombre_degat)
    {
        if (m_cooldown_degat + last_degat < Time.time)
        {

            last_degat = Time.time;
            point_de_vie -= nombre_degat;
            GameObject.Find("barre_de_vie").GetComponent<gestion_affichage_point_de_vie>().gererAffichagePointDeVie(point_de_vie);
            if (point_de_vie <= 0)
            {
                m_AudioSource.PlayOneShot(death_sound);
                //Debug.Log("mort");
                m_estMort = true;
                m_rb.velocity = new Vector2(0, 0);
                m_anim.SetBool("estMort", true);
                StartCoroutine(mort());
            } else m_AudioSource.PlayOneShot(take_damage);
        }
    }

    IEnumerator mort()
    {
        yield return new WaitForSeconds(1.3f);
        menu_mort.SetActive(true);
        GameObject.Find("nb_points_ecran_mort").GetComponent<Text>().text = "Points : " + m_nb_point;
        Time.timeScale = 0f;
        sauvegarderLesDonnees();
    }

    private void sauvegarderLesDonnees()
    {
        int t_scoreLePlusElever = PlayerPrefs.GetInt("score");

        if (m_nb_point > t_scoreLePlusElever)
            PlayerPrefs.SetInt("score", m_nb_point);

        int t_vagueLaPlusElever = PlayerPrefs.GetInt("vague");

        if (m_nouvelle_vague > t_vagueLaPlusElever)
            PlayerPrefs.SetInt("vague", m_nouvelle_vague);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.gameObject.tag)
        {
            case "heal":
                if (point_de_vie == 5)
                    ajouterPoint(5);
                else
                {
                    point_de_vie++;
                    GameObject.Find("barre_de_vie").GetComponent<gestion_affichage_point_de_vie>().gererAffichagePointDeVie(point_de_vie);
                }
                Destroy(collision.gameObject);
                break;
            case "speed":
                if (maxSpeed == 7)
                    ajouterPoint(5);
                else
                    maxSpeed++;
                GameObject.Find("barre_speed").GetComponent<affichage_bar_speed>().gererAffichage((int)maxSpeed - 4);
                Destroy(collision.gameObject);
                break;
            case "missile":
                if (m_nombre_missile == 4)
                    ajouterPoint(5);
                else
                    m_nombre_missile++;
                GameObject.Find("barre_missile").GetComponent<affichage_bar_missile>().gererAffichage(m_nombre_missile - 1);
                Destroy(collision.gameObject);
                break;
            case "piece":
                ajouterPoint(15);
                Destroy(collision.gameObject);
                break;
            case "multidirectionnel":
                if (m_multidirection == 4)
                    ajouterPoint(5);
                else
                    m_multidirection++;
                GameObject.Find("barre_multidirectionnel").GetComponent<affichage_bar_multidirectionnel>().gererAffichage(m_multidirection - 1);
                Destroy(collision.gameObject);
                break;
        }
        
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
