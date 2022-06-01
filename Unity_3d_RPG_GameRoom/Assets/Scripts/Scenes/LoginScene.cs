using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
    }
    protected override void SceneSetting()
    {
        SceneType = Defines.Scenes.Login;
        SceneName = "Login";
    }
    protected override void ObjectInit()
    {

    }

    public override void Clear()
    {
        Debug.Log($"{SceneName} Clear");
    }
}
