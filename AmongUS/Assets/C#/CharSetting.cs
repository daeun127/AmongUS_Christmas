using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSetting : MonoBehaviour
{
    private bool state_s = false;   // 산타 켜짐 or 꺼짐
    private bool state_d = false;   // 사슴뿔 켜짐 or 꺼짐
    public GameObject santa;    // 산타 오브젝트
    public GameObject deer; // 사슴뿔 오브젝트

    public void OnclickSBtn()   // 산타 버튼 누르면
    {
        if (state_s == false)   // 꺼져있으면
        {
            santa.SetActive(true);  // 오브젝트 켬
            state_s = true; // 켠 상태
        }
        else
        {
            santa.SetActive(false); // 오브젝트 끔
            state_s = false;    // 끈 상태
        }
    }

    public void OnclickDBtn()   // 뿔 버튼 누르면
    {
        if (state_d == false)   // 꺼져있으면
        {
            deer.SetActive(true);      // 오브젝트 켬
            state_d = true; // 켠 상태
        }
        else
        {
            deer.SetActive(false); // 오브젝트 끔
            state_d = false;    // 끈 상태
        }
    }
}
