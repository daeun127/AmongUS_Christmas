using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    AudioSource _audio;   // 오디오 소스
    public AudioClip killsound; // 죽이는 bgm

    public float moveSpeed = 10.0f;     // 이동 속도 변수
    public float rotSpeed = 80.0f;      // 회전 속도 변수
    Animation anim;     //Animation 변수

    public float killDist = 5.0f;   // 죽임 거리

    public List<float> allDist = new List<float>(); // 거리차이 저장 리스트

    public static GameObject[] crew;    // crew 오브젝트 배열
    public Button killBtn;  // 죽임 버튼
    public int crewnum; // 가장 가까이 있는 crew 번호 저장 변수

    private void Start()    // 시작하면
    {
        _audio = GetComponent<AudioSource>();   // 오디오 찾기

        anim = GetComponent<Animation>(); // Animation 변수에 할당
    }

    void Update()   // 계속
    {
        float h = Input.GetAxis("Horizontal"); // 좌우 이동
        float v = Input.GetAxis("Vertical"); // 전후 이동
        float r = Input.GetAxis("Mouse X"); //회전

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);  // 전후좌우 이동 방향 벡터 계산
        transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);   // movespeed 속도로 이동
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);       // rotspeed 속도로 회전

        if (v >= 0.1f || v <= -0.1f || h >= 0.1f || h <= -0.1f) // 전후 좌우 이동할 때
        {
            anim.CrossFade("RUN", 1f);  // 이동 애니메이션
        }
        else
        {
            anim.CrossFade("IDLE", 1f); // 정지 애니메이션
        }
    }

    private void OnEnable() // 산발적으로 호출
    {
        StartCoroutine(CheckState());   // 상태 체크
    }

    IEnumerator CheckState()    // 상태 체크
    {
        while (true)    // 계속
        {
            yield return new WaitForSeconds(1f);    // 1초 대기

            if (this.tag == "PlayerImpo")   // 플레이어가 임포스터면
            {
                crew = GameObject.FindGameObjectsWithTag("crew");   // 크루들을 찾아서

                for (int i = 0; i < crew.Length; i++)   // 개수만큼
                {
                    allDist[i] = Vector3.Distance(crew[i].transform.position, transform.position);  // 거리 계산
                }
            }
            yield return new WaitForSeconds(0.3f);  // 대기
        }

    }
    public void OnClickKillBtn() // 죽였을 때
    {
        crewnum = allDist.IndexOf(allDist.Min());   // 가장 가까이 있는 크루의 넘버 저장

        if (crew[crewnum].GetComponent<AIcrew>().state != AIcrew.State.DIE) // 가장 가까이 있는 크루가 안죽었으면
        {
            for (int i = 0; i < allDist.Count; i++)  // 크루 개수만큼
            {
                if (crewnum == i)   // 가장 근접 크루면
                    continue;   // 넘어가기
                if (allDist[i] <= 15f)  // 다른 크루들이 일정 거리 안이면
                {
                    SceneManager.LoadScene("Defeat");   // 실패
                }
            }

            _audio.PlayOneShot(killsound, 1.0f);    // 죽임
            crew[crewnum].GetComponent<AIcrew>().state = AIcrew.State.DIE;  // 죽은 상태로 바꿈
        }

    }
}
