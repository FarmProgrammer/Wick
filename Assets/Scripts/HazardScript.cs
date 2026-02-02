using UnityEngine;

public class HazardScript : MonoBehaviour
{
    public LevelManager manager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            manager.FailLevel();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;

        Gizmos.DrawWireCube(transform.position + new Vector3(GetComponent<BoxCollider2D>().offset.x, GetComponent<BoxCollider2D>().offset.y, 0), GetComponent<BoxCollider2D>().size);
    }
}
