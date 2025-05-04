using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public void ChangeScene()
    {
        SceneManagement.Instance.ChangeScene();

    }
}
