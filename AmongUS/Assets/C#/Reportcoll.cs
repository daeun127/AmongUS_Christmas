using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reportcoll : MonoBehaviour
{
    public Material[] newmat = new Material[2]; // 매테리얼 2개 배열 저장
    static public float count = 0;    // 색깔 바꾼 거 카운트 수

    private void OnTriggerEnter(Collider other) // 부딪히면
    {
        gameObject.GetComponent<MeshRenderer>().material = newmat[1];   // 색 바꿈
        count += 0.5f;    // 카운트 증가(충돌 2번 발생하므로 0.5씩
        if (count > 30)    // 30 이상이면
            count = 30; // 30으로

        Invoke("Return", 5.0f); // 5초 뒤에 호출
    }

    void Return()   // 색 돌아오는 함수
    {
        gameObject.GetComponent<MeshRenderer>().material = newmat[0];// 기존색
        count -= 0.5f;    // 카운트 감소
    }


}
