using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneSetting();
        CommonSetting();

    }

    protected override void SceneSetting()
    {
        //Scene Setting
        SceneType = Defines.Scenes.Lobby;
        SceneName = "Lobby";
    }

    protected override void CommonSetting()
    {
        base.CommonSetting();
    }

    public override void Clear()
    {
        Debug.Log("LobbyScene Clear");
    }
}
