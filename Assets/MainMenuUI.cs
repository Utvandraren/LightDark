using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject startObj;
    [SerializeField] GameObject settingsObj;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void StartFirstLevel()
    {
        Managers.Level.UnloadScene("MainMenu");
        Managers.Level.LoadScene("Dark");
    }

    public void GoToStartMenu()
    {
        startObj.SetActive(true);
        settingsObj.SetActive(false);
    }

    public void ToogleSettings()
    {
        settingsObj.SetActive(!settingsObj.activeSelf);
    }
}
