using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollBell : MonoBehaviour
{
    AudioSource _audio;   // 오디오 소스
    public AudioClip bellsound; // 메인 bgm

    public GameObject bell; // 벨 오브젝트
    public float angle = 20f;   // 각도

    private float lerpTime = 0; // sin 함수로 시작
    private float speed = 3f;   // 속도

    private void Start()    // 시작하면
    {
        _audio = GetComponent<AudioSource>();   // 오디오 가져오기
    }
    private void OnTriggerStay(Collider other)  // 부딪히고 있을 때
    {
        if (other.tag == "crew" || other.tag == "Player")  // 부딪힌 게 태그가 크루거나 플레이어가 크루일 때
        {
            if(_audio.isPlaying == false)
                _audio.PlayOneShot(bellsound, 1f);    // 소리 출력
            lerpTime += Time.deltaTime * speed; // 속도 * 시간 만큼 재생
            bell.transform.rotation = Rotate(); // 회전
            GameObject.Find("GameManager").GetComponent<GameManager>().AddTask3();  // 미션3 카운트
        }
    }

    private void OnTriggerExit(Collider other)  // 빠져나오면
    {
        _audio.Stop(); // 소리 끄기
    }

    Quaternion Rotate() // 회전
    {
        return Quaternion.Lerp(Quaternion.Euler(Vector3.forward * angle),Quaternion.Euler(Vector3.back * angle), GetLerpTParam()); //시작 각도와 마지막 각도 선형 계산 하여 부드럽게 회전하도록
    }

    float GetLerpTParam()   // 
    {
        return (Mathf.Sin(lerpTime)+1) * 0.5f;  // lerpTime 시간 +1 만큼 동안 반복 운동
    }

}
