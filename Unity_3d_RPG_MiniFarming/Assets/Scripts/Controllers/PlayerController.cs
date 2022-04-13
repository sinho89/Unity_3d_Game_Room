//#define PC_BASE_CONTROLLER
#define MOBILE_JOYSTICK_BASE_CONTROLLER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ActorController
{
    private Camera _followCam;
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Moving();
    }

    public override void Init()
    {
        _followCam = Camera.main;
        _characterController = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();

        ActorsType = Defines.Actors.Player;
        //_stat = gameObject.GetComponent<Stat>();

        _moveSpeed = 15.0f;
        _jumpVelocity = 5.0f;
    }

    private void Moving()
    {
        float speed = _moveSpeed * Managers.Input.MoveInput.magnitude;

        Vector3 camForwardDir = new Vector3(_followCam.transform.forward.x, 0, _followCam.transform.forward.z);

        Vector3 moveDir = Vector3.Normalize(camForwardDir * Managers.Input.MoveInput.y
                            + _followCam.transform.right * Managers.Input.MoveInput.x);

        float smoothTime = _characterController.isGrounded ? _speedSmoothTime : _speedSmoothTime / _airControlPercent;
        speed = Mathf.SmoothDamp(_currentSpeed, speed, ref _speedSmoothVelocity, smoothTime);
        _currentVelocityY += Time.deltaTime * Physics.gravity.y;

        Vector3 velocity = moveDir * speed + Vector3.up * _currentVelocityY;
        _characterController.Move(velocity * Time.deltaTime);

        // 카메라 회전값 적용
        transform.rotation = Quaternion.Lerp(transform.rotation,
        Quaternion.LookRotation(new Vector3(moveDir.x, 0, moveDir.z)), Time.deltaTime * 10.0f);

        if (_characterController.isGrounded)
        {
            _currentVelocityY = 0;
        }


        _anim.SetFloat("MoveX", Managers.Input.MoveInput.x);
        _anim.SetFloat("MoveY", Managers.Input.MoveInput.y);
    }

}
