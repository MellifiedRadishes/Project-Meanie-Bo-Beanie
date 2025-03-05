using UnityEngine;

public class AttackBarController : MonoBehaviour
{
    public Transform attackBar; // The moving pointer (red)
    public Transform sliderBar; // The background bar (black)
    public Transform greenBar; // The target zone (green)
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

    void CheckHit()
    {
        float center = greenBar.position.x; // Center of the green bar
        float greenWidth = greenBar.GetComponent<Renderer>().bounds.size.x / 2;

        if (attackBar.position.x >= (center - greenWidth) && attackBar.position.x <= (center + greenWidth))
        {
            Debug.Log("Green bar was hit. Attack detected");
        }
    }
}
