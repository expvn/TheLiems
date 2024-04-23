using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaGround : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            HeartController.instance.TruHP(3);
            player.Death();
        }
    }
}
