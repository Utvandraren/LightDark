using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransistor : MonoBehaviour
{
    [SerializeField] string levelname;

    public void GoToLevel()
    {
        Managers.Level.LoadScene(levelname);
    }
}
