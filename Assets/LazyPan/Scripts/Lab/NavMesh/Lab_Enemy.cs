using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Lab_Enemy : MonoBehaviour {
    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;
    private GameObject Player;

    private Vector3 lastPos;
    private float moveIntervalTime;
    private float moveDeployTime;
    public bool IsMove;

    private NavMeshSurface surface;
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        surface = GameObject.Find("NavMeshSurface").GetComponent<NavMeshSurface>();
        Player = GameObject.Find("Player");
        agent.SetDestination(Player.transform.position);
        obstacle.enabled = false;
        moveIntervalTime = 1;
        IsMove = true;
    }

    void Update() {
        Move();
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance < agent.radius + 0.5f || !IsMove) {
            agent.enabled = false;
            obstacle.enabled = true;
            Debug.Log("Build Nav Mesh");
        }
    }

    void Move() {
        if (!IsMove) {
            return;
        }

        IsMove = true;
        if (moveDeployTime < moveIntervalTime) {
            moveDeployTime += Time.deltaTime;
        } else {
            if (Vector3.Distance(transform.position, lastPos) < 0.1f) {
                IsMove = false;
            }
            lastPos = transform.position;
            moveDeployTime = 0;
        }
    }
}
