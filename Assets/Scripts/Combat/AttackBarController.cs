using UnityEngine;

public class AttackBarController : MonoBehaviour
{
    public Transform attackBar; // The moving pointer (red)
    public Transform sliderBar; // The background bar (for reference)(green)
    public Transform greenBar;
    public float speed = 5f; // Speed of movement
    private float leftLimit, rightLimit;
    private bool movingRight = true;

    void Start()
    {
        // Get the size of the SliderBar to determine movement limits
        float barWidth = sliderBar.localScale.x / 2; // Half the width
        leftLimit = sliderBar.position.x - barWidth;
        rightLimit = sliderBar.position.x + barWidth;
    }

    void Update()
    {
        // Move the AttackBar (pointer) back and forth
        if (movingRight)
        {
            attackBar.position += Vector3.right * speed * Time.deltaTime;
            if (attackBar.position.x >= rightLimit)
                movingRight = false;
        }
        else
        {
            attackBar.position += Vector3.left * speed * Time.deltaTime;
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
        float center = greenBar.position.x; // The center of the bar
        float greenWidth = greenBar.localScale.x / 2;
        if ((center - greenWidth) < attackBar.position.x | attackBar.position.x < (center + greenWidth))
        {
            Debug.Log("Green bar was hit. Attack detected");
        }
    }
}
