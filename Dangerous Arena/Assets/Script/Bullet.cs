using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Vector3 vitesse;


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    public void addVelocity(Vector3 velocity)
    {
        vitesse = velocity;
    }

    private void FixedUpdate()
    {
        m_rb.velocity = vitesse;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Joueur" && collision.gameObject.tag != "Bullet")
        {
            //Debug.Log("toucher quelquechose !");
            if(collision.gameObject.tag == "Ennemi")
            {
                collision.gameObject.GetComponent<Ennemi_IA>().enleverPointDeVie();
            }
            Destroy(this.gameObject);
        }
    }

}
