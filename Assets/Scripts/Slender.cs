using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public enum State
{
    IDLE = 0,
    STALKING = 1,
    AGGRESSIVE = 2
}

public class Movement : MonoBehaviour
{
    Transform player;
    public LayerMask obstacleMask;
    private float slenderRadius = 0.6f;
    private float nextMovement = 5.0f;
    private float movementPeriod = 15.0f;
    private SC_FPSController player_class;
    public int speed = 5;
    float timeDeath = 0.0f;
    float maxTimeDeath = 13.0f;
    private State slender_state = State.IDLE;
    public float maxRange = 100.0f;
    // Start is called before the first frame update
    public NavMeshAgent slender;
    public NavMeshPath path;

    float Lerp(float firstFloat, float secondFloat, float by)
    {
        return firstFloat * (1 - by) + secondFloat * by;
    }

    bool IsPositionFree(Vector3 position)
    {
        return !Physics.CheckSphere(
            position,
            slenderRadius,
            obstacleMask,
            QueryTriggerInteraction.Ignore
        );
    }

    void Start()
    {
        player = GameObject.Find("FPSPlayer").GetComponent<Transform>();
        path = new NavMeshPath();
        player_class = GameObject.Find("FPSPlayer").GetComponent<SC_FPSController>();
    }

    public float GetDistanceToPlayer()
    {
        return (transform.position - player.transform.position).magnitude;
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        switch(slender_state)
        {
            case State.IDLE:
                Teleport();
                break;
            case State.STALKING:
                if (Time.time > nextMovement && !IsInFront(transform.position))
                {
                    nextMovement = Time.time + movementPeriod;
                    slender_state = State.IDLE;
                }
                break;
            case State.AGGRESSIVE:
                break;
            default:
                break;
        }
        /*
        else
        {
            timeDeath = timeDeath > 0 ? timeDeath - 0.2f : 0;
        }*/
    }
    Vector3 GetTeleportPosition(Vector3 playerPosition, float distance)
    {

        for (int i = 0; i < 30; i++)
        {
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float x = playerPosition.x + Mathf.Cos(angle) * distance;
            float z = playerPosition.z + Mathf.Sin(angle) * distance;
            Vector3 newPos = new(x, 0.0f, z);
            if (!IsPositionFree(newPos))
            {
                continue;
            }
            return newPos;
        }
        return transform.position;
    }
    private void Teleport()
    {
        float distance = Lerp(100.0f, 20.0f, (float)player_class.getPageNumber() / 8);
        Vector3 newPos = GetTeleportPosition(player.position, distance);
        while (IsInFront(newPos))
        {
            newPos = GetTeleportPosition(player.position, distance);
            nextMovement = Time.time + movementPeriod;
        }
        transform.position = newPos;
        slender_state = State.STALKING;
    }

    public bool IsInFront(Vector3 newPos)
    {
        Vector3 directionOfPlayer = transform.position - player.position;
        float distanceToTarget = directionOfPlayer.magnitude;
        float angle = Vector3.Angle(player.forward, directionOfPlayer);
        if (Mathf.Abs(angle) > 30 && Mathf.Abs(angle) < 330)
        {
            return false;
        }
        if(distanceToTarget > maxRange)
        {
            return false;
        }
        return true;
    }
}