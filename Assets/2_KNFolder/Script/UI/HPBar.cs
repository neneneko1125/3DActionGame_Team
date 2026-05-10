using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Image _hpBarGreen;
        [SerializeField] private Image _hpBarRed;
        [SerializeField] private Image _hpBarBlue;
        [SerializeField] private Image _shieldFrame;
        [SerializeField] private EnemyBase _enemyBase;

        void Update()
        {
            float hpRatio = _enemyBase.Hp / _enemyBase.EnemyData.MaxHp;

            _hpBarGreen.fillAmount = Mathf.Lerp(_hpBarGreen.fillAmount, hpRatio, Time.deltaTime * 10);
            _hpBarRed.fillAmount = Mathf.Lerp(_hpBarRed.fillAmount, hpRatio, Time.deltaTime * 7.5f);

            if (_enemyBase.Shield > 0)
            {
                _hpBarBlue.enabled = true;
                _shieldFrame.enabled = true;

                float shieldRatio = 0;

                if (_enemyBase.EnemyData.DefaultShield <= 0 || _enemyBase.Shield >= _enemyBase.EnemyData.DefaultShield)
                {
                    shieldRatio = 1;
                }
                else
                    shieldRatio = _enemyBase.Shield / _enemyBase.EnemyData.DefaultShield;

                _hpBarBlue.fillAmount = Mathf.Lerp(_hpBarBlue.fillAmount, shieldRatio, Time.deltaTime * 10);
            }
            else
            {
                _hpBarBlue.enabled = false;
                _shieldFrame.enabled = false;
            }

        }
    }
}
