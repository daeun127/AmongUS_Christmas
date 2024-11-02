using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reportpre : MonoBehaviour
{
    public GameObject prefabs; //찍어낼 게임 오브젝트

    private BoxCollider area;    //박스콜라이더의 사이즈
    public int count = 30;      //찍어낼 게임 오브젝트 갯수

    void Start()    // 시작하면
    {
        area = GetComponent<BoxCollider>(); // 박스 콜라이더

        for (int i = 0; i < count; ++i)//count 수 만큼 생성
        {
            Spawn();//생성
        }

        area.enabled = false;   // 박스콜라이더 사용 불가
    }

    private Vector3 GetRandomPosition() // 랜덤 위치
    {
        Vector3 basePosition = transform.position;  // 현재 위치 (0,0,0) 기점
        Vector3 size = area.size;   // 박스콜라이더 사이즈

        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);  // 랜덤 값
        float posZ = basePosition.y + Random.Range(-size.z / 2f, size.z / 2f);  // 랜덤 값

        Vector3 spawnPos = new Vector3(posX, 0.5f, posZ);  // 랜덤 위치 설정

        return spawnPos;    // 랜덤 위치 반환
    }

    private void Spawn()    // 생성
    {
        Vector3 spawnPos = GetRandomPosition(); //랜덤위치함수

        GameObject instance = Instantiate(prefabs, spawnPos, Quaternion.identity);  // 생성
    }

}
