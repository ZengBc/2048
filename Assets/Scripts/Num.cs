using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region ���ڵ�Ԥ�����ϵ��������
public class Num : MonoBehaviour
{
    #region ȫ�ֱ���
    public int newPosX;
    public int newPosY;
    public bool toDestroy;

    private int offsetX = 192;      //�� +
    private int offsetY = 1308;     //�� -
    private int space = 232;        //200+32

    public int value;
    public bool isMoving = false;
    #endregion

    #region ���ؾ���
    private Sprite LoadSprite(int _value)
    {
        value = _value;
        return Resources.Load<Sprite>(value.ToString());
    }
    #endregion

    #region ��NumPoolTransformΪ��׼���λ��
    public Vector3 GetLocalPos(int _posX, int _posY)
    {
        return new Vector3(offsetX + _posX * space, offsetY - _posY * space, 0);
    }
    #endregion

    #region ���ֳ�ʼ��
    public void CreateNumber(int _posX, int _posY, int _value)
    {
        GetComponent<Image>().sprite = LoadSprite(_value);
        transform.localPosition = GetLocalPos(_posX, _posY);
    }
    #endregion

    #region �ϲ�������ֵ�ӱ�
    public void DoubleValue()
    {
        GetComponent<Image>().sprite = LoadSprite(value*2);
    }
    #endregion

    #region ѭ������Ƿ�������Ҫ�ƶ�
    void Update()
    {
        if (isMoving)
        {
            transform.SetParent(Slave.s_NumPoolTransform);
            if (transform.localPosition != GetLocalPos(newPosX, newPosY))
            {
                StartCoroutine(MoveAni());
            }
        }
    }

    IEnumerator MoveAni()
    {
        float t = 0;
        for (int i = 0; i < 20; i++)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, GetLocalPos(newPosX,newPosY),t);
            t += 0.01f;
            yield return new WaitForEndOfFrame();
        }
        isMoving = false;
    }
    #endregion

    #region ��������
    public void Destroy()
    {
        Destroy(gameObject);
    }
    #endregion
}
#endregion