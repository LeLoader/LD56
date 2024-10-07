using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Header("Life")]
    [SerializeField]
    GameObject lifeWrapper;
    [SerializeField]
    GameObject heart_Prefab;
    [SerializeField]
    Sprite fullHeart;
    [SerializeField]
    Sprite halfHeart;
    [SerializeField]
    Sprite fullBonusHeart;
    [SerializeField]
    Sprite halfBonusHeart;

    [Header("Food")]
    [SerializeField]
    GameObject foodWrapper;
    [SerializeField]
    GameObject food_prefab;

    [Header("Weapon")]
    [SerializeField]
    Image weaponUI;
    [SerializeField]
    GameObject knife;
    [SerializeField]
    GameObject pan;

    [Header("Menu")]
    [SerializeField]
    GameObject menu;
    [SerializeField]
    GameObject deathScreen;

    public static event Action OnRetry;

    void Awake()
    {
        Character.OnUpdateHealth += UpdateHealth;
        GameManager.OnUpdateRequest += UpdateFood;
        Weapon.OnUpdateWeapon += UpdateWeapon;
        Player.ToggleMenu += ToggleMenu;
        Character.OnDeathh += ShowDeathScreen;
        
    }

    void UpdateHealth(Character character)
    {
        if (character.GetComponent<Player>())
        {
            int fullRedHeartCount = character.GetLife() / 2;
            bool hasHalfRedHeart = character.GetLife() % 2 == 1;
            int fullBonusHeartCount = character.GetBonusLife() / 2;
            bool hasHalfBonusHeart = character.GetBonusLife() % 2 == 1;

            foreach (Transform transform in lifeWrapper.transform) // Clear
            {
                Destroy(transform.gameObject);
            }

            for (int i = 0; i < fullRedHeartCount; i++)
            {
                Image heartImage = Instantiate(heart_Prefab, lifeWrapper.transform).GetComponent<Image>();
                heartImage.sprite = fullHeart;
            }

            if (hasHalfRedHeart)
            {
                Image heartImage = Instantiate(heart_Prefab, lifeWrapper.transform).GetComponent<Image>();
                heartImage.sprite = halfHeart;
            }

            for (int i = 0; i < fullBonusHeartCount; i++)
            {
                Image heartImage = Instantiate(heart_Prefab, lifeWrapper.transform).GetComponent<Image>();
                heartImage.sprite = fullBonusHeart;
            }

            if (hasHalfBonusHeart)
            {
                Image heartImage = Instantiate(heart_Prefab, lifeWrapper.transform).GetComponent<Image>();
                heartImage.sprite = halfBonusHeart;
            }
        }
    }

    void UpdateFood(Request request)
    {
        foreach (Transform transform in foodWrapper.transform) // Clear
        {
            Destroy(transform.gameObject);
        }

        foreach (KeyValuePair<FoodData, FoodState> ingredient in request.recipe)
        {
            GameObject go = Instantiate(food_prefab, foodWrapper.transform);
            Image image = go.GetComponentsInChildren<Image>().First(component => component.gameObject != go);
            image.sprite = ingredient.Key.sprite;
            if(ingredient.Value == FoodState.Alive)
            {
                image.color = new Color(0.3f, 0.3f, 0.3f);
            }      
        }
    }

    public void AddWeapon(WeaponData weapon)
    {
        if (weapon.weaponName == "knife")
        {
            knife.SetActive(true);
        }
        else if (weapon.weaponName == "pan")
        {
            pan.SetActive(true);
        }
    }

    private void UpdateWeapon(WeaponData weapon)
    {
        weaponUI.sprite = weapon.sprite;
    }

    void ToggleMenu()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }
    }

    void ShowDeathScreen(Character character)
    {
        if (character.CompareTag("Player"))
        {
            deathScreen.SetActive(true);
        }
    }

    public void Retry()
    {
        deathScreen.SetActive(false);
        menu.SetActive(false);
        OnRetry.Invoke();
    }
}
