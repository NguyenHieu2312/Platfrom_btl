using UnityEngine;

public class ParalaxBG : MonoBehaviour
{
    private float startPosition;
    private float bgLenght;
    private GameObject cam;

    [SerializeField]
    private float parallaxEffect;


    private void Start()
    {
        cam = GameObject.Find("CmCam");
        startPosition = transform.position.x;
        bgLenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        if (temp > startPosition + bgLenght)
            startPosition += bgLenght;

        else if(temp < startPosition - bgLenght)
            startPosition -= bgLenght;

    }
}
