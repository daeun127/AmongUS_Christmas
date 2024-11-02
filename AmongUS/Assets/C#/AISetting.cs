using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISetting : MonoBehaviour
{
    public Material[] mat;  // ���׸��� �迭 ����

    public GameObject santa;    // ��Ÿ ������Ʈ
    public GameObject deer;     // �罿�� ������Ʈ

    private void Start()    // ����
    {
        int setColor = Random.Range(0, 3);  // ���� ���� ����
        set_skinned_mat(0, mat[setColor]);  // ��Ų �� ����

        if (Random.Range(0, 2) == 0)    // ��������
        {
            santa.SetActive(false);     // ��Ÿ ������Ʈ ����
        }
        if (Random.Range(0, 2) == 0)    // �������� 
        {
            deer.SetActive(false);      // �罿 �� ������Ʈ ����
        }

    }

    void set_skinned_mat(int num, Material Mat)     // �� ����
    {

        SkinnedMeshRenderer renderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();    // ��Ų ������ ����

        Material[] mats = renderer.materials;   // ���׸����� �迭�� ����

        mats[num] = Mat;    // ���� ���� ������ �ٲ�ġ��

        renderer.materials = mats; //�ٲ㼭 �ٽ� ����
    }
}
