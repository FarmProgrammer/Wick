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
}
