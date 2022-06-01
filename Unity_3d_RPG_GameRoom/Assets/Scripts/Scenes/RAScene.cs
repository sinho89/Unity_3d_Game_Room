using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
    }
    protected override void SceneSetting()
    {
        SceneType = Defines.Scenes.Raise_Assasin;
        SceneName = "Raise_Assasin";
    }
    protected override void ObjectInit()
    {

    }

    public override void Clear()
    {
        Debug.Log($"{SceneName} Clear");
    }
}
