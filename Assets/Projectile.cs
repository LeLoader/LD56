using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    Collider2D collider;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [Header("General")]
    int damage;
    [SerializeField]
    float speed = 1;
    [SerializeField]
    Sprite sprite;
    [SerializeField]
    float destroyAfter = 2.0f;

    private void Start()
    {
        Destroy(gameObject, destroyAfter);

        collider = GetComponent<Collider2D>();
        collider.isTrigger = true;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * transform.up;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            player.InflictDamage(damage);
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
