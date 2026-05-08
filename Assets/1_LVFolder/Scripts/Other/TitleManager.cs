using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private string _mainSceneName;

    [SerializeField] private GameObject _mainUI;
    [SerializeField] private GameObject _howToOperateUI;


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

    public void OnClickHowToOperate()
    {
        _mainUI.SetActive(false);
        _howToOperateUI.SetActive(true);
    }

    public void OnClickBack()
    {
        _mainUI.SetActive(true);
        _howToOperateUI.SetActive(false);
    }
}
