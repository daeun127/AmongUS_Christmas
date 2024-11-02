using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSetting : MonoBehaviour
{
    private bool state_s = false;   // ��Ÿ ���� or ����
    private bool state_d = false;   // �罿�� ���� or ����
    public GameObject santa;    // ��Ÿ ������Ʈ
    public GameObject deer; // �罿�� ������Ʈ

    public void OnclickSBtn()   // ��Ÿ ��ư ������
    {
        if (state_s == false)   // ����������
        {
            santa.SetActive(true);  // ������Ʈ ��
            state_s = true; // �� ����
        }
        else
        {
            santa.SetActive(false); // ������Ʈ ��
            state_s = false;    // �� ����
        }
    }

    public void OnclickDBtn()   // �� ��ư ������
    {
        if (state_d == false)   // ����������
        {
            deer.SetActive(true);      // ������Ʈ ��
            state_d = true; // �� ����
        }
        else
        {
            deer.SetActive(false); // ������Ʈ ��
            state_d = false;    // �� ����
        }
    }
}
