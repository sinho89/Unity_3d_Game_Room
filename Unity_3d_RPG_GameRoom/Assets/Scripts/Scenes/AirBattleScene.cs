using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBattleScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
    }
    protected override void SceneSetting()
    {
        SceneType = Defines.Scenes.Air_Battle;
        SceneName = "AirBattle";
    }
    protected override void ObjectInit()
    {
        GameObject player = Managers.Actor.Spawn(Defines.Actors.Player, $"{SceneName}/Player/Player");

        Managers.UI.ShowSceneUI<UI_AirBattleBack>();
        Managers.UI.ShowSceneUI<UI_Joystick>();
    }

    public override void Clear()
    {
        Debug.Log($"{SceneName} Clear");
    }

}
