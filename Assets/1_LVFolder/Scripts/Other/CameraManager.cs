using Player;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _mouseSensivity = 2.0f;

    [SerializeField] private float _distance_Y = 2.0f;
    [SerializeField] private float _distance_Z = 5.0f;

    [SerializeField] private float _rotateLimit_min = 5.0f;
    [SerializeField] private float _rotateLimit_max = 5.0f;

    private float _mouseMovement_X = 0f;
    private float _mouseMovement_Y = 0f;

    private Transform _playerTransform;

    private struct ShakeInfo
    {
        public ShakeInfo(float duration, float strength, float vibrato)
        {
            Duration = duration;
            Strength = strength;
            Vibrato = vibrato;
        }
        public float Duration { get; } // 時間
        public float Strength { get; } // 揺れの強さ
        public float Vibrato { get; }  // どのくらい振動するか
    }
    private ShakeInfo _shakeInfo;
    private Vector3 _initPosition; // 初期位置
    private bool _isShakeing;       // 揺れ実行中か？
    private float _totalShakeTime; // 揺れ経過時間

    public static CameraManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _playerTransform = PlayerCore.Instance.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        ExecuteShakePosition();
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    public void StartShake(float duration, float strength, float vibrato)
    {
        _shakeInfo = new ShakeInfo(duration, strength, vibrato);
        _isShakeing = true;
        _totalShakeTime = 0f;
    }

    private void ExecuteShakePosition()
    {
        if (!_isShakeing)
        {
            return;
        }

        _initPosition = transform.position;

        // 位置更新
        transform.position = UpdateShakePosition(
        transform.position,
        _shakeInfo,
        _totalShakeTime,
        _initPosition);

        _totalShakeTime += Time.deltaTime;
        if (_totalShakeTime >= _shakeInfo.Duration)
        {
            _isShakeing = false;
            _totalShakeTime = 0f;

            transform.position = _initPosition;
        }
    }

    private Vector3 UpdateShakePosition(Vector3 currentPosition, ShakeInfo shakeInfo, float totalTime, Vector3 initPosition)
    {
        // 揺れの強さを取得
        float strength = shakeInfo.Strength;
        float randomX = Random.Range(-1.0f * strength, strength);
        float randomY = Random.Range(-1.0f * strength, strength);

        // 現在の位置に加える
        Vector3 position = currentPosition;
        position.x += randomX;
        position.y += randomY;

        float vibrato = shakeInfo.Vibrato;
        float ratio = 1.0f - totalTime / shakeInfo.Duration;

        //フェードアウト 経過時間によって揺れを減衰させる
        vibrato *= ratio;   
        position.x = Mathf.Clamp(position.x, initPosition.x - vibrato, initPosition.x + vibrato);
        position.y = Mathf.Clamp(position.y, initPosition.y - vibrato, initPosition.y + vibrato);

        return position;
    }

    private void FollowPlayer()
    {
        if (_playerTransform == null)
        {
            Debug.LogError("Playerの座標が不明です");
            return;
        }
        if (_isShakeing)
        {
            return;
        }

        //マウスの移動量を取得 +-は設定でいじれるようにしたい
        _mouseMovement_X += Input.GetAxis("Mouse X") * _mouseSensivity;
        _mouseMovement_Y -= Input.GetAxis("Mouse Y") * _mouseSensivity;

        // Y方向は制限　地面の裏側が見えたらまずい
        _mouseMovement_Y = Mathf.Clamp(_mouseMovement_Y, _rotateLimit_min, _rotateLimit_max);
        var rotation = Quaternion.Euler(_mouseMovement_Y, _mouseMovement_X, 0);

        // プレイヤーの座標から、指定距離分だけ離れる
        Vector3 position = _playerTransform.position + rotation * new Vector3(0, _distance_Y, -_distance_Z);

        transform.rotation = rotation;
        transform.position = position;
    }
}
