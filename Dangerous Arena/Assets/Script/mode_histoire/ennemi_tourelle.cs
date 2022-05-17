using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemi_tourelle : MonoBehaviour
{
    public GameObject bullet;
    public float vitesse = 5f;
    public float temps_entre_tire = 1f;

    private Transform player;
    private Vector2 movement;
    private Animator m_anim;
    private SpriteRenderer m_sprite;
    private float orientation_joueur;
    private bool m_flip_x = false;
    [SerializeField]
    private int point_de_vie = 2;
    private bool m_estMort = false;
    private AudioSource m_audio;
    private float dernier_tire;

    private void Start()
    {
        player = GameObject.Find("perso").GetComponent<Transform>();
        m_anim = GetComponent<Animator>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_audio = GetComponent<AudioSource>();
        dernier_tire = Time.time;
}

    private void Update()
    {
        if(dernier_tire + temps_entre_tire < Time.time)
        {
            dernier_tire = Time.time;
            Vector3 diretion = player.position - transform.position;
            //Debug.Log(diretion);
            diretion.Normalize();
            m_sprite.flipX = m_flip_x;
            movement = diretion;
            generateBuller(movement);
            //Debug.Log(player.position);
        }
    }

    private void generateBuller(Vector2 direction)
    {
        GameObject bullet_tire = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        bullet_tire.GetComponent<Bullet>().addVelocity(new Vector3(direction.x * vitesse, (direction.y + 0.1f) * vitesse));
        //permet d'ajuster la rotation des bullets
        Vector3 rotationNeed = Quaternion.FromToRotation(Vector3.forward, direction).eulerAngles;
        bullet_tire.GetComponent<Transform>().transform.rotation = Quaternion.Euler(0, 0, -rotationNeed.z);
        bullet_tire.GetComponent<BoxCollider2D>().isTrigger = true;
    }
}
