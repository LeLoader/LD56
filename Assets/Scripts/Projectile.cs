using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    public Damage damageComponent;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [Header("General")]
    [SerializeField]
    float speed = 1;
    [SerializeField]
    bool decreaseSpeed;
    [ShowIf("decreaseSpeed"), SerializeField]
    float decreaseFactor = 20.0f;
    [SerializeField]
    Sprite sprite;
    [SerializeField]
    float destroyAfter = 2.0f;

    float initTime;

    private void Start()
    {
        initTime = Time.timeSinceLevelLoad;

        Destroy(gameObject, destroyAfter);

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        damageComponent = GetComponent<Damage>();
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * transform.up;
        if(speed > 0.1 && decreaseSpeed)
        {
            speed -= Time.deltaTime * (Time.timeSinceLevelLoad - initTime) * decreaseFactor;
        }
    }
}
