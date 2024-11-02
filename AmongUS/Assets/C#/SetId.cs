using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetId : MonoBehaviour
{
    static public List<string> ids = new List<string>() { "맥모닝불여일견", "반지하의제왕", "헨젤과그랬대", "오즈의맙소사", "하울의음쥑이는성", "모르는개산책" };   // id 리스트
    TextMesh idtext;    // id 텍스트

    void Start()
    {
        idtext = GetComponent<TextMesh>();  // text 속성 불러오기
        int i = Random.Range(0, ids.Count);    // 랜덤 번호로
        idtext.text = ids[i];     // text 값 변경
        ids.RemoveAt(i);    // 쓴 값 삭제
    }
}
