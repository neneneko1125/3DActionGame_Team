using UnityEngine;

public class E_Look : MonoBehaviour
{
    [Header("プレイヤーをターゲットとするか")]
    [SerializeField] private bool _targetPlayer;
    [Header("なにをターゲットとするか(プレイヤー以外の場合)")]
    [SerializeField] private GameObject _target;

    private void Start()
    {
        if (_targetPlayer)
        {
            _target = GameObject.Find("Player");
        }
    }
    private void Update()
    {
        if (!_target) return;

        transform.LookAt(_target.transform);
    }

}
