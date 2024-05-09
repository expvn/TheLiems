using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class die : MonoBehaviour
{
    public Player player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            player.Hit();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            player.Hit();

        }
    }
}
