using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Hit();
        }
    }
}
