using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    LuaManager luaManager = new LuaManager();

    void Start()
    {
        luaManager.OnCreate();
        luaManager.OnCreateFinish();
    }
    void Update()
    {
        if (luaManager.IsPauseUpdate == false)
        {
            luaManager.OnUpdate(Time.deltaTime);
        }
    }
}
