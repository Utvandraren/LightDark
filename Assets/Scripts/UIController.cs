using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private InventoryPopup inventoryPopup;
    [SerializeField] GameObject loseUI;
    [SerializeField] AudioMixer mixer;

    void OnEnable()
    {
        PlayerStats.OnHealthChanged += UpdateHealth;
        PlayerInputManager.OnOpenInventory += OnOpenSettings;
    }

    void Start()
    {
        inventoryPopup.gameObject.SetActive(!inventoryPopup.gameObject.activeSelf);
    }

    public void OnOpenSettings()
    {
        inventoryPopup.gameObject.SetActive(!inventoryPopup.gameObject.activeSelf);
        //Cursor.visible = !(Cursor.visible);
        inventoryPopup.Refresh();
    }

    public void UpdateHealth(int health)
    {
        healthText.text = health.ToString();
    }

    void OnDestroy()
    {
        PlayerInputManager.OnOpenInventory -= OnOpenSettings;
    }

    public void ShowLoseUI()
    {
        loseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseLoseUI()
    {
        loseUI.SetActive(true);
        Time.timeScale = 0f;
        Managers.Level.GoToLevel("Bunker");
    }
}
