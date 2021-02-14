using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MovetoCannon : MonoBehaviour
{
    [SerializeField] private Transform cannon;
    [SerializeField] public NavMeshAgent Agent;
    public UnityEvent UnityEvent;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, cannon.position) > 2)
        {
            Agent.SetDestination(cannon.position);  
        }
        else
        {
            _animator.SetTrigger("bite");
            UnityEvent.Invoke();
        }
        
    }
}
