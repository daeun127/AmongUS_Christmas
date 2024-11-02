using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // 씬 전환

public class ManageS : MonoBehaviour
{
    static public void OnClickStartBtn()   // 시작 버튼을 누르면
    {
        SceneManager.LoadScene("AmongUs");  // 씬 로드

        SetId.ids = new List<string>() { "맥모닝불여일견", "반지하의제왕", "헨젤과그랬대", "오즈의맙소사", "하울의음쥑이는성", "모르는개산책" };   // id 리스트
        GameManager.dieList = null; // 크루 죽었는지 살았는지 배열
        GameManager.cdist = null;    // 자동 생성 크루와의 거리
        GameManager.textList = new List<string>(); // text 문자열만 모은 리스트
        GameManager.impoText = "";  // 임포 아이디 저장
        GameManager.dienum = 5;   // 죽은 크루 넘버
        PlayerCtrl.crew = null;     // 하이어라키 크루 수
        AIcrew.dieCount = 0;   // crew 죽인 횟수
        Reportcoll.count = 0;   // 매테리얼 바꾼 횟수 순
    }

    public void OnClickHTPBtn()   // 게임방법 버튼을 누르면
    {
        GameObject.Find("Canvas").transform.Find("htpui").gameObject.SetActive(true);   // 하이어라키 창에서 패널 찾아서 켜기
    }

    public void OnClickCloseBtn()   // 닫기 버튼 누르면
    {
        GameObject.Find("htpui").gameObject.SetActive(false);   // 패널 끄기
    }

    public void OnClickQuitBtn()   // 끝내기 버튼을 누르면
    {
        SceneManager.LoadScene("Start");  // 씬 로드
    }
}
