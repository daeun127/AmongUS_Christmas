using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    AudioSource _audio;   // 오디오 소스
    public AudioClip startsound; // start bgm
    public AudioClip votesound; // start bgm

    public GameObject us;   // 플레이어 오브젝트
    public GameObject impo; // 적 오브젝트
    public GameObject crew; // 크루 오브젝트
    GameObject map; // 지도 버튼
    GameObject report;  // 리포트 버튼

    public Text TaskText;   // 미션 횟수 표시
    float task1count = 0;    // 미션1 횟수
    float task2count = 0;    // 미션2 횟수
    float task3count = 0;    // 미션3 횟수

    public Image TaskBar;   // 할일 가시화
    public GameObject txtpref; // report 버튼 텍스트
    GameObject txtinst;    // 생성할 버튼 text

    GameObject[] crewtext;    // 자동으로 생성될 크루 배열
    static public string impoText;    // impo id 문자열
    static public List<string> textList = new List<string>(); // text 문자열만 모은 리스트

    static public List<bool> dieList = new List<bool>();
    static public int dienum = 5;   // 죽은 크루 넘버
    static public List<float> cdist = new List<float>();    // 자동 생성 크루와의 거리
    private void Start()// 시작하면서
    {
        _audio = GetComponent<AudioSource>();   // 오디오 찾기
    }
    public void OnClickCompleteBtn()    // 완료 버튼을 누르면
    {
        _audio.PlayOneShot(startsound, 1.0f);   // 시작 소리 재생

        us.transform.position = new Vector3(0, 0, 0);   // 플레이어 위치는 0,0,0

        GameObject.Find("Before").gameObject.SetActive(false);  // 게임전 ui 끄기
        GameObject.Find("US").transform.Find("Camera").gameObject.SetActive(true);  //  플레이어 카메라 켜기
        GameObject.Find("Camera (1)").gameObject.SetActive(false);  // 썼던 카메라 끄기


        GameObject.Find("Canvas").transform.Find("Taskgage").gameObject.SetActive(true);    // 할일 바 ui 켜키

        if (Random.Range(0, 2) == 0)    // 50:50의 확률로
        {
            GameObject.Find("Canvas").transform.Find("Kill_btn").gameObject.SetActive(true);    // 킬 버튼 켜기
            GameObject.Find("Canvas").transform.Find("im").gameObject.SetActive(true);  //임포스터ui 켜기

            us.tag = "PlayerImpo";  // 플레이어가 임포스터
            Invoke("imDelay", 2.0f);    // 2초후 호출
        }
        else//아니면
        {
            GameObject.Find("Canvas").transform.Find("TaskList").gameObject.SetActive(true);    // 할일 목록 켜기
            GameObject.Find("Canvas").transform.Find("Report_btn").gameObject.SetActive(true);  // 리포트 버튼 켜기
            GameObject.Find("Canvas").transform.Find("crew").gameObject.SetActive(true);    // 크루 ui 켜기

            us.tag = "Player";  // 플레이어는 크루원
            Invoke("crewDelay", 2.0f);  // 2초후 호출
        }
    }

    private void imDelay()  //임포스터일때
    {
        GameObject.Find("im").gameObject.SetActive(false);  // 패널 끄기

        for (int i = 0; i < 4; i++) // 크루원 수 만큼
        {
            Vector3 randomPosition = new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15));  // 랜덤 위치에
            Instantiate(crew, randomPosition, Quaternion.identity); // 크루 4명 생성
        }

        StartCoroutine(crewDistList()); // 크루와의 거리 ui 시작
    }
    private void crewDelay()    // 크루원일때
    {
        GameObject.Find("crew").gameObject.SetActive(false);    // 패널 끄기

        Instantiate(impo);  // 임포스터 하나 생성

        for (int i = 0; i < 3; i++) // 크루원 수만큼
        {
            Vector3 randomPosition = new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15));  // 랜덤 위치에
            Instantiate(crew, randomPosition, Quaternion.identity); // 크루원 3명 생성
        }
    }

    IEnumerator crewDistList()  // 크루와의 거리 재기
    {
        while (true)    // 계속
        {
            yield return new WaitForSeconds(1f);    //1초 쉬고

            if (crew != null)   // 크루원이 있으면
            {
                dieList = new List<bool>{    // 죽은 상태 리스트
            PlayerCtrl.crew[0].GetComponent<AIcrew>().state == AIcrew.State.DIE,    // 크루가 죽었으면 true, 살아있으면 false
            PlayerCtrl.crew[1].GetComponent<AIcrew>().state == AIcrew.State.DIE,    // 크루가 죽었으면 true, 살아있으면 false
            PlayerCtrl.crew[2].GetComponent<AIcrew>().state == AIcrew.State.DIE,    // 크루가 죽었으면 true, 살아있으면 false
            PlayerCtrl.crew[3].GetComponent<AIcrew>().state == AIcrew.State.DIE     // 크루가 죽었으면 true, 살아있으면 false
            };
            }

            if (dieList[0] == true) // 0번째가 죽음
            {
                dienum = 0; // 죽은 크루는 0번
            }
            if (dieList[1] == true)  // 1번째가 죽음
            {
                dienum = 1;  // 죽은 크루는 1번
            }
            if (dieList[2] == true) // 2번째가 죽음
            {
                dienum = 2;  // 죽은 크루는 2번
            }
            if (dieList[3] == true) // 3번째가 죽음
            {
                dienum = 3;  // 죽은 크루는 3번
            }

            yield return new WaitForSeconds(0.3f);  // 0.3초 대기
        }
    }



    public void OnClickMap()    // map 버튼 클릭하면
    {
        map = GameObject.Find("Map_btn").transform.GetChild(0).gameObject;  // 버튼 아래 자식 오브젝트 찾아서
        if (map.activeSelf == false)    // 꺼져있으면
        {
            map.SetActive(true);    // 켜고
        }
        else//켜져있으면
        {
            map.SetActive(false);//끈다
        }
    }

    public void OnClickReport() // report 버튼 누름 == 플레이어는 크루원
    {
        _audio.PlayOneShot(votesound, 1.0f);//report 소리 재생

        report = GameObject.Find("Report_btn").transform.GetChild(0).gameObject;    //리포트ui 찾기

        impoText = GameObject.FindGameObjectWithTag("impoID").GetComponent<TextMesh>().text;   // impo id 문자열 저장
        crewtext = GameObject.FindGameObjectsWithTag("crewID");   // crew id 문자열 저장

        for (int j = 0; j < crewtext.Length; j++) // 크루원 수만큼
        {
            textList.Add(crewtext[j].GetComponent<TextMesh>().text);  // 문자열 list에 크루 id 추가
        }

        textList.Add(impoText); // impo id 문자열 list에 추가 저장

        if (report.activeSelf == false) //리포트ui가 꺼져있으면
        {
            report.SetActive(true); // ui켜기

            for (int i = 0; i < 4; i++) // 크루원 3명 + 임포스터 1명
            {
                txtinst = Instantiate(txtpref, new Vector3(50, 0, 0), Quaternion.identity); // 아이디 텍스트 생성
                txtinst.transform.SetParent(GameObject.Find("Button (" + i + ")").transform, false);    // 부모는 각자 생성된 위치의 버튼으로 설정
            }
        }
    }

    public void AddTask1()  // 할일 1
    {
        if (task1count < 4) // 카운트 4가 아니면
        {
            ++task1count; //카운트 증가
            SetTaskText();  // 할 일 text 변경
            FillTaskBar();  // 할 일 바 색 채우기
        }
    }

    public void AddTask2()  // 할일 2
    {
        if (task2count < 4) // 카운트 4가 아니면
        {
            ++task2count; //카운트 증가
            SetTaskText();  // 할 일 text 변경
            FillTaskBar();  // 할 일 바 색 채우기
        }
    }

    public void AddTask3()  // 할일 3
    {
        if (task3count < 2) // 카운트 2가 아니면
        {
            ++task3count; //카운트 증가
            SetTaskText();  // 할 일 text 변경
            FillTaskBar();  // 할 일 바 색 채우기
        }
    }

    public void SetTaskText()   // 할일 텍스트 변경
    {
        TaskText.text = "어린이들 선물 포장하기(" + task1count + "/4)\n지구로 편지 보내기(" + task2count + "/4)\n징글벨 신호 울리기(" + task3count + "/2)"; //카운트 대로
    }

    public void FillTaskBar()   // 할 일 바 채우기
    {
        TaskBar.fillAmount += 0.1f; // 할일을 할 때마다 0.1씩

        if (TaskBar.fillAmount >= 1.0f)    //1이 넘으면
        {
            if(us.tag == "Player")
                SceneManager.LoadScene("Victory");  // 승리
            else if(us.tag == "PlayerImpo")
                SceneManager.LoadScene("Defeat");  // 패배
        }
    }

}
