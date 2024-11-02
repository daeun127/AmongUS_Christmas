using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISetting : MonoBehaviour
{
    public Material[] mat;  // 매테리얼 배열 저장

    public GameObject santa;    // 산타 오브젝트
    public GameObject deer;     // 사슴뿔 오브젝트

    private void Start()    // 시작
    {
        int setColor = Random.Range(0, 3);  // 색깔 랜덤 설정
        set_skinned_mat(0, mat[setColor]);  // 스킨 색 결정

        if (Random.Range(0, 2) == 0)    // 랜덤으로
        {
            santa.SetActive(false);     // 산타 오브젝트 끄기
        }
        if (Random.Range(0, 2) == 0)    // 랜덤으로 
        {
            deer.SetActive(false);      // 사슴 뿔 오브젝트 끄기
        }

    }

    void set_skinned_mat(int num, Material Mat)     // 색 결정
    {

        SkinnedMeshRenderer renderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();    // 스킨 렌더러 변수

        Material[] mats = renderer.materials;   // 매테리얼을 배열에 저장

        mats[num] = Mat;    // 설정 색이 들어오면 바꿔치지

        renderer.materials = mats; //바꿔서 다시 저장
    }
}
