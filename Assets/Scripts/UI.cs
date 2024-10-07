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


    void Awake()
    {
        Character.OnUpdateHealth += UpdateHealth;
        GameManager.OnUpdateRequest += UpdateFood;
    }

    void UpdateHealth(Character character)
    {
        if (character.GetComponent<Player>())
        {
            int fullRedHeartCount = character.life / 2;
            bool hasHalfRedHeart = character.life % 2 == 1;
            int fullBonusHeartCount = character.bonusLife / 2;
            bool hasHalfBonusHeart = character.bonusLife % 2 == 1;

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
}
