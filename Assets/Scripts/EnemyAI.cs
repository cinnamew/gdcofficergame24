using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float detectionRange = 10f; // Adjustable detection range
    [SerializeField] private float moveSpeed = 5f; // Configurable movement speed
    [SerializeField] private bool enableAim = true;
    [SerializeField] private float noMoveRange = 0.1f;
    [SerializeField] private string targetTag;
    public Vector2 aimPoint;
    private bool inFormation = false;

    private Rigidbody2D rb;

    public bool canFlipSprites = false; //OVERRIDE OVVERRIDE PLEAZSSSSE OERRIDE

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetEndPoint(Vector2 point){
        inFormation = true;
        aimPoint = point;
        enableAim = false;
    }

    public void SetMoveSpeed(float speed){
        moveSpeed = speed; 
    }

    public void SetEnableAim(bool aim){
        enableAim = aim;
    }

    public GameObject GetHighestPriorityTarget()
    {
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
                if (distanceToTarget < detectionRange && botAiming.aimPriority > highestPriority)
                {
                    highestPriority = botAiming.aimPriority;
                    highestPriorityTarget = target;
                    closestDistance = distanceToTarget;
                }
                else if (distanceToTarget < detectionRange && botAiming.aimPriority == highestPriority && distanceToTarget < closestDistance)
                {
                    // If there are multiple targets with the same highest priority, choose the closest one
                    highestPriorityTarget = target;
                    closestDistance = distanceToTarget;
                }
            }
        }

        return highestPriorityTarget;
    }

    void FixedUpdate()
    {
        if (enableAim){
            //every frame, look at the aimpoint
            if (!inFormation)
                CheckAimPriority();

            Vector2 direction = (aimPoint - (Vector2)transform.position).normalized;
            //found the mirror culprit\
            if (canFlipSprites) {
                if (direction.x > 0){
                    transform.rotation = Quaternion.Euler(0f, Mathf.Sign(direction.x) * 180f, 0f);
                } else {
                    transform.rotation = Quaternion.Euler(0f, Mathf.Sign(direction.x) * 0f, 0f);
                }
            }
            float distance = ((aimPoint - (Vector2)transform.position)).magnitude;
            rb.velocity = direction * moveSpeed;
            if (distance < noMoveRange){
                rb.velocity = Vector2.zero;
                if (inFormation)
                {
                    Destroy(gameObject); //Need to add particle effects here
                }
            }
        }
    }

    // Update is called once per frame
    void CheckAimPriority()
    {
        //first check for whatever target is closest. Then check aimPriority value
        GameObject target = GetHighestPriorityTarget();
        if (target != null)
        {
            // Aim at the target
            aimPoint = target.transform.position;
        }
        else
        {
            // If no target found, stop moving
            rb.velocity = Vector2.zero;
        }
    }
}
