using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Collider2D))]
public class Damage : MonoBehaviour
{
    Collider2D collider2D;
    int damage;
    [SerializeField]
    bool isOneShot;
    [Tooltip("If true, damage only enemy, if false damage only player"), SerializeField]
    bool damageEnemy;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        collider2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Enemy") && damageEnemy) || (collision.CompareTag("Player") && !damageEnemy))
        {
            collision.TryGetComponent<Character>(out Character character);
            character.InflictDamage(damage);
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
