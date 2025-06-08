using UnityEngine;

public class PoitToRun : MonoBehaviour
{
    Saw_wave_run saw;

    private void Start()
    {
        saw = FindFirstObjectByType<Saw_wave_run>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            saw.isActivated = true;
    }
}


