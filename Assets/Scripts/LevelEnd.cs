using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public LevelManager manager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            manager.CompleteLevel();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.green;

        Gizmos.DrawWireCube(transform.position + new Vector3(GetComponent<BoxCollider2D>().offset.x, GetComponent<BoxCollider2D>().offset.y, 0), GetComponent<BoxCollider2D>().size);
    }
}
