using Enemy;
using Player;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealth : PlayerBase, IDamaged
    {
        [SerializeField] private float _flashingTime = 0.1f;

        [SerializeField] private float _hpFillSpeedGreen;
        [SerializeField] private float _hpFillSpeedRed;

        [SerializeField] private string _titleScene;

        [Header("カメラの振動関連")]
        [SerializeField] private float duration;
        [SerializeField] private float strength;
        [SerializeField] private float vibrato;

        private Renderer[] _renderers; // 全てのメッシュを格納する配列

        private float _currentHp;

        private TextMeshProUGUI _hpText;
        private Image _hpBarGreen;
        private Image _hpBarRed;


        protected override void Awake()
        {
            base.Awake();
            _renderers = GetComponentsInChildren<Renderer>();

            if (Core == null)
            {
                Debug.LogError("コアがないです！");
                return;
            }
            if (Core.PlayerData == null)
            {
                Debug.LogError("SOデータがないです！");
                return;
            }
        }

        private void Start()
        {
            _currentHp = Core.PlayerData.MaxHp + Core.PlayerData.HpBonusPerLevel * (Core.PlayerLevel - 1);    // 強化分も反映

            _hpBarGreen = UIManager.Instance.HPBarGreen;
            _hpBarRed = UIManager.Instance.HPBarRed;
            _hpText = UIManager.Instance.HPText;
        }

        private void Update()
        {
            UpdateUI();
           // DebugOfHP();
        }

        public void ChangeHP(float value)
        {
            // 無敵中 かつ ダメージなら終了 
            if (Core.IsInvicible && value < 0)
            {
                return;
            }

            _currentHp += value;

            if (value < 0)   //ダメージをくらったとき
            {
                CameraManager.Instance.StartShake(duration, strength, vibrato);
                SEManager.Instance.PlaySE_Damage();
                StartCoroutine(DamageSequence());
            }

            if (_currentHp > Core.PlayerData.MaxHp + Core.PlayerData.HpBonusPerLevel * (Core.PlayerLevel - 1))
            {
                Debug.Log("現在のHPが最大HPを上回りました。調整します。");
                _currentHp = Core.PlayerData.MaxHp + Core.PlayerData.HpBonusPerLevel * (Core.PlayerLevel - 1);
            }

            if (_currentHp <= 0)
            {
                StartCoroutine(DeadSequence());
            }
        }

        private IEnumerator DamageSequence()
        {
            Core.IsStunned = true;
            Core.IsInvicible = true;

            StartCoroutine(FlashRoutine());

            Anim.SetFloat(StunRecoverySpeedHash, Core.PlayerData.StunRecoverySpeed);
            Anim.SetTrigger(StunTriggerHash);

            // 1フレーム待ってからアニメーションの終了判定に入る
            yield return null;

            // 今再生されているアニメーションがStunStateNameじゃないならループして待機
            while (!Anim.GetCurrentAnimatorStateInfo(0).IsName(StunStateName))
            {
                yield return null;
            }

            // アニメーションが終わりかけるまでループして待機
            while (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.95f)
            {
                yield return null;
            }

            Core.IsStunned = false;

            // 無敵状態で自由に動ける時間
            yield return new WaitForSeconds(Core.PlayerData.InvincibleDuration);
            Core.IsInvicible = false;
        }

        private IEnumerator DeadSequence()
        {
            Core.IsDead = true;

            // 完全停止
            Rb.linearVelocity = Vector3.zero;
            Rb.angularVelocity = Vector3.zero;
            Rb.isKinematic = true;

            // ゲームオーバーテキストと暗転
            UIManager.Instance.GameOverText.gameObject.SetActive(true);
            UIManager.Instance.BlackImage.gameObject.SetActive(true);

            Anim.SetTrigger(DeadHash);

            // アニメーションの遷移を待つ
            yield return null;

            // アニメーションが終わるまで待機
            while (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.95f)
            {
                yield return null;
            }

            // 少し余韻を持たせてからシーン遷移
            yield return new WaitForSeconds(1.0f);

            SceneManager.LoadScene(_titleScene);
        }

        private IEnumerator FlashRoutine()
        {
            while (Core.IsInvicible)
            {
                // レンダラーOFF
                SetRenderersEnabled(false);
                yield return new WaitForSeconds(_flashingTime); // 点滅の間隔

                // レンダラーON
                SetRenderersEnabled(true);
                yield return new WaitForSeconds(_flashingTime);
            }

            SetRenderersEnabled(true);
        }

        private void SetRenderersEnabled(bool isEnabled)
        {
            foreach (var r in _renderers)
            {
                r.enabled = isEnabled;
            }
        }

        private void UpdateUI()
        {
            if (_hpBarGreen != null && _hpBarRed != null)
            {
                float hpRatio = _currentHp / (Core.PlayerData.MaxHp + Core.PlayerData.HpBonusPerLevel * (Core.PlayerLevel - 1));

                // 緑のバーが速く減って、赤のバーが遅く減る
                _hpBarGreen.fillAmount = Mathf.Lerp(_hpBarGreen.fillAmount, hpRatio, Time.deltaTime * _hpFillSpeedGreen);
                _hpBarRed.fillAmount = Mathf.Lerp(_hpBarRed.fillAmount, hpRatio, Time.deltaTime * _hpFillSpeedRed);
            }

            if (_hpText != null)
            {
                _hpText.text = _currentHp.ToString();
            }
        }

        private void DebugOfHP()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("デバッグ機能　プレイヤーのHPを減少させました");
                ChangeHP(-10f);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("デバッグ機能　プレイヤーのHPを回復させました");
                ChangeHP(1f);
            }
        }
    }
}