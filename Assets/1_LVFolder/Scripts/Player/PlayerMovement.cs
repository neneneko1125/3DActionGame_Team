using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8.0f;
    [SerializeField] private float _gravityScale = 5.0f;
    [SerializeField] private float _turningSpeed = 0.1f;

    private PlayerInput _input;
    private Rigidbody _rb;
    private Animator _anim;

    void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //重力
        _rb.AddForce(Vector3.down * _gravityScale, ForceMode.Acceleration);

        PerformMove();
    }

    private void PerformMove()
    {
        _rb.linearVelocity = _moveSpeed * _input.MoveDirection;

        //移動入力があれば
        if(_input.MoveDirection.magnitude > 0.1f)
        {
            var playerRotation = Quaternion.LookRotation(_input.MoveDirection);

            //Slerpでなめらかにカメラを回転させる
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, _turningSpeed);

            _anim.SetBool("Walk", true);
        }
        else
        {
            _anim.SetBool("Walk", false);
        }
    }
}
