using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using System.Linq;

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
    Player player;

    [SerializeField]
    Request currentRequest;

    public static int orderCount = 0;

    [SerializeField]
    Queue<Client> clients = new();

    [SerializeField]
    UI ui;

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

    public WeaponData[] weaponToUnlockImmuable;
    public List<WeaponData> weaponToUnlock;
    public CycleWeapon cycleWeapon;

    public static event Action<Request> OnUpdateRequest;

    private void Awake()
    {
        Client.OnNewRequest += OnNewClient;
        Client.OnRequestFullfilled += OnRequestFullfilled;
        Enemy.OnEnemyDeath += OnEnemyDeath;
        UI.OnRetry += Retry;
    }

    private void Update()
    {
        if (clients.Count < maxClientInQueue)
        {
            if (spawnCountdown <= 0)
            {
                Client client = Instantiate(clientPrefab, clientSpawn, Quaternion.identity).GetComponent<Client>();
                clients.Enqueue(client);
                foreach (Client clientTemp in clients)
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
        if (enemy.foodData != null && currentRequest.recipe != null)
        {
            if (currentRequest.recipe.ContainsKey(enemy.foodData))
            {
                currentRequest.recipe[enemy.foodData] = FoodState.Killed;
            }
            OnUpdateRequest.Invoke(currentRequest);
        }
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

    private void GiveRandomEffect()
    {
        float randomRoll = Random.Range(0f, 1f);
        Debug.Log("Rolled:" + randomRoll);
        if (orderCount != 0)
        {
            if (orderCount % 3 == 0)
            {
                if (randomRoll < 0.1) // new weapon
                {
                    player.ModifyBaseLife(player.baseLife - 1);
                }
                else if (0.1 <= randomRoll && randomRoll < 0.2)
                {
                    player.AddLife(-(player.GetLife() / 2));
                }
                else if (0.2 <= randomRoll && randomRoll < 0.5)
                {
                    foreach (Enemy enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
                    {
                        enemy.AddLife(1);
                    }
                }
                else if (0.5 <= randomRoll && randomRoll < 0.8)
                {
                    player.SetSpeed(player.GetSpeed() - 2);
                }
                else
                {
                    foreach (Enemy enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
                    {
                        enemy.AddLife(2);
                    }
                }
            }
            else
            {
                if (randomRoll < 0.4) // new weapon
                {
                    if (weaponToUnlock.Count == 2)
                    {
                        int randomIndex = (int)Time.time % 2;
                        cycleWeapon.unlockedWeaponList.Add(weaponToUnlock[randomIndex]);
                        ui.AddWeapon(weaponToUnlock[randomIndex]);
                        weaponToUnlock.RemoveAt(randomIndex);
                    }
                    else if (weaponToUnlock.Count == 1)
                    {
                        cycleWeapon.unlockedWeaponList.Add(weaponToUnlock[0]);
                        weaponToUnlock.Clear();
                        ui.AddWeapon(weaponToUnlock[0]);
                    }
                }
                else if (0.4 <= randomRoll && randomRoll < 0.5)
                {
                    player.ModifyBaseLife(player.baseLife + 2);
                    player.AddLife(2);
                }
                else if (0.5 <= randomRoll && randomRoll < 0.8)
                {
                    player.AddBonusLife(2);
                }
                else
                {
                    player.SetSpeed(player.GetSpeed() + 2);
                }
            }
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
            Enemy enemy = Instantiate(food.prefab, food.spawnPosition, Quaternion.identity).GetComponent<Enemy>();

            enemy.foodData = food;
        }

        GiveRandomEffect();
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

    void Retry()
    {
        weaponToUnlock = weaponToUnlockImmuable.ToList();
        orderCount = 0;
        clients.Clear();
        currentRequest = null; 
    }
}
