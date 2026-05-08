using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private string _mainSceneName;
    [SerializeField] private string _tutorialSceneName;

    public TextMeshProUGUI HighScoreText;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        HighScoreText.text = EnemySpawnManager.HighScore.ToString();
    }

    public void OnClickMainScene()
    {
        SceneManager.LoadScene(_mainSceneName);
    }

    public void OnClickTutorialScene()
    {
        SceneManager.LoadScene(_tutorialSceneName);
    }
}
