using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
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


    void Awake()
    {
        Character.OnUpdateHealth += UpdateHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
