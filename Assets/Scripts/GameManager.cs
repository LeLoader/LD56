using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Random = UnityEngine.Random;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    DBFood foodDatabase;
    [SerializeField]
    int maxClientInQueue = 2;
    [SerializeField]
    List<Vector2> queuePositions;
    [SerializeField]
    List<Vector2> afterQueuePositions;
    [SerializeField]
    Vector2 clientSpawn;

    [SerializeField]
    Request currentRequest;

    public static int orderCount = 0;

    [SerializeField]
    Queue<Client> clients = new();

    [SerializeField]
    float spawnCooldown = 2;
    float spawnCountdown = 0;

    [SerializeField]
    GameObject clientPrefab;

    [Header("Enemy")]
    [SerializeField]
    GameObject meleeEnemy_Prefab;
    [SerializeField]
    GameObject rangedEnemy_Prefab;

    public static event Action<Request> OnUpdateRequest;

    private void Awake()
    {
        Client.OnNewRequest += OnNewClient;
        Client.OnRequestFullfilled += OnRequestFullfilled;
        Enemy.OnEnemyDeath += OnEnemyDeath;
    }

    private void Update()
    {
        if (clients.Count < maxClientInQueue) 
        {
            if(spawnCountdown <= 0)
            {
                Client client = Instantiate(clientPrefab, clientSpawn, Quaternion.identity).GetComponent<Client>();
                clients.Enqueue(client);
                foreach(Client clientTemp in clients)
                {
                    int nextPosIndex = (clients.Count - 1) - clients.IndexOf(clientTemp);
                    clientTemp.walkQueue.Enqueue(queuePositions[nextPosIndex]);
                }

                spawnCountdown = spawnCooldown;
            }
            else
            {
                spawnCountdown -= Time.deltaTime;
            }   
        }
    }

    void OnEnemyDeath(Enemy enemy)
    {
        if (enemy.foodData != null)
        {
            if (currentRequest.recipe.ContainsKey(enemy.foodData))
            {
                currentRequest.recipe[enemy.foodData] = FoodState.Killed;
            }
        }
        OnUpdateRequest.Invoke(currentRequest);
    }

    void OnRequestFullfilled(Request request)
    {
        Client client = clients.Dequeue();
        client.SetRequest(null);
        currentRequest = null;
        //Anim happy
        foreach (Vector2 destination in afterQueuePositions)
        {
            client.walkQueue.Enqueue(destination);
        }
    }

    void OnNewClient(Client client)
    {
        Request request = CreateDish();
        client.SetRequest(request);
        currentRequest = request;
        SpawnFood(request);
        OnUpdateRequest.Invoke(request);
        orderCount++;
    }

    void SpawnFood(Request request)
    {
        foreach (FoodData food in request.recipe.Keys)
        {
            Enemy enemy;
            if (food.IsMelee)
            {
                enemy = Instantiate(meleeEnemy_Prefab, food.spawnPosition, Quaternion.identity).GetComponent<Enemy>();
            }
            else
            {
                enemy = Instantiate(rangedEnemy_Prefab, food.spawnPosition, Quaternion.identity).GetComponent<Enemy>();
            }
            enemy.foodData = food;
            enemy.InitFoodData(food);
        }
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
        Dictionary<FoodData, FoodState> ingredients = new();
        
        for (int i = 0; i < ingredientCount; i++)
        {
            if (possibleIngredients.Count == 0)
            {
                break;
            }

            int random = Random.Range(0, possibleIngredients.Count);
            ingredients.Add(possibleIngredients[random], FoodState.Alive);
            possibleIngredients.RemoveAt(random);
        }

        return new Request(ingredients);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        foreach(Vector2 position in queuePositions)
        {
            Handles.DrawWireDisc(position, Vector3.forward, 0.5f);
        }
    }
}
