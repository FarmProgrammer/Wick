using UnityEngine;

public class DropPlatform : MonoBehaviour
{
    [SerializeField, Range(0f, 2f)]
    private float dropDelay=0.3f;
    private bool hasDropped = false;
    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.collider.tag);
        if (col.collider.tag == "Player" && !hasDropped)
        {
            hasDropped = true;
            Invoke("Drop", dropDelay);
        }
    }

    private void Drop()
    {
        gameObject.SetActive(false);
    }

    public void ResetPlatform()
    {
        gameObject.SetActive(true);
        hasDropped=false;
    }
}
