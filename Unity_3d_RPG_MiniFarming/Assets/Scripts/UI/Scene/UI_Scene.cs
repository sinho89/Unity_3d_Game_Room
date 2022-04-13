using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base
{
    enum SceneUIObjects
    {
        SU_Joystick,
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(SceneUIObjects));
        Managers.UI.SetCanvas(gameObject, false);
    }
}
