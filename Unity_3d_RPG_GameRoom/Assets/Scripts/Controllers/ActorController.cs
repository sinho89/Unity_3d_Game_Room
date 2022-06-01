using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorController : MonoBehaviour
{
    protected CharacterController _characterController;
    protected Animator _anim;

    protected float _moveSpeed; // Actor 이동 속도

    protected float _speedSmoothTime = 0.1f; // Smooth한 이동을 위한 지연시간

    protected float _speedSmoothVelocity; // SmoothDamp 메서드에서 리턴된 값을 받기 위한 변수

    protected float _currentVelocityY; // Y방향 속도 (중력크기)

    protected float _currentSpeed => // Actor X,Z 속도(지면상의 현재 속도)
        new Vector2(_characterController.velocity.x, _characterController.velocity.z).magnitude;

    public Defines.Actors ActorsType { get; protected set; } = Defines.Actors.Unknown; // ActorType 초기화
    public abstract void Init();
    private void Start()
    {
        Init();
    }
}
