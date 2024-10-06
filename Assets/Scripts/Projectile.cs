using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    public DamagePlayer damagePlayer;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [Header("General")]
    [SerializeField]
    float speed = 1;
    [SerializeField]
    Sprite sprite;
    [SerializeField]
    float destroyAfter = 2.0f;

    private void Start()
    {
        Destroy(gameObject, destroyAfter);

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        damagePlayer = GetComponent<DamagePlayer>();
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * transform.up;
    }
}
