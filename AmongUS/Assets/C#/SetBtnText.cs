using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBtnText : MonoBehaviour
{
    Text btnText;   // ��ư �ؽ�Ʈ

    void Start()
    {
        btnText = GetComponent<Text>(); // �ؽ�Ʈ �Ӽ� ����

        int i = Random.Range(0, GameManager.textList.Count);    // ���� ��ȣ��
        btnText.text = GameManager.textList[i];     // btntext �� ����
        GameManager.textList.RemoveAt(i);    // �� �� ����
    }
}