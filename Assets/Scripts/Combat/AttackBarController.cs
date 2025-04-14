using UnityEngine;

public class AttackBarController : MonoBehaviour
{
    public Transform attackBar; // The moving pointer (red)
    public Transform sliderBar; // The background bar (black)
    public Transform greenBar; // The target zone (green)
    public Transform criticalBar; // The critical bar
    public float speed = 5f; // Speed of movement

    private float leftLimit, rightLimit;
    private bool movingRight = true;

    void Start()
    {
        // Get actual width in world space
        float barWidth = sliderBar.GetComponent<Renderer>().bounds.size.x / 2;
        leftLimit = sliderBar.position.x - barWidth;
        rightLimit = sliderBar.position.x + barWidth;
    }

    void Update()
    {
        // Move the AttackBar (pointer) back and forth
        float moveAmount = speed * Time.deltaTime;
        if (movingRight)
        {
            attackBar.position += Vector3.right * moveAmount;
            if (attackBar.position.x >= rightLimit)
                movingRight = false;
        }
        else
        {
            attackBar.position += Vector3.left * moveAmount;
            if (attackBar.position.x <= leftLimit)
                movingRight = true;
        }

        // Detect Spacebar Press for attack timing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckHit();
        }
    }

    int CheckHit()
    {
        
        float centerCrit = criticalBar.position.x;
        float critWidth = criticalBar.GetComponent<Renderer>().bounds.size.x / 2;
        if (attackBar.position.x >= (centerCrit - critWidth) && attackBar.position.x <= (centerCrit + critWidth))
        {
            Debug.Log("Critical bar was hit. Critical Attack detected");
            speed = 0;
            return 2;
        }
        
        float centerGreen = greenBar.position.x; // Center of the green bar
        float greenWidth = greenBar.GetComponent<Renderer>().bounds.size.x / 2;
        if (attackBar.position.x >= (centerGreen - greenWidth) && attackBar.position.x <= (centerGreen + greenWidth))
        {
            Debug.Log("Green bar was hit. Attack detected");
            speed = 0f;
            return 1;
        }

        return 0;
    }
}
