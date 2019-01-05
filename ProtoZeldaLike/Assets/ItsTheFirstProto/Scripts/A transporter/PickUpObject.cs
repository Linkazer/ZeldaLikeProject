using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour {

    public Objet item;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interaction")
        {
            Inventaire.instance.Add(item);
            Destroy(gameObject);
        }
    }
}
