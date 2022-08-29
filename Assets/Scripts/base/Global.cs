using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global Instance = null;

    private List<IManager> m_ListManager = new List<IManager>();

    #region Mono

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        OnInit();
    }

    void Update()
    {
        OnUpdate(Time.deltaTime);
    }

    void LateUpdate()
    {
        OnLateUpdate(Time.deltaTime);
    }

    void OnDestroy()
    {
        Debug.Log("Global.OnDestroy");

        OnRelease();
        Instance = null;
    }

    #endregion

    #region Flow

    private void OnInit()
    {
        Application.lowMemory += OnLowMemory;

        RegisterManager();

        foreach (var mgr in m_ListManager)
        {
            mgr.OnCreate();
        }

        foreach (var mgr in m_ListManager)
        {
            mgr.OnCreateFinish();
        }
    }

    private void OnRelease()
    {
        Application.lowMemory -= OnLowMemory;

        foreach (var mgr in m_ListManager)
        {
            mgr.OnDestroy();
        }

        foreach (var mgr in m_ListManager)
        {
            mgr.OnDestroyFinish();
        }
    }

    private void OnUpdate(float deltaTime)
    {
        foreach (var mgr in m_ListManager)
        {
            if (mgr.IsPauseUpdate == false)
            {
                mgr.OnUpdate(deltaTime);
            }
        }
    }

    private void OnLateUpdate(float deltaTime)
    {
        foreach (var mgr in m_ListManager)
        {
            if (mgr.IsPauseUpdate == false)
            {
                mgr.OnLateUpdate(deltaTime);
            }
        }
    }

    private void OnLowMemory()
    {
        foreach (var mgr in m_ListManager)
        {
            mgr.OnLowMemory();
        }
    }

    #endregion

    #region Manager

    private LuaManager m_LuaManager;

    private void RegisterManager()
    {
        m_LuaManager = new LuaManager();

        m_ListManager.Add(m_LuaManager);
    }

    #endregion

}
