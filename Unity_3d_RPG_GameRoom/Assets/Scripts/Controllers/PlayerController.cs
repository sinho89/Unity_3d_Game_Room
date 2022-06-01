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

        _moveSpeed = 15.0f;
        //_stat = gameObject.GetComponent<Stat>();
        // Move Stat [Json] Parsing 예정
    }
    private void Moving()
    {
        // InputManager를 통하여 갱신된 이동관련 Input값(조이스틱 Input의 H,V) Getter
        Vector2 conDir = Managers.Input.MoveInput;

        /***** 애니메이션 설정 *****/
        _anim.SetFloat("MoveX", conDir.x);
        _anim.SetFloat("MoveY", conDir.y);

        if (conDir == Vector2.zero) return;

        /***** 이동 관련 코드 *****/

        // Input의 방향벡터와 y축 사이의 각도(theta)를 구하고 _camPivot.rotation.y 값에 더하여 플레이어의 회전값을 적용. => 카메라 기준으로 캐릭터의 rotation값을 적용하기 위하여
        // theta = Acos(conDir.y(y축)/conDir.magnitude(Input방향벡터의 크기))
        // thetaEuler = theta * (180/PI)
        // X값이 음수가 될 경우 y는 동일하기 때문에 theta가 양수값이 나온다 => 왼쪽으로 갈수 없는 문제 발생 => 'x의 부호'만 theta값에 곱한다 => Sign(conDir.x)를 곱해줌으로써 해결할 수 있다.
        float thetaEuler = Mathf.Acos(conDir.y / conDir.magnitude) * (180 / Mathf.PI) * Mathf.Sign(conDir.x);
        Vector3 moveAngle = Vector3.up * (_camPivot.transform.rotation.eulerAngles.y + thetaEuler);
        transform.rotation = Quaternion.Euler(moveAngle);
        
        // Player의 Speed, Input방향벡터의 크기, 설정된 Smooth Move 관련 변수를 통하여 최종 speed값 설정
        // 현재 Player의 전방단위벡터, Speed, 중력값을 연산하여 최종 velocity값을 통해 이동시킨다.
        float speed = _moveSpeed * Managers.Input.MoveInput.magnitude;
        speed = Mathf.SmoothDamp(_currentSpeed, speed, ref _speedSmoothVelocity, _speedSmoothTime);

        _currentVelocityY += Time.deltaTime * Physics.gravity.y; // 중력 값

        Vector3 moveDir = transform.forward.normalized; // 방향 단위벡터
        Vector3 velocity = moveDir * speed + Vector3.up * _currentVelocityY;

        _characterController.Move(velocity * Time.deltaTime); // 이동거리 = 속도 * 시간
    }

}
