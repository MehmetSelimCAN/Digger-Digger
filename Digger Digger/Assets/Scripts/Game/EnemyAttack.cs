using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "fightPlayer") {
            Movement.Instance.TakeDamage(1);
            Destroy(gameObject);
        }

        if (collision.tag == "Block") {
            Destroy(gameObject);
        }
    }
}
