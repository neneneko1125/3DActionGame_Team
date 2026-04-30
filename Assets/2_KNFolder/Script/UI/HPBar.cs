using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Image _hpBarGreen;
        [SerializeField] private Image _hpBarRed;
        [SerializeField] private EnemyBase _enemyBase;
        void Update()
        {
            float hpRatio = _enemyBase.Hp / _enemyBase.EnemyData.MaxHp;

            _hpBarGreen.fillAmount = Mathf.Lerp(_hpBarGreen.fillAmount, hpRatio, Time.deltaTime * 10);
            _hpBarRed.fillAmount = Mathf.Lerp(_hpBarRed.fillAmount, hpRatio, Time.deltaTime * 7.5f);
        }
    }
}
