using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

//C#��¶��lua�Ľӿ�
[LuaCallCSharp]
public class CSToLuaExporter
{
    //����
    public static void CreateNum(int posX, int posY, int value)
    {
        Slave.numController.CreateNum(posX, posY, value);
    }
    //����
    public static void SlideOperate(int posX, int posY, int newPosX, int newPosY)
    {
        Slave.numController.SlideOperate(posX, posY, newPosX, newPosY);
    }
    //�ϲ�
    public static void MergeOperate(int posX, int posY, int newPosX, int newPosY) {
        Slave.numController.MergeOperate(posX, posY, newPosX, newPosY);
    }
    //���
    public static void ClearNumArray()
    {
        Slave.numController.Clear();
    }
    //�ð�ť
    public static Button GetButton(int uid) {
        return Slave.btnController.GetButton(uid);
    }
    //�Ұ�ť
    public static int FindButton(string nodename)
    {
        return Slave.btnController.FindButton(nodename);
    }
    //����
    public static int Detect()
    {
        int dir = 0;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dir = 1;
            return dir;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            dir = 2;
            return dir;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            dir = 3;
            return dir;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            dir = 4;
            return dir;
        }
        return 0;
    }
}
