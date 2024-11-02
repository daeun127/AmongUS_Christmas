using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AIcrew : MonoBehaviour
{
	public enum State	// 상태
	{
		PATROL,	// 순찰
		TRACE,	// 추적
		DIE	// 죽음
	}

	AudioSource _audio;   // 오디오 소스
	public AudioClip reportsound; // 메인 bgm

	public State state = State.PATROL;	// 원래 상태 순찰
	public float traceDist = 8.0f;	// 10f 이내 추적
	GameObject player;	// 플레이어 오브젝트
	Transform playerTr;	// 플레이어 위치

	static public int dieCount = 0;   // crew 죽인 횟수

	WaitForSeconds ws;	// 대기 시간
	MoveAgent moveAgent;	// moveagent 스크립트의 변수
	Animation anim;     //Animation 변수


	private void Awake()	// 시작 전에
	{
		player = GameObject.Find("US");	// 플레이어 찾기
		if (player != null)	// 플레이어 찾으면
		{
			playerTr = player.transform;	// 플레이어 위치 찾아서 변수 할당
		}
		
		moveAgent = GetComponent<MoveAgent>();	// moveagent 찾기
		anim = GetComponent<Animation>(); // Animation 찾기
		_audio = GetComponent<AudioSource>();	// 오디오 찾기

		ws = new WaitForSeconds(0.3f);	// 0.3초로 지정
	}

	private void OnEnable()	// 산발적으로 호출
	{
		StartCoroutine(CheckState());	//상태 체크
		StartCoroutine(Action());	//행동
	}

	IEnumerator CheckState()	//상태체크
	{
		while (state != State.DIE)	// 죽은 상태 아닌 동안
		{
			if (Vector3.Distance(playerTr.position, transform.position) <= traceDist)   // 거리 내 추적
			{
				state = State.TRACE;    // 추적 상태
			}
			else
			{
				state = State.PATROL;   // 순찰
			}

			if (player.tag == "PlayerImpo")	// 플레이어가 임포스터일때
			{
				if (GameManager.dienum < 5)
				{
					if (Vector3.Distance(PlayerCtrl.crew[GameManager.dienum].transform.position, transform.position) <= traceDist)  // 죽은 애가 가까이 있으면
					{
						state = State.TRACE;    // 추적 상태
					}
				}
			}

			yield return ws;	// 대기
		}
	}

	IEnumerator Action()	// 행동
	{
		while (true)	// 계속
		{
			yield return ws;	// 대기

			switch (state) //상태별로
			{
				case State.PATROL:	// 순찰 상태
					moveAgent.SetPatrolling(true);	//순찰 ok
					anim.CrossFade("RUN", 1f);  // 이동 애니메이션
					break;	// 끝

				case State.TRACE:	// 추적 상태
					anim.CrossFade("RUN", 1f);  // 이동 애니메이션
					if(GameManager.dienum < 5)	// 죽은 크루가 있고
                    {
						if (Vector3.Distance(PlayerCtrl.crew[GameManager.dienum].transform.position, transform.position) <= 20f)  // 죽은 애가 가까이 있으면
						{
							moveAgent.SetTraceTarget(PlayerCtrl.crew[GameManager.dienum].transform.position); // 죽은애 추적
						}
					}
					else//아니면
                    {
						moveAgent.SetTraceTarget(playerTr.position); // 플레이어 추적
                    }
					break; // 끝

				case State.DIE: // 죽은 상태
					anim.CrossFade("IDLE", 1f);  // idle 애니메이션
					moveAgent.Stop(); // 멈추기
					OnPlayerDie();	// 죽었을 때 함수 호출
					break;	// 반복의 끝
			}
		}
	}

	public void OnPlayerDie()	// 죽었을 때
	{
		++dieCount; // 죽은 크루 수 증가
		StopAllCoroutines();	//모든 코루틴 멈추기

		if (dieCount >= 3 && player.tag == "Player")	// 3명이상 죽고 플레이어가 크루
			SceneManager.LoadScene("Defeat");	// 실패
		else if (dieCount >= 3 && player.tag == "PlayerImpo")	// 3명이상 죽고 플레이어가 임포스터일 때
			SceneManager.LoadScene("Victory");	// 승리
	}

	private void OnTriggerEnter(Collider other)	// 충돌 시
    {
		if(player.tag == "PlayerImpo" && other.tag == "crew")	// 플레이어가 임포스터이면서 충돌한 오브젝트가 크루일 때
        {
			if (other.GetComponent<AIcrew>().state == State.DIE)	// 충돌한 크루가 죽었을 때
            {
				GameObject.Find("Canvas").transform.Find("Report_ui").gameObject.SetActive(true);	// 시체 리포트
				_audio.PlayOneShot(reportsound, 1.0f);  // 리포트 소리 재생
				Invoke("ReportSc", 2.0f);	// 2초 후에 호출
            }
		}
	}

	void ReportSc()
    {
		SceneManager.LoadScene("Report"); // 씬 전환
	}
}
