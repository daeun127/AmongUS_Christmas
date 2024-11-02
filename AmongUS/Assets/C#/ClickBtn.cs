using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickBtn : MonoBehaviour
{
    public void OnClickBtn()    // 버튼을 누르면
    {
        if(transform.GetChild(0).GetComponent<Text>().text == GameManager.impoText) //버튼의 자식 오브젝트의 텍스트가 임포스터 텍스트와 일치하면
        {
            SceneManager.LoadScene("Victory");  // 성공
        }
        else
        {
            SceneManager.LoadScene("Defeat");   // 실패
        }
    }

    public void OnclickHtPBtn() // 게임 방법 버튼 누르면
    {
        GameObject ui = GameObject.Find("HTP").transform.Find("HTPui").gameObject;  // ui 찾아서

        if (ui.activeSelf == false)  // 꺼지면
            ui.SetActive(true); //켜고
        else//켜지면
            ui.SetActive(false);//끈다
    }

    public void OnclickPlayBtn()    // pl
    {
        print("전환");
    }

}
