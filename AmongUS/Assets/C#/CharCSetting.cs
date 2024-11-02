using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCSetting : MonoBehaviour
{
    public Material[] mat;  // ���� ���׸����� �迭
    
    public void OnclickBlBtn()  // �Ķ��� ��ư
    {
        set_skinned_mat(0, mat[0]); // �Ķ������� ����
    }

    public void OnclickGBtn()   // �ʷϻ� ��ư
    {
        set_skinned_mat(0, mat[1]); // �ʷϻ����� ����
    }

    public void OnclickBrBtn()  // ���� ��ư
    {
        set_skinned_mat(0, mat[2]); // ���� ����
    }

    void set_skinned_mat(int num, Material Mat) // ��Ų �÷� ���� �Լ�
    {

        SkinnedMeshRenderer renderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();    // ��Ų ������ ����

        Material[] mats = renderer.materials;   // ���׸����� �迭�� ����

        mats[num] = Mat;    // ���� ���� ������ �ٲ�ġ��

        renderer.materials = mats; //�ٲ㼭 �ٽ� ����
    }

}
