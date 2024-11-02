using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // 씬 바꿀 때
using UnityEngine.UI;   // 텍스트

public class ReportTimer : MonoBehaviour
{
    public float sec = 30; // 시간 30초

    public Text timer;  // 타이머 텍스트
    Color initiClor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);  // 텍스트 시작색 == 흰색
    Color currColor = new Vector4(1.0f, 0, 0, 1.0f);    // 텍스트 현재 색

    GameObject htpui;   // 플레이 방법 ui 패널

    private void Start()    // 시작하면
    {
        timer.color = initiClor;    // 타이머 텍스트 흰색
        htpui = GameObject.Find("htpui");   // htpui 찾기
    }

    public void OnClickPlay()   // 플레이 버튼 누르면
    {
        htpui.SetActive(false); // htpui 끄기
    }

    void Update()   // 매 프레임 마다
    {
        if(htpui.activeSelf == false)   // htpui가 꺼져있을 때만
        {
            sec -= Time.deltaTime;  // 타이머 작동
            timer.text = string.Format("{0:D2}:{1:D2}", 0, (int)sec);   // 출력 형식 바꾸고 타이머 텍스트 출력

            if (sec < 5)    // 5초 카운트
            {
                timer.color = currColor;    // 텍스트 빨간색
            }

            if (sec < 0)    // 0초 
            {
                if (Reportcoll.count > 10)  // 10개 이상 색깔 바꿨으면 
                    SceneManager.LoadScene("Victory");  // 성공
                else
                    SceneManager.LoadScene("Defeat");   // 실패
            }
        }
    }
}
