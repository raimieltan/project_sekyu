using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Summoned : MonoBehaviour
{
    private NavMeshAgent character;

    public Transform PlayerTarget;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        character.SetDestination(PlayerTarget.position);
    }
}
