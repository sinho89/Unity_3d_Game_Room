using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defines
{
    public enum Scenes
    {
        Unknown,
        Login,
        Lobby,
        Raise_Assasin,//(3d_RPG)
    }

    public enum Actors
    {
        Unknown,
        Player,
        Monster,
        Npc,
        Envis,
    }

    public enum ActorStates
    {
        Idle = 0,
        Move,
        Attack,
        Casting,
        Hit,
        Die,
    }

    public enum Sounds
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvents
    {
        Click,
        ClickUp,
        ClickDown,
        BeginDrag,
        Drag,
    }
}
