using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Security.Cryptography;
using UnityEngine;
using XLua;

public class LuaManager : IManager
{
    private LuaEnv m_LuaEnv = null;
    public LuaEnv luaEnv
    {
        get
        {
            return m_LuaEnv;
        }
    }  

    #region Flow

    public override void OnCreateFinish()
    {
        m_LuaEnv = new LuaEnv();
        m_LuaEnv.AddLoader(LoadFile);

        LoadMainCode();

        m_ActionEnter("");
    }

    public override void OnDestroy()
    {
        if (m_ActionExit != null)
        {
            m_ActionExit();
        }

        m_ActionEnter = null;
        m_ActionUpdate = null;
        m_ActionLateUpdate = null;
        m_ActionExit = null;
        m_ActionPause = null;
        m_ActionFocus = null;

        m_DictCode.Clear();
    }

    public override void OnDestroyFinish()
    {
        /*
        //退出前的调试
        var action = m_LuaEnv.Global.Get<DelegateEmpty>("__exit_debug__");
        if (action != null)
        {
            action();
            action = null;
        }

        m_LuaEnv.Dispose();
        */

        m_LuaEnv = null;
    }

    public override void OnUpdate(float deltaTime)
    {
        if (m_ActionUpdate != null)
        {
            m_ActionUpdate(deltaTime);
        }

        if (m_LuaEnv != null)
        {
            m_LuaEnv.Tick();
        }
    }

    public override void OnLateUpdate(float deltaTime)
    {
        if (m_ActionLateUpdate != null)
        {
            m_ActionLateUpdate(deltaTime);
        }
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        if (m_ActionFocus != null)
        {
            m_ActionFocus(hasFocus);
        }
    }

    public void OnApplicationPause(bool pauseStatus)
    {
        if (m_ActionPause != null)
        {
            m_ActionPause(pauseStatus);
        }
    }

    #endregion

    #region Lua流程

    [CSharpCallLua]
    public delegate void DelegateEmpty();

    [CSharpCallLua]
    public delegate void DelegateString(string str);

    [CSharpCallLua]
    public delegate void DelegateFloat(float f);

    [CSharpCallLua]
    public delegate void DelegateBool(bool b);

    private DelegateString m_ActionEnter;

    private DelegateFloat m_ActionUpdate;

    private DelegateFloat m_ActionLateUpdate;

    private DelegateEmpty m_ActionExit;

    private DelegateBool m_ActionPause;

    private DelegateBool m_ActionFocus;

    #endregion

    #region Code

    private Dictionary<string, byte[]> m_DictCode = new Dictionary<string, byte[]>();

    private void LoadMainCode()
    {
        m_LuaEnv.DoString("require 'main'");

        m_ActionEnter = m_LuaEnv.Global.Get<DelegateString>("__enter__");
        m_ActionUpdate = m_LuaEnv.Global.Get<DelegateFloat>("__update__");
        m_ActionLateUpdate = m_LuaEnv.Global.Get<DelegateFloat>("__late_update__");
        m_ActionExit = m_LuaEnv.Global.Get<DelegateEmpty>("__exit__");
        m_ActionPause = m_LuaEnv.Global.Get<DelegateBool>("__pause__");
        m_ActionFocus = m_LuaEnv.Global.Get<DelegateBool>("__focus__");
    }

    private byte[] LoadFile(ref string filePath)
    {
        var newFilePath = filePath.Replace('.', '/').ToLower();

        if (!newFilePath.EndsWith(".lua"))
        {
            newFilePath += ".lua";
        }

#if UNITY_EDITOR
        //if (Global.launchConfig.luaFromBundle == false)
        {
            string fullPath = Path.Combine(Application.dataPath, "../Lua/", newFilePath);
            if (File.Exists(fullPath))
            {
                byte[] data = File.ReadAllBytes(fullPath);
                if (IsUTF8Bom(data))
                {
                    Debug.LogErrorFormat("LuaManager.LoadFile: 文件{0}编码错误，去掉UTF8-Bom标记", filePath);
                    return CleanUTF8Bom(data);
                }

                return data;
            }
        }
#endif

        byte[] code = null;
        if (m_DictCode.TryGetValue(newFilePath, out code))
        {
            code = CleanUTF8Bom(code);
            return code;
        }

        Debug.LogErrorFormat("LuaManager.LoadFile: 加载Lua文件{0}失败", filePath);
        return null;
    }

    public void DoLuaString(string luaCode)
    {
        m_LuaEnv.DoString(luaCode);
    }

    public bool IsUTF8Bom(byte[] bytes)
    {
        if (bytes.Length > 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
        {
            return true;
        }

        return false;
    }

    public byte[] CleanUTF8Bom(byte[] bytes)
    {
        if (bytes.Length > 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
        {
            var oldBytes = bytes;
            bytes = new byte[bytes.Length - 3];
            Array.Copy(oldBytes, 3, bytes, 0, bytes.Length);
        }
        return bytes;
    }

    #endregion

    public LuaTable GetNewTable()
    {
        return m_LuaEnv.NewTable();
    }
}
