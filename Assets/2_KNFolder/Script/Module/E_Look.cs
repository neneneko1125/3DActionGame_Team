using Enemy;
using UnityEngine;
using UnityEngine.AI;

public class E_Look : EnemyModule
{
    private GameObject _target;

    private NavMeshAgent _agent;

    private void Start()
    {
        _target = GetComponent<EnemyBase>()._target;

        _agent = GetComponent<NavMeshAgent>();
    }
    override public void OnTick()
    {
        if (!_target) return;

        transform.LookAt(_agent.destination);
    }

}
