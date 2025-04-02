using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    [SerializeField] private float CameraOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Locks Camera Y Value
        transform.position = new Vector3(transform.position.x, transform.position.y, CameraOffset);
    }
}
