#define MOBILE_JOYSTICK_BASE_CONTROLLER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ActorController
{
    private Transform _camPivot;

    public void SetCamPivot(Transform campivot) { _camPivot = campivot; }
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
        _characterController = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        

        ActorsType = Defines.Actors.Player;
        //_stat = gameObject.GetComponent<Stat>();

        _moveSpeed = 15.0f;
        _jumpVelocity = 5.0f;


    }

    private void PcBaseMoving()
    {
        /*float speed = _moveSpeed * Managers.Input.MoveInput.magnitude;

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
        _anim.SetFloat("MoveY", Managers.Input.MoveInput.y);*/
    }
    private void Moving()
    {
        //Vector2 conDir = new Vector2(Managers.Input._joystickHorizontal, Managers.Input._joystickVertical);
        Vector2 conDir = Managers.Input.MoveInput;

        _anim.SetFloat("MoveX", conDir.x);
        _anim.SetFloat("MoveY", conDir.y);

        if (conDir == Vector2.zero) return;

        float thetaEuler = Mathf.Acos(conDir.y / conDir.magnitude) * (180 / Mathf.PI) * Mathf.Sign(conDir.x);

        Vector3 moveAngle = Vector3.up * (_camPivot.transform.rotation.eulerAngles.y + thetaEuler);

        transform.rotation = Quaternion.Euler(moveAngle);


        float speed = _moveSpeed * Managers.Input.MoveInput.magnitude;
        float smoothTime = _characterController.isGrounded ? _speedSmoothTime : _speedSmoothTime / _airControlPercent;
        speed = Mathf.SmoothDamp(_currentSpeed, speed, ref _speedSmoothVelocity, smoothTime);

        _currentVelocityY += Time.deltaTime * Physics.gravity.y;

        Vector3 moveDir = transform.forward.normalized;
        Vector3 velocity = moveDir * speed + Vector3.up * _currentVelocityY;

        _characterController.Move(velocity * Time.deltaTime);
    }

}
