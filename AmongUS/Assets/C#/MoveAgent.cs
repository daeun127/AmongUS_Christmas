using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   // NavMeshAgent�� ����

public class MoveAgent : MonoBehaviour
{
    public List<Transform> wayPoints;   // ���� ���� ����
    public int nextIdx;     // ���� ���� ������ �迭

    float patrolSpeed = 4.0f;   // �����ӵ�
    float traceSpeed = 7.0f;    // �����ӵ�

    NavMeshAgent agent; // ������̼�
    bool patrolling;    // ���� ���ΰ�
    Vector3 traceTarget;    // Ÿ�� ��ġ

    public GameObject id;  // id

    public void SetPatrolling(bool patrol)  // ������ ����
    {
        patrolling = patrol;    // ���� ������
        agent.speed = patrolSpeed;  //���� �ӵ���
        agent.angularSpeed = 120;   // �� �ӵ� 120
        MoveWayPoint(); // ���� ����Ʈ
    }

    public void SetTraceTarget(Vector3 pos) // ���� Ÿ��
    {
        traceTarget = pos;  // Ÿ�� ��ġ
        agent.speed = traceSpeed;   // ���� �ӵ���
        agent.angularSpeed = 200;   // �� �ӵ� 200
        TraceTarget(traceTarget);   // Ÿ�� ����
    }

    public float GetSpeed() // ���ǵ�
    {
        return agent.velocity.magnitude;    // ������̼� �̵� �ӵ� ���
    }

    void Start()    // ����
    {
        agent = GetComponent<NavMeshAgent>();   // navmeshagent ������Ʈ ã��
        agent.speed = patrolSpeed;              // ���� �ӵ� ���� ����

        id.SetActive(true); // id �ѱ�

        var group = GameObject.Find("WayPointGroup");   // ���̶�Ű���� ���� ������Ʈ ã�Ƽ�
        if (group != null)  // ������� ������
        {
            group.GetComponentsInChildren<Transform>(wayPoints);    // �ڽ� ������Ʈ �߰�
            wayPoints.RemoveAt(0);  // �迭 ù��° �׸� ����
            nextIdx = Random.Range(0, wayPoints.Count); // ù��° �̵� ��ġ ���� ����
        }
        SetPatrolling(true); // ����
    }

    void MoveWayPoint() // ���� ���������� �̵�
    {
        if (agent.isPathStale) return;  // �ִ� �Ÿ� ��� ��� ������
        agent.destination = wayPoints[nextIdx].position;    // ���� ������ �迭���� �����Ͽ� ����
        agent.isStopped = false;    // ������̼� Ȱ��ȭ �̵�
    }

    void TraceTarget(Vector3 pos)   // ����
    {
        if (agent.isPathStale)  return; // �ִ� �Ÿ� ��� ��� ������
        agent.destination = pos;    // ��ġ ����
        agent.isStopped = false;    // ������̼� Ȱ��ȭ �̵�
    }

    public void Stop()
    {
        agent.isStopped = true;     // ������̼� ��Ȱ��ȭ
        agent.velocity = Vector3.zero;  //�ӵ� ����
        patrolling = false; // ���� ����
    }

    void Update()
    {        
        if (!patrolling)    // ������尡 �ƴϸ�
            return; // ����
        if (agent.velocity.magnitude > 0.2f && agent.remainingDistance <= 0.5f) // navmeshagent�� �̵��ϰ� �������� ����������
        {
            nextIdx = Random.Range(0, wayPoints.Count); // ���� ������ ���� ����
            MoveWayPoint();     // ���� �������� �̵�
        }
    }
}
