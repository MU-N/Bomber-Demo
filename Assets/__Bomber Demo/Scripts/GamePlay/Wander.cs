using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour, ISteer
{
    [SerializeField] float range = 5;
    [SerializeField] LayerMask mask;
    [SerializeField] float speed = 5;

    [SerializeField] Vector3 target;
    public Vector3 GetForce()
    {
        if ((target - transform.position).magnitude < 1)
        {
            target = GetRandomPoint();
        }
        target.y = 0.5f;

        Vector3 dir = target - transform.position;
        Vector3 velocity = dir.normalized * speed;

        return velocity;
    }

    Vector3 GetRandomPoint()
    {
        Vector3 randomPosition = Random.insideUnitSphere * range;
        RaycastHit hit;
        Ray ray = new Ray(randomPosition, Vector3.down);
        Debug.DrawRay(randomPosition, Vector3.down * range);
        if (Physics.Raycast(ray, out hit, range, mask))
        {
            return hit.point;
        }
        return transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(target, 0.5f);
    }
}