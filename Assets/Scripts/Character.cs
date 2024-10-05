using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected string characterName;
    [SerializeField] protected float speed = 5;

    [SerializeField] protected int baseLife = 10;
    protected int life;

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Collider2D collisionCollider;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collisionCollider = GetComponent<CircleCollider2D>();

        life = baseLife;
    }

    public string GetName()
    {
        return characterName;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
