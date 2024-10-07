using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    GameObject requestWidget;
    [SerializeField]
    GameObject ingredientPrefab;

    [Header("References")]
    [SerializeField]
    Animator animator;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [Header("Movement")]
    [SerializeField]
    Vector2 targetPos;
    [SerializeField]
    int speed;

    public Queue<Vector2> walkQueue = new();

    [SerializeField]
    Request request;

    [SerializeField]
    bool IsPlayerNearby;

    bool HasReachedTargetPos;

    public static event Action<Client> OnNewRequest;
    public static event Action<Request> OnRequestFullfilled;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (walkQueue.Count > 0)
        {
            Walk(walkQueue.Peek());
        }

        if (Input.GetKeyDown(KeyCode.E) && HasReachedTargetPos && IsPlayerNearby)
        {
            TryFullfillRequest();
        }
    }

    void Walk(Vector3 destination)
    {
        if (Vector2.Distance(transform.position, destination) < 0.1f)
        {
            walkQueue.Dequeue();

            if (Vector2.Distance(transform.position, targetPos) < 0.1f)
            {
                OnNewRequest.Invoke(this);
                HasReachedTargetPos = true;
            }

            if(HasReachedTargetPos && request == null)
            {
                Destroy(gameObject);
            }

            animator.SetFloat("xVelocityAbs", 0);
            animator.SetFloat("yVelocity", 0);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);
            Vector2 test = (destination - transform.position).normalized * speed;
            if (test.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
            animator.SetFloat("xVelocityAbs", Mathf.Abs(test.x));
            animator.SetFloat("yVelocity", test.y);
        }
    }

    public void SetRequest(Request request)
    {
        this.request = request;
        if (request != null)
        {
            foreach (FoodData ingredient in request.recipe.Keys)
            {
                GameObject ingredientInstance = Instantiate(ingredientPrefab, requestWidget.transform);

                Image image = ingredientInstance.GetComponentsInChildren<Image>().First(component => component.gameObject != ingredientInstance);
                image.sprite = ingredient.sprite;
                // image.color = new Color(0.3f, 0.3f, 0.3f); needs  to be updated on killed
            }
        }
    }

    void TryFullfillRequest()
    {
        foreach(FoodState state in request.recipe.Values)
        {
            if(state != FoodState.Killed)
            {
                // Animation pas content
                return;
            }
        }
        OnRequestFullfilled.Invoke(request);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            IsPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            IsPlayerNearby = false;
        }
    }
}
