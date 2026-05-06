using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image HPBarGreen;
    public Image HPBarRed;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI LevelText;

    public Image EXPBarBlue;

    [Header("スペシャルゲージ関連")]
    public Image SpecalBar;
    [SerializeField] private Image _specalBarBack; // バーの後ろの装飾
    [SerializeField] private TextMeshProUGUI _spText; // テキストの装飾
    [SerializeField] private TextMeshProUGUI _spText2; // テキストの装飾2
    public Color FlashColor = Color.yellow; // 点滅時の色
    public float FlashSpeed = 5.0f; // 点滅の速さ


    public static UIManager Instance {  get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Player.PlayerCore.Instance != null && Player.PlayerCore.Instance.PermissionSpecialAttack)
        {
            if (!_spText2.gameObject.activeSelf)
            {
                _spText2.gameObject.SetActive(true);    // PushSpace！という表示を出す
            }
            FlashSpecialBar();
        }
        else
        {
            if (_spText2.gameObject.activeSelf)
            {
                _spText2.gameObject.SetActive(false);   // PushSpace！という表示を消す
            }
            ResetSpecialBarColor();
        }
    }

    // ピカピカさせる処理
    private void FlashSpecialBar()
    {
        if (_spText == null || _spText2 == null)
        {
            return;
        }

        // 0～1の間を一定速度で往復
        float lerp = Mathf.PingPong(Time.time * FlashSpeed, 1.0f);

        // 元の色とFlashColorの間で色を補完する
        _specalBarBack.color = Color.Lerp(Color.white, FlashColor, lerp);
        _spText.color = Color.Lerp(Color.white, FlashColor, lerp);
        _spText2.color = Color.Lerp(Color.white, FlashColor, lerp);
    }

    // 白色に戻す
    private void ResetSpecialBarColor()
    {
        if (_spText == null || _spText2 == null)
        {
            return;
        }

        if (_specalBarBack.color != Color.white)
        {
            _specalBarBack.color = Color.white;
        }
        if (_spText.color != Color.white)
        {
            _spText.color = Color.white;
        }
        if(_spText2.color != Color.white)
        {
            _spText2.color = Color.white;
        }
    }
}
