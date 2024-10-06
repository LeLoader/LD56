using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamagePlayer : MonoBehaviour
{
    Collider2D collider2D;
    int damage;
    [SerializeField]
    bool isOneShot;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        collider2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.InflictDamage(damage);
            if (isOneShot)
            {
                Destroy(this);
            }
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
