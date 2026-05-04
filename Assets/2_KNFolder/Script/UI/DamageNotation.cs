using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Enemy
{
    public class DamageNotation : MonoBehaviour
    {
        [SerializeField]
        private EnemyBase _enemyBase;
        [SerializeField] private TextMeshPro _damageText;
        [SerializeField] private Vector3 _generatePos;
        private float _pastHp;
        private float _pastShield;

        private void Update()
        {
            if (_pastShield > 0 && _pastShield != _enemyBase.Shield)
            {
                TextMeshPro text = Instantiate(_damageText.gameObject, transform.position + _generatePos, Quaternion.identity, transform).GetComponent<TextMeshPro>();
                string damage = (_pastShield - _enemyBase.Shield).ToString();
                text.text = $"<color=blue>{damage}</color>";
            }

            if (_pastHp > 0 && _pastHp != _enemyBase.Hp)
            {
                TextMeshPro text = Instantiate(_damageText.gameObject, transform.position + _generatePos, Quaternion.identity, transform).GetComponent<TextMeshPro>();
                text.text = (_pastHp - _enemyBase.Hp).ToString();
            }
            _pastHp = _enemyBase.Hp;
            _pastShield = _enemyBase.Shield;
        }
    }
}
