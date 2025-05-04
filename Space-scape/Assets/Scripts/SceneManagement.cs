using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance { get; private set; }
    [SerializeField, ReadOnly]
    public int CurrentGameLevel;
    public string s_currentgamelevel = "CurrentGameLevel";

    private void Awake()
    {
        PlayerPrefs.GetInt(s_currentgamelevel, CurrentGameLevel);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {

        DontDestroyOnLoad(gameObject);
    }
    public void ChangeScene() //play
    {
        SceneManager.LoadScene(CurrentGameLevel+1);

    }
    public int GetSceneID(int value)
    {
        return CurrentGameLevel + value;
    }
    public void SetSceneID(int value)
    {
        CurrentGameLevel = value;
    }

}
