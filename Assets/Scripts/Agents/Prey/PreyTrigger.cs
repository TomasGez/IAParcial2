using System.Collections;
using UnityEngine;

public class PreyTrigger : MonoBehaviour
{
    [SerializeField] private AgentPrey _agent;
    [SerializeField] private LayerMask _targetMask = ~0;
    [SerializeField] private float _escapingTime;
    Coroutine _coroutine;

    private void OnTriggerEnter(Collider other)
    {
        if ((_targetMask.value & (1 << other.gameObject.layer)) != 0)
        {
            AgentHunter target = other.GetComponentInParent<AgentHunter>();
                
            if (target != _agent.GetCurrentTarget())
            {
                _agent?.AddTarget(target);
            }
            else
            {
                if(_coroutine != null) StopCoroutine(_coroutine);
            }

            _agent.IsBeingFollowed(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((_targetMask.value & (1 << other.gameObject.layer)) != 0)
        {
            AgentHunter target = other.GetComponentInParent<AgentHunter>();
                
            if (target != _agent.GetCurrentTarget()) _agent.RemoveTarget(target);

            _coroutine = StartCoroutine(KeepGoing(target));
        }
    }

    IEnumerator KeepGoing(AgentHunter target)
    {
        yield return new WaitForSeconds(_escapingTime);

        _agent.RemoveTarget(target);
    }
}
