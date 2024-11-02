using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBtnText : MonoBehaviour
{
    Text btnText;   // 버튼 텍스트

    void Start()
    {
        btnText = GetComponent<Text>(); // 텍스트 속성 저장

        int i = Random.Range(0, GameManager.textList.Count);    // 랜덤 번호로
        btnText.text = GameManager.textList[i];     // btntext 값 변경
        GameManager.textList.RemoveAt(i);    // 쓴 값 삭제
    }
}