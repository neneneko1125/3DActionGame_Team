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
    [SerializeField] private Image _specalBarWhite; // バーの後ろの装飾
    [SerializeField] private TextMeshProUGUI _spText; // テキストの装飾
    public Color FlashColor = Color.yellow; // 点滅時の色
    public float FlashSpeed = 5.0f; // 点滅の速さ

    private Color _colorWhite; // 元の色を保存用

    public static UIManager Instance {  get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            if (_specalBarWhite != null)
            {
                // 起動時の色を保存
                _colorWhite = _specalBarWhite.color;
            }
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
            FlashSpecialBar();
        }
        else
        {
            ResetSpecialBarColor();
        }
    }

    // ピカピカさせる処理
    private void FlashSpecialBar()
    {
        if (_specalBarWhite == null || _spText == null)
        {
            return;
        }

        // 0～1の間を一定速度で往復
        float lerp = Mathf.PingPong(Time.time * FlashSpeed, 1.0f);

        // 元の色とFlashColorの間で色を補完する
        _specalBarWhite.color = Color.Lerp(_colorWhite, FlashColor, lerp);
        _spText.color = Color.Lerp(_colorWhite, FlashColor, lerp);
    }

    // 元の色に戻す
    private void ResetSpecialBarColor()
    {
        if (_specalBarWhite == null || _spText == null)
        {
            return;
        }

        if (_specalBarWhite.color != _colorWhite)
        {
            _specalBarWhite.color = _colorWhite;
        }
        if (_spText.color != _colorWhite)
        {
            _spText.color = _colorWhite;
        }
    }
}
