using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reportcoll : MonoBehaviour
{
    public Material[] newmat = new Material[2]; // ���׸��� 2�� �迭 ����
    static public float count = 0;    // ���� �ٲ� �� ī��Ʈ ��

    private void OnTriggerEnter(Collider other) // �ε�����
    {
        gameObject.GetComponent<MeshRenderer>().material = newmat[1];   // �� �ٲ�
        count += 0.5f;    // ī��Ʈ ����(�浹 2�� �߻��ϹǷ� 0.5��
        if (count > 30)    // 30 �̻��̸�
            count = 30; // 30����

        Invoke("Return", 5.0f); // 5�� �ڿ� ȣ��
    }

    void Return()   // �� ���ƿ��� �Լ�
    {
        gameObject.GetComponent<MeshRenderer>().material = newmat[0];// ������
        count -= 0.5f;    // ī��Ʈ ����
    }


}
