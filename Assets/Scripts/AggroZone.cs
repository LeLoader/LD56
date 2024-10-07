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
        Debug.Log("TriggerEnter");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("It's a player");
            if (collision.TryGetComponent<Player>(out Player player))
            {
                Debug.Log("and he has player comp");
                GetComponentInParent<Enemy>().TriggerFight(player);
                collider.enabled = false;
            }
            else
            {
                Debug.Log("and he doesn't have player comp");
            }
        }
        else
        {
            Debug.Log("It's not a player");
        }
    }
}
