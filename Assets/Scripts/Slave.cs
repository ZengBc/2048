using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//��λ��
public class Slave : MonoBehaviour
{
    //ȫ�ֱ���
    public Transform NumPoolTransform;                                    //���ֳ�transform
    public static Transform s_NumPoolTransform;                           //���ֳ�transform����̬��
    public static GameObject numPrefab;                                   //����Ԥ����
    static private GameObject[,] m_ArrayNum = new GameObject[4,4];        //��������
    static private int uid = -1;                                          //Ψһ���
    static private Dictionary<int, GameObject> m_DictUI = new Dictionary<int, GameObject>();//�洢��ť���ֵ�
    static private Dictionary<int, Queue> m_DictNum = new Dictionary<int, Queue>();
    public static NumController numController = NumController.Instance;
    public static BtnController btnController = BtnController.Instance;
    //Start()
    void Start()
    {
        numPrefab = Resources.Load<GameObject>("Number");
        s_NumPoolTransform = NumPoolTransform;
        numController.CachePoolInit();
    }
    //���ֿ�����
    public class NumController
    {
        static NumController instance;
        public static NumController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NumController();
                }
                return instance;
            }
        }
        public GameObject go;
        public Num num;
        //����
        public void CreateNum(int posX, int posY, int value)
        {
            if (m_DictNum[value].Count <= 0)
            {
                Debug.Log("Create Unity Support");
                numController.SetGoFromGo(Instantiate(numPrefab));
                numController.NumInit(posX, posY, value);
                numController.SetNumTransParent(s_NumPoolTransform);
                numController.SetGoTransLocalScale(Vector3.one);
                numController.SetArrayNum(posX, posY);
            }
            else
            {
                //Debug.Log(posY + "," + posX + "," +value);
                Debug.Log("Create CachePool Support");
                go = (GameObject)m_DictNum[value].Dequeue();
                var num = go.GetComponent<Num>();
                num.transform.localPosition = num.GetLocalPos(posX, posY);
                go.SetActive(true);
                num.newPosX = posX;
                num.newPosY = posY;
                num.isMoving = true;
                m_ArrayNum[posY, posX] = go;
            }
        }
        //����
        public void SlideOperate(int posX, int posY, int newPosX, int newPosY)
        {
            numController.SetGoFromArray(posX, posY);
            numController.ClearArrayNum(posX, posY);
            numController.SetNumNewPos(newPosX, newPosY);
            numController.SetNumMove();
            numController.SetArrayNum(newPosX, newPosY);
        }
        //�ϲ�
        public void MergeOperate(int posX, int posY, int newPosX, int newPosY)
        {
            //��λ���ϵ�����
            numController.SetGoFromArray(newPosX, newPosY);
            numController.DestroyGo();
            //��λ���ϵ�����
            numController.SetGoFromArray(posX, posY);
            numController.ClearArrayNum(posX, posY);
            numController.DoubleAndMove(posX, posY, newPosX, newPosY);
        }
        public void CachePoolInit()
        {
            for (int n = 2; n <= 2048; n = n * 2)
            {
                m_DictNum.Add(n, new Queue());
            }
        }
        public void SetGoFromGo(GameObject go)
        {
            this.go = go;
            num = go.GetComponent<Num>();
        }
        public void SetGoFromArray(int posX,int posY)
        {
            go = m_ArrayNum[posY,posX];
            num = go.GetComponent<Num>();
        }
        public void DoubleAndMove(int posX, int posY, int newPosX, int newPosY)
        {
            if (m_DictNum[2 * num.value].Count <= 0)
            {
                Debug.Log("Merge Unity Support");
                num.newPosX = newPosX;
                num.newPosY = newPosY;
                num.DoubleValue();
                num.isMoving = true;
                m_ArrayNum[newPosY, newPosX] = go;
            }
            else
            {
                Debug.Log("Merge CachePool Support");
                numController.DestroyGo();
                go = (GameObject)m_DictNum[2 * num.value].Dequeue();
                num = go.GetComponent<Num>();
                num.transform.localPosition = num.GetLocalPos(posX, posY);
                go.SetActive(true);
                num.newPosX = newPosX;
                num.newPosY = newPosY;
                num.isMoving = true;
                m_ArrayNum[newPosY, newPosX] = go;
            }
        }
        public void NumInit(int posX,int posY,int value)
        {
            num.CreateNumber(posX, posY, value);
        }
        public void SetNumTransParent(Transform transform)
        {
            num.transform.SetParent(transform);
        }
        public void SetGoTransLocalScale(Vector3 vector3)
        {
            go.transform.localScale = vector3;
        }
        public void SetArrayNum(int posX,int posY)
        {
            m_ArrayNum[posY, posX] = go;
        }
        public void ClearArrayNum(int posX,int posY)
        {
            m_ArrayNum[posY, posX] = null;
        }
        public void Clear()
        {
            Array.Clear(m_ArrayNum, 0, m_ArrayNum.Length);
            m_DictNum.Clear();
        }
        public void SetNumNewPos(int newPosX,int newPosY)
        {
            num.newPosX = newPosX;
            num.newPosY = newPosY;
        }
        public void SetNumMove()
        {
            num.isMoving = true;
        }
        public void DestroyGo()
        {
            var num = go.GetComponent<Num>();
            if (m_DictNum[num.value].Count <= 16)
            {
                Debug.Log("Destroy CachePool Support");
                go.SetActive(false);
                m_DictNum[num.value].Enqueue(go);
            }
            else
            {
                Debug.Log("Destroy Unity Support");
                num.Destroy();
            }
        }
    }
    //��ť������
    public class BtnController {
        static BtnController instance;
        public static BtnController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BtnController();
                }
                return instance;
            }
        }
        //Ѱ�Ұ�ť
        public int FindButton(string _nodeName)
        {
            var _btn = GameObject.Find(_nodeName);
            if (_btn != null && m_DictUI.ContainsValue(_btn) == false)
            {
                uid = uid + 1;
                m_DictUI.Add(uid, _btn);
                return uid;
            }
            else if (m_DictUI.ContainsValue(_btn) == true)
            {
                foreach (var go in m_DictUI)
                {
                    if (go.Equals(_btn))
                    {
                        Debug.Log(go.Key);
                        return go.Key;
                    };
                }
            }
            return -1;
        }
        //��ð�ť
        public Button GetButton(int _uid)
        {
            return m_DictUI[_uid].GetComponent<Button>();
        }
    }
}
