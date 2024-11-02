using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reportpre : MonoBehaviour
{
    public GameObject prefabs; //�� ���� ������Ʈ

    private BoxCollider area;    //�ڽ��ݶ��̴��� ������
    public int count = 30;      //�� ���� ������Ʈ ����

    void Start()    // �����ϸ�
    {
        area = GetComponent<BoxCollider>(); // �ڽ� �ݶ��̴�

        for (int i = 0; i < count; ++i)//count �� ��ŭ ����
        {
            Spawn();//����
        }

        area.enabled = false;   // �ڽ��ݶ��̴� ��� �Ұ�
    }

    private Vector3 GetRandomPosition() // ���� ��ġ
    {
        Vector3 basePosition = transform.position;  // ���� ��ġ (0,0,0) ����
        Vector3 size = area.size;   // �ڽ��ݶ��̴� ������

        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);  // ���� ��
        float posZ = basePosition.y + Random.Range(-size.z / 2f, size.z / 2f);  // ���� ��

        Vector3 spawnPos = new Vector3(posX, 0.5f, posZ);  // ���� ��ġ ����

        return spawnPos;    // ���� ��ġ ��ȯ
    }

    private void Spawn()    // ����
    {
        Vector3 spawnPos = GetRandomPosition(); //������ġ�Լ�

        GameObject instance = Instantiate(prefabs, spawnPos, Quaternion.identity);  // ����
    }

}
