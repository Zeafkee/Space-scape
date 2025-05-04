using UnityEngine;
using UnityEngine.UI;

public class AssignText : MonoBehaviour
{
    public Text currentLevelText;
    public Text NextLevelText;

    private void Start()
    {
        currentLevelText.text = SceneManagement.Instance.CurrentGameLevel.ToString();
        NextLevelText.text = SceneManagement.Instance.CurrentGameLevel.ToString();
    }

}
