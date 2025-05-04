using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int ShipCounter;
    public GameObject LevelCompletedPanel;

    [SerializeField] public List<Controller> m_CarsOnLevel = new List<Controller>();
    public static LevelManager Instance { get; private set; }
   
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    private void Start()
    {
    }
    public void AddCar(Controller controller)
    {
        if(m_CarsOnLevel.Contains(controller))
            return;

        m_CarsOnLevel.Add(controller);
        ShipCounter++;
    }
    public void DeleteCar(GameObject Car)
    {
        ShipCounter--;
        Destroy(Car);
        ShipManager.Instance.cars.Remove(Car.GetComponent<Controller>());
        if (ShipCounter == 0)
        {
            Debug.Log("xxxxxx");
            SceneManagement.Instance.CurrentGameLevel++;
            LevelCompletedPanel.SetActive(true);
            PlayerPrefs.SetInt(SceneManagement.Instance.s_currentgamelevel, SceneManagement.Instance.CurrentGameLevel);
        }
    }
    public void SetCurrentLevel()
    {
        SceneManager.LoadScene(0);
    }
}

