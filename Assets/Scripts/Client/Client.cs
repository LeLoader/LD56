using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    GameObject requestWidget;
    [SerializeField]
    GameObject ingredientPrefab;

    [Header("Movement")]
    [SerializeField]
    Vector2 targetPos;
    [SerializeField]
    int speed;

    public Queue<Vector2> walkQueue = new();

    Request request;
    bool IsFirstCustomer;
    bool HasReachedTargetPos;

    public static event Action<Client> OnNewRequest;
    public static event Action<Request> OnRequestFullfilled;

    // Update is called once per frame
    void Update()
    {
        if (walkQueue.Count > 0)
        {
            Walk(walkQueue.Peek());
        }
    }

    void Walk(Vector2 destination)
    {
        if (Vector2.Distance(transform.position, destination) < 0.1f)
        {
            walkQueue.Dequeue();
            if (Vector2.Distance(transform.position, targetPos) < 0.1f)
            {
                HasReachedTargetPos = true;
                OnNewRequest.Invoke(this);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);
        }
    }

    public void SetRequest(Request request)
    {
        this.request = request;
        foreach (FoodData ingredient in request.recipe)
        {
            GameObject ingredientInstance = Instantiate(ingredientPrefab, requestWidget.transform);

            Image image = ingredientInstance.GetComponent<Image>();
            image.sprite = ingredient.sprite;
            image.color = new Color(0.3f, 0.3f, 0.3f);
        }
    }
}
