using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCSetting : MonoBehaviour
{
    public Material[] mat;  // 현재 매테리얼의 배열
    
    public void OnclickBlBtn()  // 파란색 버튼
    {
        set_skinned_mat(0, mat[0]); // 파란색으로 설정
    }

    public void OnclickGBtn()   // 초록색 버튼
    {
        set_skinned_mat(0, mat[1]); // 초록색으로 설정
    }

    public void OnclickBrBtn()  // 갈색 버튼
    {
        set_skinned_mat(0, mat[2]); // 갈색 설정
    }

    void set_skinned_mat(int num, Material Mat) // 스킨 컬러 설정 함수
    {

        SkinnedMeshRenderer renderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();    // 스킨 렌더러 변수

        Material[] mats = renderer.materials;   // 매테리얼을 배열에 저장

        mats[num] = Mat;    // 설정 색이 들어오면 바꿔치지

        renderer.materials = mats; //바꿔서 다시 저장
    }

}
