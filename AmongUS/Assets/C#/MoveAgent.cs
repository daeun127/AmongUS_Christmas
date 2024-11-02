using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   // NavMeshAgent을 위한

public class MoveAgent : MonoBehaviour
{
    public List<Transform> wayPoints;   // 순찰 지점 저장
    public int nextIdx;     // 다음 순찰 지점의 배열

    float patrolSpeed = 4.0f;   // 순찰속도
    float traceSpeed = 7.0f;    // 추적속도

    NavMeshAgent agent; // 내비게이션
    bool patrolling;    // 순찰 중인가
    Vector3 traceTarget;    // 타겟 위치

    public GameObject id;  // id

    public void SetPatrolling(bool patrol)  // 순찰중 설정
    {
        patrolling = patrol;    // 순찰 중으로
        agent.speed = patrolSpeed;  //순찰 속도로
        agent.angularSpeed = 120;   // 각 속도 120
        MoveWayPoint(); // 순찰 포인트
    }

    public void SetTraceTarget(Vector3 pos) // 추적 타깃
    {
        traceTarget = pos;  // 타깃 위치
        agent.speed = traceSpeed;   // 추적 속도로
        agent.angularSpeed = 200;   // 각 속도 200
        TraceTarget(traceTarget);   // 타깃 추적
    }

    public float GetSpeed() // 스피드
    {
        return agent.velocity.magnitude;    // 내비게이션 이동 속도 얻기
    }

    void Start()    // 시작
    {
        agent = GetComponent<NavMeshAgent>();   // navmeshagent 컴포넌트 찾기
        agent.speed = patrolSpeed;              // 순찰 속도 변수 대입

        id.SetActive(true); // id 켜기

        var group = GameObject.Find("WayPointGroup");   // 하이라키에서 게임 오브젝트 찾아서
        if (group != null)  // 비어있지 않으면
        {
            group.GetComponentsInChildren<Transform>(wayPoints);    // 자식 오브젝트 추가
            wayPoints.RemoveAt(0);  // 배열 첫번째 항목 삭제
            nextIdx = Random.Range(0, wayPoints.Count); // 첫번째 이동 위치 랜덤 지정
        }
        SetPatrolling(true); // 순찰
    }

    void MoveWayPoint() // 다음 목적지까지 이동
    {
        if (agent.isPathStale) return;  // 최단 거리 경로 계산 끝나면
        agent.destination = wayPoints[nextIdx].position;    // 다음 목적지 배열에서 추출하여 지정
        agent.isStopped = false;    // 내비게이션 활성화 이동
    }

    void TraceTarget(Vector3 pos)   // 추적
    {
        if (agent.isPathStale)  return; // 최단 거리 경로 계산 끝나면
        agent.destination = pos;    // 위치 지정
        agent.isStopped = false;    // 내비게이션 활성화 이동
    }

    public void Stop()
    {
        agent.isStopped = true;     // 내비게이션 비활성화
        agent.velocity = Vector3.zero;  //속도 제로
        patrolling = false; // 순찰 안함
    }

    void Update()
    {        
        if (!patrolling)    // 순찰모드가 아니면
            return; // 안함
        if (agent.velocity.magnitude > 0.2f && agent.remainingDistance <= 0.5f) // navmeshagent가 이동하고 목적지에 도착했으면
        {
            nextIdx = Random.Range(0, wayPoints.Count); // 다음 목적지 랜덤 설정
            MoveWayPoint();     // 다음 목적지로 이동
        }
    }
}
