using UnityEngine;

public class MoveOnTrigger : MonoBehaviour
{
    public bool MoveNow = false;

    public Vector3 targetPosition;
    public bool positionIsRelative;
    public float travelTime;

    private Vector3 difference;
    private Vector3 startPos;

    public void TriggerMove()
    {
        MoveNow=true;
        startPos=transform.position;
        if (positionIsRelative)
        {
            difference = transform.position + targetPosition - transform.position;
        }
        else
        {
            difference = targetPosition - transform.position;
        }
    	
    }

    void Update()
    {
        if (MoveNow)
        {
            transform.Translate(difference.normalized * (Time.deltaTime*(difference.magnitude/travelTime)));
        }

        if((startPos - transform.position).magnitude >= difference.magnitude)
        {
            MoveNow=false;
        }
    }

    void OnDrawGizmos()
    {
        if (positionIsRelative)
        {
            Gizmos.color=Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
            Gizmos.DrawWireSphere(transform.position+targetPosition, 0.5f);
            Gizmos.DrawLine(transform.position, transform.position+targetPosition);
        }
        else
        {
            Gizmos.color=Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
            Gizmos.DrawWireSphere(targetPosition, 0.5f);
            Gizmos.DrawLine(transform.position, targetPosition);
        }
    }
}
