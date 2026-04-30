using Player;
using Unity.VisualScripting;
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

    private void Start()
    {
        _playerTransform = PlayerCore.Instance.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if(_playerTransform == null)
        {
            Debug.LogError("Playerの座標が不明です");
            return;
        }

        //マウスの移動量を取得 +-は設定でいじれるようにしたい
        _mouseMovement_X += Input.GetAxis("Mouse X") * _mouseSensivity;
        _mouseMovement_Y -= Input.GetAxis("Mouse Y") * _mouseSensivity;

        _mouseMovement_Y = Mathf.Clamp(_mouseMovement_Y, _rotateLimit_min, _rotateLimit_max);

        var rotation = Quaternion.Euler(_mouseMovement_Y,_mouseMovement_X, 0);
        Vector3 position = _playerTransform.position + rotation * new Vector3(0, _distance_Y, -_distance_Z);

        transform.rotation = rotation;
        transform.position = position;
    }
}
