using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollPresent : MonoBehaviour
{
    public GameObject present; // 선물 오브젝트

    private void OnTriggerEnter(Collider other) //부딪히면
    {
        if (other.tag == "crew" || other.tag == "Player")   // 태그가 크루거나 플레이어가 크루일 때
        {
            present.transform.rotation = Quaternion.Euler(0, 0, 0); // 회전 각도 000으로 세팅
            GameObject.Find("GameManager").GetComponent<GameManager>().AddTask1();  // 미션1 카운트
        }
    }
}
