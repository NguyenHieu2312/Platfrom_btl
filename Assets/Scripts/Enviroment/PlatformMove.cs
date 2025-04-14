using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    [SerializeField] private float plfatformSpeed = 5f;
    private bool movingToB = true;
    void Update()
    {
        Vector2 targetPosition = movingToB ? pointB.position : pointA.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, plfatformSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            movingToB = !movingToB;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(this.gameObject.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
