using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AIimpo : MonoBehaviour
{
    AudioSource _audio;   // 오디오 소스
    public AudioClip killsound; // 메인 bgm

    public enum State       // 상태
    {
        PATROL, // 순찰
        TRACE,  // 추적
        KILL    // 죽임
    }   

    public State state = State.PATROL;  // 기본은 순찰
    public float traceDist = 5.0f; // 추적 가능 거리
    Transform playerTr; // 플레이어위치


    GameObject[] crew;  // 크루들 저장 배열
    List<float> allDist = new List<float>(4) { 0, 0, 0, 0 };    // 크루와의 거리 리스트
    public int crewnum; // 가장 가까이 있는 crew 번호 저장 변수

    public int count = 0;  // 카운트 0

    WaitForSeconds ws;  // 대기 시간 변수
    MoveAgent moveAgent;  // moveagent 스크립트의 변수
    Animation anim;     //Animation 변수

    private void Awake()    // 시작 전에
    {
        _audio = GetComponent<AudioSource>();   // 오디오 찾기

        GameObject player = GameObject.Find("US");
        if (player != null) // 플레이어 찾으면
        {
            playerTr = player.transform;    // 플레이어 위치 찾아서 변수 할당
        }
        moveAgent = GetComponent<MoveAgent>();	// moveagent 찾기
        anim = GetComponent<Animation>(); // Animation 변수에 할당
        
        ws = new WaitForSeconds(0.3f);	// 0.3초로 지정
    }

    private void OnEnable() // 산발적으로 호출
    {
        StartCoroutine(CheckState());   //상태 체크
        StartCoroutine(Action());   //행동
    }

    IEnumerator CheckState()   //상태 체크
    {
        while (true) // 계속
        {
            crew = GameObject.FindGameObjectsWithTag("crew"); // 하이어라키 창에서 크루들 찾아서 배열에 저장
            if (crew != null)   // 크루가 있으면
            {
                allDist[0] = Vector3.Distance(playerTr.position, transform.position);   // 배열 0번째는 플레이어와 임포스터의 거리 차

                for (int i = 1; i <= crew.Length; i++)  // 크루 개수 만큼
                {
                    allDist[i] = Vector3.Distance(crew[i - 1].transform.position, transform.position);  // 배열에 저장
                }
                crewnum = allDist.IndexOf(allDist.Min());   // 임포스터와 가장 가까이있는 크루의 배열 번호  
            }

            if (allDist.Min() <= traceDist) // 가장 가까이 있는 크루와의 거리가 추적 가능 거리 안일 때 
                state = State.TRACE;    // 추적
            else// 밖이면
                state = State.PATROL;   // 순찰


            yield return ws;    // 대기
        }
    }
    private void OnTriggerStay(Collider other) // 부딪힌 동안
    {
        if (other.tag == "Player")  // 플레이어 크루와 부딪힘
        {
            count = 0;  // 카운트 0
            for (int i = 1; i < allDist.Count; i++) // 크루거리 저장 리스트 개수까지
            {
                if (allDist[i] > traceDist)    // 다른 크루들이 사정 거리 밖일 때
                {
                    ++count;    // 카운트 증가

                    if (count >= (crew.Length)-1) // 다 밖이면
                    {
                        _audio.PlayOneShot(killsound, 1.0f);    // 죽는 소리 재생
                        Invoke("Defeat", 1.5f); // 1.5초 뒤 씬 전환
                    }
                    else// 아니면
                    {
                        state = State.PATROL;   // 순찰 상태 변환
                        break; // 반복 종료
                    }
                }
            }
            state = State.PATROL;   // 순찰 상태 변환

        }
        else if (other.tag == "crew")   // 부딪힌 게 크루일 때
        {
            count = 0;  // 카운트 0
            for (int i = 1; i < allDist.Count; i++)  // 크루거리 저장 리스트 개수까지 
            {
                if (crewnum == i) // 만약 크루 넘버가 저장된 변수 넘버와 같으면
                    continue;   // 건너뛰고
                if (allDist[i] > traceDist) // 나머지 크루들이 사정 거리 밖일 때
                {
                    ++count;    // 카운트 증가
                }
            }

            if (count >= (crew.Length) - 1) // 현재 생존 중인 크루가 일정 거리 밖이면
            {
                anim.CrossFade("IDLE", 1f);  // 이동 애니메이션
                crew[crewnum - 1].GetComponent<AIcrew>().state = AIcrew.State.DIE;  // 죽음
                crew[crewnum - 1].tag = "notme";    //  추적 대상에서 제외
                _audio.PlayOneShot(killsound, 1.0f);    // 죽는 소리 재생
            }
            else//아니면
            {
                state = State.PATROL;   // 순찰 상태 변환
            }
        }
    }

    IEnumerator Action() // 행동
    {
        while (true)    // 계속
        {
            yield return ws;    // 대기

            switch (state)  // 상태별로
            {
                case State.PATROL:  // 순찰
                    moveAgent.SetPatrolling(true);  // 순찰 ok
                    anim.CrossFade("RUN", 1f);  // 이동 애니메이션
                    break;  // 끝

                case State.TRACE:   // 추적
                    anim.CrossFade("RUN", 1f);  // 이동 애니메이션
                    if (crewnum != 0 && crew[crewnum - 1] != null)   // 크루가 플레이어가 아니고 죽기전이면
                    {
                        moveAgent.SetTraceTarget(crew[crewnum - 1].transform.position);   // 해당 크루 추적
                    }
                    else if (crewnum == 0) //플레이어이면
                        moveAgent.SetTraceTarget(playerTr.position);    // 플레이어 추적
                    else// 아니면
                        state = State.PATROL;   // 순찰 상태 변환
                    break;  // 끝
            }
        }
    }

    void Defeat()   // 씬 전환
    {
        SceneManager.LoadScene("Defeat"); //씬 전환
    }
}
