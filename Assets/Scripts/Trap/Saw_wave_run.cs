using UnityEngine;

public class Saw_wave_run : MonoBehaviour
{
    [SerializeField] private float speed = 5f;                  
    [SerializeField] private GameObject checkPointToRun;     
    [SerializeField] private Vector2 moveDirection = Vector2.right; 

    public bool isActivated = false;                    

    void Start()
    {
        if (checkPointToRun == null)
            Debug.LogError("checkPointToRun is not assigned!");
        
    }

    void Update()
    {
        if (isActivated)
            transform.position += (Vector3)(moveDirection.normalized * speed * Time.deltaTime);
        
        /*if (!isActivated)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null && Vector2.Distance(player.transform.position, checkPointToRun.transform.position) < 0.5f)
            {
                isActivated = true;
                Debug.Log("Saw is now moving forward!");
            }
        }*/
    }

    void OnDrawGizmos()
    {
        if (checkPointToRun != null)
        {
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawWireSphere(checkPointToRun.transform.position, 0.5f);
        }
    }
}