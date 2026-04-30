using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class DamageNotation : MonoBehaviour
    {
        [SerializeField]
        private EnemyBase _enemyBase;
        [SerializeField] private Text _damageText;
        [SerializeField] private Vector3 _generatePos;
        private float _pastHp;

        private void Update()
        {
            if (_pastHp > 0 && _pastHp != _enemyBase.Hp)
            {
                Text text = Instantiate(_damageText.gameObject, transform.position + _generatePos, Quaternion.identity, transform).GetComponent<Text>();
                text.text = (_pastHp - _enemyBase.Hp).ToString();
            }
            _pastHp = _enemyBase.Hp;
        }
    }
}
