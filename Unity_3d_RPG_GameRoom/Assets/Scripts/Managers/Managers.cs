using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_Instance; // singleton
    static Managers Instance { get { Init(); return s_Instance; } }

    #region Core
    ActorManager _actor = new ActorManager();           // Dictionary기반 게임상의 Object를 관리하는 Manager
    DataManager _data = new DataManager();              // Dictionary기반 Json파일을 파싱하여 Data를 관리하는 Manager
    InputManager _input = new InputManager();           // Key조작과 관련된 변수들을 관리하는 Manager
    PoolManager _pool = new PoolManager();              // Dictionary기반 Pooling Object가 담긴 Stack Pool을 관리하는 Manager 
    ResourceManager _resource = new ResourceManager();  // Prefabs 기반 Object를 생성 및 소멸 관리하는 Manager
    SceneManagerEx _scene = new SceneManagerEx();       // Scene의 이동을 관리하는 Manager
    SoundManager _sound = new SoundManager();           // SoundType기반의 배열로 이루어진 AudioSource를 통해 Clip을 달아 Sound를 출력 관리하는 Manager ( Clips => Dictionary 관리)
    UIManager _ui = new UIManager();                    // UI 생성을 관리하고 PopUp창과 같은 SordID와 컨테이너롤 통해 관리하기 용이한 UI를 Stack 컨테이너로 관리 

    public static ActorManager Actor { get { return Instance._actor; } }
    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    #endregion

    private void Start()
    {
        Init();
    }

    private void Update()
    {
    }

    private static void Init()
    {
        if(s_Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if( go == null )
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_Instance = go.GetComponent<Managers>();

            s_Instance._data.Init();
            s_Instance._pool.Init();
            s_Instance._sound.Init();

        }
    }

    public static void Clear()
    { 
        Actor.Clear();
        Data.Clear();
        Input.Clear();
        Pool.Clear();
        Scene.Clear();
        Sound.Clear();
        UI.Clear();
    }
}
