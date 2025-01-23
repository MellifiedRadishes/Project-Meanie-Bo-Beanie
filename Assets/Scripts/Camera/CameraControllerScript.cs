using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Locks Camera Y Value
        transform.position = new Vector3(transform.position.x, transform.position.y, -2.75f);
    }
}
