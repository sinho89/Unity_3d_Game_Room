using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air2DPlayerController : ActorController
{
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
    }
    private void Moving()
    {
        // InputManager를 통하여 갱신된 이동관련 Input값(조이스틱 Input의 H,V) Getter
        Vector2 conDir = Managers.Input.MoveInput;

        /***** 애니메이션 설정 *****/
        //_anim.SetFloat("MoveX", conDir.x);
        //_anim.SetFloat("MoveY", conDir.y);

        if (conDir == Vector2.zero) return;

        // Player의 Speed, Input방향벡터의 크기, 설정된 Smooth Move 관련 변수를 통하여 최종 speed값 설정
        // 현재 Player의 전방단위벡터, Speed, 중력값을 연산하여 최종 velocity값을 통해 이동시킨다.
        if (conDir.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else
            transform.localScale = new Vector3(1f, 1f, 1f);

        conDir = conDir.normalized;
        float speed = _moveSpeed * conDir.magnitude;
        speed = Mathf.SmoothDamp(_currentSpeed, speed, ref _speedSmoothVelocity, _speedSmoothTime);


        Vector3 moveDir = new Vector3(conDir.x, conDir.y, 0);  //transform.right.normalized; // 방향 단위벡터
        Vector3 velocity = moveDir * _moveSpeed;

        _characterController.Move(velocity * Time.deltaTime); // 이동거리 = 속도 * 시간
    }
}
