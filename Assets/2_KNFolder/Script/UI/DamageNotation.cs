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

        private void Update()
        {
            if (_pastHp > 0 && _pastHp != _enemyBase.Hp)
            {
                TextMeshPro text = Instantiate(_damageText.gameObject, transform.position + _generatePos, Quaternion.identity, transform).GetComponent<TextMeshPro>();
                text.text = (_pastHp - _enemyBase.Hp).ToString();
            }
            _pastHp = _enemyBase.Hp;
        }
    }
}
