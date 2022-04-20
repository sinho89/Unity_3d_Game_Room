//#define PC_BASE_INPUT
#define MOBILE_JOYSTICK_BASE_INPUT

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
#if PC_BASE_INPUT
    // PC Input
    public Action<Defines.KeyboardEvents> _keyAction = null;
    public Action<Defines.MouseEvents> _mouseAction = null;

    public string _moveHorizontalAxisName = "Horizontal"; // Left, Right
    public string _moveVerticalAxisName = "Vertical"; // Forward, Back

    public string _jumpAxisName = "Jump";

    public string _mouseXAxisName = "Mouse X";
    public string _mouseYAxisName = "Mouse Y";
    public string _mouseScrollWheelName = "Mouse ScrollWheel";

    public Vector2 MoveInput { get; private set; } // x, y
    public float MouseRotInputX { get; private set; } // rot x
    public float MouseRotInputY { get; private set; } // rot y
    public float MouseWheelValue { get; private set; } // current Wheel Value
    public bool JumpInput { get; private set; }

    private bool _IsLeftPressed = false;
    private float _pressedLeftTime = 0.0f;

    private bool _IsRightPressed = false;
    private float _pressedRightTime = 0.0f;

    private float _minRotYValue = 70.0f;
    private float _maxRotYValue = 80.0f;

    private float _minWheelValue = -3.0f;
    private float _maxWheelValue = -1.0f;

#else

    // Player Move
    public float _joystickHorizontal = 0.0f;
    public float _joystickVertical   = 0.0f;
    public Vector2 MoveInput { get; private set; } // x, y

    // Cam Rotate
    public float _originAngleX = 0.0f;
    public float _originAngleY = 0.0f;
    public float _tempAngleX   = 0.0f;
    public float _tempAngleY   = 0.0f;

    public float _beginX       = 0.0f;
    public float _dragX        = 0.0f;
    public float _beginY       = 0.0f;
    public float _dragY        = 0.0f;
    public Vector4 CamRotateInput { get; private set; } // bx,dx,by,dy

    // Cam Zoom
    public float     _wheelValue = 0.0f;
    public int       _touchCount = 0;
    public Vector2[] _touches = { };
    public bool      _touchMoveCheck = false;
    public float     _touchDist = 0.0f;
    public float     _touchOldDist = 0.0f;
    public float     _camFOV = 0.0f;


#endif

    public void Init()
    {
#if PC_BASE_INPUT
        PcBaseInputInit();
#else
        JostickBaseInputInit();
#endif
    }
    public void OnUpdate()
    {
#if PC_BASE_INPUT
        PcBaseInputUpdate();
#else
        JostickBaseInputUpdate();
#endif
    }
    public void Clear()
    {

#if PC_BASE_INPUT
        PcBaseInputClear();
#else
        JostickBaseInputClear();
#endif
    }
#if PC_BASE_INPUT
    public void PcBaseInputInit()
    {

    }

    public void PcBaseInputUpdate()
    {
        if (Input.anyKey && _keyAction != null)
        {
            MoveInput = new Vector2(Input.GetAxis(_moveHorizontalAxisName), Input.GetAxis(_moveVerticalAxisName));

            if (MoveInput.sqrMagnitude > 1) // 대각선 이동시 보정 처리를 위한 값세팅
                MoveInput = MoveInput.normalized;

            JumpInput = Input.GetKey(KeyCode.Space);

            // Down, Pressing

            // 이동
            if (Input.GetButtonDown(_moveHorizontalAxisName) || Input.GetButtonDown(_moveVerticalAxisName))
                _keyAction.Invoke(Defines.KeyboardEvents.MoveButtonDown);
            if (Input.GetButton(_moveHorizontalAxisName) || Input.GetButton(_moveVerticalAxisName))
                _keyAction.Invoke(Defines.KeyboardEvents.MoveButtonPressing);

            // 점프
            if (Input.GetButtonDown(_jumpAxisName))
                _keyAction.Invoke(Defines.KeyboardEvents.JumpButtonDown);


        }
        else
        {
            // Up

            // 이동
            if (Input.GetButtonUp(_moveHorizontalAxisName) || Input.GetButtonUp(_moveVerticalAxisName))
                _keyAction.Invoke(Defines.KeyboardEvents.MoveButtonUp);

            MoveInput = Vector2.zero;
            JumpInput = false;
        }

        if (_mouseAction != null)
        {
            MouseRotInputX += Input.GetAxis(_mouseXAxisName);
            MouseRotInputY += Input.GetAxis(_mouseYAxisName) * -1;
            MouseRotInputY = Mathf.Clamp(MouseRotInputY, _minRotYValue, _maxRotYValue);


            MouseWheelValue += Input.GetAxis(_mouseScrollWheelName);
            MouseWheelValue = Mathf.Clamp(MouseWheelValue, _minWheelValue, _maxWheelValue);

            if (Input.GetMouseButton(0)) // 좌클릭
            {
                if (!_IsLeftPressed) // 처음 누름
                {
                    _mouseAction.Invoke(Defines.MouseEvents.LeftPointerDown);
                    _pressedLeftTime = Time.time;
                }

                _mouseAction.Invoke(Defines.MouseEvents.LeftPress);
                _IsLeftPressed = true;
            }
            else if (Input.GetMouseButton(1)) // 우클릭
            {
                if (!_IsRightPressed) // 처음 누름
                {
                    _mouseAction.Invoke(Defines.MouseEvents.RightPointerDown);
                    _pressedRightTime = Time.time;
                }

                _mouseAction.Invoke(Defines.MouseEvents.RightPress);
                _IsRightPressed = true;
            }
            else
            {
                if (_IsLeftPressed)
                {
                    if (Time.time < _pressedLeftTime * 0.2f)
                        _mouseAction.Invoke(Defines.MouseEvents.LeftClick);
                    _mouseAction.Invoke(Defines.MouseEvents.LeftPonterUp);
                }


                _IsLeftPressed = false;
                _pressedLeftTime = 0.0f;
            }
        }
    }
    public void PcBaseInputClear()
    {
        _keyAction = null;
        _mouseAction = null; 
    }
#else
    public void JostickBaseInputInit()
    {

    }
    public void JostickBaseInputUpdate()
    {
        MoveInput = new Vector2(_joystickHorizontal, _joystickVertical);
        CamRotateInput = new Vector4(_beginX, _dragX, _beginY, _dragY);

        _touchCount = Input.touchCount;

        if (_touchCount == 2)
        {
            for (int i = 0; i < _touchCount; i++)
                _touches[i] = Input.touches[i].position;

            _touchMoveCheck = (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved);

            _touchDist = (Input.touches[0].position - Input.touches[1].position).sqrMagnitude;

            float fDis = (_touchDist - _touchOldDist) * 0.01f;

            _camFOV -= fDis;

            _wheelValue = Mathf.Clamp(_camFOV, 20.0f, 100.0f);

            _touchOldDist = _touchDist;
        }
        else
            _touchMoveCheck = false;

    }
    public void JostickBaseInputClear()
    {

    }
#endif
}
