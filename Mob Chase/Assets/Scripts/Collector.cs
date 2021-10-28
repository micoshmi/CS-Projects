using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //enemies going out of bounderies - destroying the object("enemy" tagged)
        if(collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
        //the player going out of bounderies - destroying the object("player" tagged)
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}
