using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager
{
    GameObject _player = null;
    static long _monsterId = 0;

    Dictionary<long, GameObject> _monsters = new Dictionary<long, GameObject>();
    HashSet<GameObject> _envis = new HashSet<GameObject>();


    public Action<int> OnSpawnEvent;

    public GameObject GetPlayer() { return _player; }
    public Defines.Actors GetActorsType(GameObject go)
    {
        ActorController ac = go.GetComponent<ActorController>();
        if (ac == null)
            return Defines.Actors.Unknown;

        return ac.ActorsType;
    }

    public GameObject Spawn(Defines.Actors type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Defines.Actors.Player:
                _player = go;
                break;

            case Defines.Actors.Monster:
                {
                    _monsters.Add(_monsterId, go);
                    _monsterId++;
                    /*if(OnSpawnEvent != null)
                        OnSpawnEvent.Invoke(1);*/
                }
                break;
            case Defines.Actors.Npc:
                break;
            case Defines.Actors.Envis:
                _envis.Add(go);
                break;
        }

        return go;
    }
    public void Despawn(GameObject go)
    {
        Defines.Actors type = GetActorsType(go);

        switch (type)
        {
            case Defines.Actors.Player:
                {
                    if (_player == go)
                        _player = null;
                }
                break;
            case Defines.Actors.Monster:
                {
                    if (_monsters.ContainsKey(_monsterId))
                    {
                        _monsters.Remove(_monsterId);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                        _monsterId--;
                    }
                }
                break;
            case Defines.Actors.Npc:
                break;
            case Defines.Actors.Envis :
                {
                    if (_envis.Contains(go))
                        _envis.Remove(go);
                }
                break;
        }

        Managers.Resource.Destroy(go);
    }
}
