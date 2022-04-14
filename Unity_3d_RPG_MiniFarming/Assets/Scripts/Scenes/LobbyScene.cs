using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        LobbyInit();

        Managers.Sound.Play("BGM/LobbyBgm", Defines.Sounds.Bgm, 0.5f);

    }

    private void LobbyInit()
    {
        //Scene Setting
        SceneType = Defines.Scenes.Lobby;
        SceneName = "Lobby";

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
    }

    public override void Clear()
    {
        Debug.Log("LobbyScene Clear");
    }
}
