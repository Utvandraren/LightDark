using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField] private string loadingScene;
    //[SerializeField] private UIManager uiManager;

    private string currentLevel;
    private string previousLevel;
    //private string nextLevel;
    private List<AsyncOperation> loadOperations;
    private List<GameObject> instancedSystemPrefabs;

    public void Startup()
    {
        status = ManagerStatus.Started;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        loadOperations = new List<AsyncOperation>();
        if (!Application.isEditor)
            LoadScene("MainMenu");
        
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
            //if (currentLevel == loadingScene)
            //{
            //    UnloadScene(previousLevel);
            //}
            //else if (currentLevel != loadingScene && SceneManager.GetSceneByName(loadingScene).isLoaded)
            //{
            //    UnloadScene(loadingScene);
            //}
            //transition between scenes here
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentLevel)); //Required for SceneManager.GetActiveScene to work properly

       
        Debug.Log("Load complete");
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload complete");

        //if (currentLevel == loadingScene)
        //{
        //    LoadScene(nextLevel);
        //}
    }

    /// <summary>
    /// Loads a level additively on top of already loaded scenes.
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        currentLevel = sceneName;

        if (ao == null)
        {
            Debug.LogError("[GameManager] unable to load level: " + sceneName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        loadOperations.Add(ao);
    }

    /// <summary>
    /// Unloads one of the scenes that are currently loaded.
    /// </summary>
    /// <param name="sceneName"></param>
    public void UnloadScene(string sceneName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(sceneName);

        if (ao == null)
        {
            Debug.LogError("[Gamemanager] unable to unload level " + sceneName);
            return;
        }

        ao.completed += OnUnloadOperationComplete;
    }

    public void GoToLevel(string sceneName)
    {
        previousLevel = currentLevel;
        UnloadScene(previousLevel);
        LoadScene(sceneName);

        Cursor.visible = false;
    }

    public void RestartLevel()
    {
        //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().ResetStats();
        GoToLevel("MainMenu");
    }

    //public void GameOver()
    //{
    //    //uiManager.SetLoseUI();
    //}

    public void ReturnToMain()
    {
        GoToLevel("MainMenu");
    }

    public void Win()
    {
        //Load timelineendingscene and credit
        LoadScene("Win");
        Debug.Log("Game won!!!");
    }

    public void Lose()
    {

    }

    //protected override void OnDestroy()
    //{
    //    base.OnDestroy();
    //    for (int i = 0; i < instancedSystemPrefabs.Count; i++)
    //    {
    //        Destroy(instancedSystemPrefabs[i]);
    //    }

    //    instancedSystemPrefabs.Clear();
    //}


}
