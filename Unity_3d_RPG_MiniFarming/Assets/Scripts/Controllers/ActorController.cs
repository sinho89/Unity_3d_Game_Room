using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorController : MonoBehaviour
{
    protected CharacterController _characterController;
    protected Animator _anim;

    protected float _moveSpeed;
    protected float _jumpVelocity;
    [Range(0.01f, 1.0f)] protected float _airControlPercent;

    protected float _speedSmoothTime = 0.1f;
    protected float _turnSmoothTime = 0.1f;

    protected float _speedSmoothVelocity;
    protected float _turnSmoothVelocity;

    protected float _currentVelocityY;

    protected float _currentSpeed =>
        new Vector2(_characterController.velocity.x, _characterController.velocity.z).magnitude;

    public abstract void Init();
    private void Start()
    {
        Init();
    }
}
