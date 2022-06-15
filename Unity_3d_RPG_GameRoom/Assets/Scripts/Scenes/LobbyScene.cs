using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
    }

    protected override void SceneSetting()
    {
        //Scene Setting (SceneType, SceneName, SceneBGM)
        SceneType = Defines.Scenes.Lobby;
        SceneName = "Lobby";
        //Managers.Sound.Play($"{SceneName}/Bgm", Defines.Sounds.Bgm, 0.5f);
    }

    protected override void ObjectInit()
    {
        //Player Setting
        GameObject player = Managers.Actor.Spawn(Defines.Actors.Player, $"{SceneName}/Player/Player");
        PlayerController pc = player.GetOrAddComponent<PlayerController>();

        //Camera Setting
        GameObject camPivot = new GameObject { name = "camPivot" };
        camPivot.GetOrAddComponent<FollowObject>().SetFollowTarget(player.transform);
        CameraController cc = Camera.main.gameObject.GetOrAddComponent<CameraController>();
        cc.SetCamPivot(camPivot.transform);
        cc.SetFollowTarget(player.transform);

        //Player in camPivot Setting
        pc.SetCamPivot(camPivot.transform);

        //Envis Setting
        GameObject envis = Managers.Actor.Spawn(Defines.Actors.Envis, $"{SceneName}/Envis/Envis");

        Transform EnterLights = envis.transform.Find("EnterLights");
        foreach (Transform child in EnterLights)
            Managers.UI.MakeWorldSpaceUI<UI_Interaction>(child.transform);

        //Joystick Setting
        Transform camRotator = Managers.UI.ShowSceneUI<UI_Joystick>().transform.Find("CamRotator");
        camRotator.gameObject.GetOrAddComponent<UI_CamRotator>().SetCamPivot(camPivot.transform);

        //Button Setting
        Managers.UI.ShowSceneUI<UI_EnterButton>();
    }

    public override void Clear()
    {
        Debug.Log($"{SceneName} Clear");
    }
}
