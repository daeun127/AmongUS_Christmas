using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollPresent : MonoBehaviour
{
    public GameObject present; // ���� ������Ʈ

    private void OnTriggerEnter(Collider other) //�ε�����
    {
        if (other.tag == "crew" || other.tag == "Player")   // �±װ� ũ��ų� �÷��̾ ũ���� ��
        {
            present.transform.rotation = Quaternion.Euler(0, 0, 0); // ȸ�� ���� 000���� ����
            GameObject.Find("GameManager").GetComponent<GameManager>().AddTask1();  // �̼�1 ī��Ʈ
        }
    }
}
