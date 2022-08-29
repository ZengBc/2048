/******************************************************************
** 文件名:
** 版  权:
** 创建人:  Liange
** 联系QQ:  376993313
** 日  期:  2019/04/25

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
*******************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IManager 
{
    //public virtual bool IsInitialized { get; protected set; }

    public bool IsPauseUpdate = false;

    public virtual void OnCreate()
    {

    }

    public virtual void OnCreateFinish()
    {

    }

    public virtual void OnDestroy()
    {

    }

    public virtual void OnDestroyFinish()
    {

    }

    public virtual void OnUpdate(float deltaTime)
    {

    }

    public virtual void OnLateUpdate(float deltaTime)
    {

    }

    public virtual void OnLowMemory()
    {

    }
}
