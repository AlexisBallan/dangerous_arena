using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_mouvement : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector3(GameObject.Find("perso").GetComponent<Transform>().transform.position.x, transform.position.y, transform.position.z);
    }
}
