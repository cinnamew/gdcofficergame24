using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiming : BotAiming
{
    [SerializeField] private float detectionRange = 10f; // Adjustable detection range

    GameObject GetHighestPriorityTarget()
    {
        string targetTag = gameObject.GetComponent<GenericHitbox>().targetTag; //Finds the target tag this object is looking for
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        GameObject highestPriorityTarget = null;
        int highestPriority = int.MinValue;
        float closestDistance = Mathf.Infinity;
        Vector2 currentPosition = transform.position;

        foreach (GameObject target in targets)
        {
            BotAiming botAiming = target.GetComponent<BotAiming>();
            if (botAiming != null)
            {
                float distanceToTarget = Vector2.Distance(currentPosition, target.transform.position);
                if (distanceToTarget <= detectionRange && botAiming.aimPriority > highestPriority)
                {
                    highestPriority = botAiming.aimPriority;
                    highestPriorityTarget = target;
                    closestDistance = distanceToTarget;
                }
                else if (distanceToTarget <= detectionRange && botAiming.aimPriority == highestPriority && distanceToTarget < closestDistance)
                {
                    // If there are multiple targets with the same highest priority, choose the closest one
                    highestPriorityTarget = target;
                    closestDistance = distanceToTarget;
                }
            }
        }

        return highestPriorityTarget;
    }

    // Update is called once per frame
    public override void CheckAimPriority() {
        //first check for whatever target is closest. Then check aimPriority value
        GameObject target = GetHighestPriorityTarget();
        if (target != null)
        {
            // Aim at the target
            aimPoint = target.transform.position;
        }
        else
        {
            // If no target found, do default behavior
            base.CheckAimPriority();
        }
    }
}
