using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetId : MonoBehaviour
{
    static public List<string> ids = new List<string>() { "�Ƹ�׺ҿ��ϰ�", "������������", "�������׷���", "�����Ǹ��һ�", "�Ͽ��������̴¼�", "�𸣴°���å" };   // id ����Ʈ
    TextMesh idtext;    // id �ؽ�Ʈ

    void Start()
    {
        idtext = GetComponent<TextMesh>();  // text �Ӽ� �ҷ�����
        int i = Random.Range(0, ids.Count);    // ���� ��ȣ��
        idtext.text = ids[i];     // text �� ����
        ids.RemoveAt(i);    // �� �� ����
    }
}
