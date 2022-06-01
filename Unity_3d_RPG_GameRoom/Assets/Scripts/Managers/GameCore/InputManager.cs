
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    // Player Move
    public float _joystickHorizontal = 0.0f;
    public float _joystickVertical   = 0.0f;

    // Cam Rotate
    public float _originAngleX = 0.0f;
    public float _tempAngleX   = 0.0f;
    public float _originAngleY = 0.0f;
    public float _tempAngleY   = 0.0f;

    public float _beginX       = 0.0f;
    public float _dragX        = 0.0f;
    public float _beginY       = 0.0f;
    public float _dragY        = 0.0f;

    public Vector2 MoveInput => // Joystick H,V°ª
        new Vector2(_joystickHorizontal, _joystickVertical);
    public Vector4 CamRotateInput => // Rotator Touch Point°ª
        new Vector4(_beginX, _dragX, _beginY, _dragY);

    public void Clear()
    {

    }
}
