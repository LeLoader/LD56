using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LangSelector : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown dropdown;
    [SerializeField]
    LocalizationSettings localizationSettings;

    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        // ILocalesProvider a = localizationSettings.GetAvailableLocales();
        Debug.Log(localizationSettings.GetSelectedLocale());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
