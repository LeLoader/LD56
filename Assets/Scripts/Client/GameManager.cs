using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] 
    DBFood foodDatabase;
    [SerializeField]
    int maxClientInQueue = 2;
    [SerializeField]
    List<Vector2> queuePositions;
    [SerializeField]
    Vector2 clientSpawn;

    public static int orderCount = 0;
    Queue<Client> clients = new();

    [SerializeField]
    float spawnCooldown = 2;
    float spawnCountdown = 0;

    [SerializeField]
    GameObject clientPrefab;

    private void Awake()
    {
        Client.OnNewRequest += OnNewClient;
        Client.OnRequestFullfilled += OnRequestFullfilled;
    }

    private void Update()
    {
        if (clients.Count < maxClientInQueue) 
        {
            if(spawnCountdown <= 0)
            {
                Client client = Instantiate(clientPrefab, clientSpawn, Quaternion.identity).GetComponent<Client>();
                clients.Enqueue(client);
                client.walkQueue.Enqueue(queuePositions[clients.IndexOf(client)]);
                spawnCountdown = spawnCooldown;
            }
            else
            {
                spawnCountdown -= Time.deltaTime;
            }   
        }
    }

    void OnRequestFullfilled(Request request)
    {
        clients.Dequeue(); //Set position to go away
    }

    void OnNewClient(Client client)
    {
        client.SetRequest(CreateDish());
        orderCount++;
    }

    Request CreateDish()
    {
        List<FoodData> possibleIngredients = new(foodDatabase.GetFoodList());
        int rollMin = 1;
        int rollMax;

        switch (orderCount)
        {
            case 0:
                rollMax = 1;
                break;
            case < 5:
                rollMax = 3;
                break;
            case < 10:
                rollMin = 2;
                rollMax = 5;
                break;
            case >= 10:
                rollMin = 3;
                rollMax = 6;
                break;
        }

        int ingredientCount = Random.Range(rollMin, rollMax + 1);
        List<FoodData> ingredients = new();
        
        for (int i = 0; i < ingredientCount; i++)
        {
            if (possibleIngredients.Count == 0)
            {
                break;
            }

            int random = Random.Range(0, possibleIngredients.Count);
            ingredients.Add(possibleIngredients[random]);
            possibleIngredients.RemoveAt(random);
        }

        return new Request(ingredients);
    }
}
