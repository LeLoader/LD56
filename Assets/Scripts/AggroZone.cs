using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class AggroZone : MonoBehaviour
{
    CircleCollider2D collider;

    public float radius = 5;

    private void Awake()
    {
        if(collider == null)
        {
            collider = GetComponent<CircleCollider2D>();
            collider.isTrigger = true;
            collider.enabled = true;
            collider.radius = radius;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<Player>(out Player player))
            {
                GetComponentInParent<Enemy>().TriggerFight(player);
                collider.enabled = false;
            }
        }
    }
}
