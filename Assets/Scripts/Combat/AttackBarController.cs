using System.Collections;
using UnityEngine;

public class AttackBarController : MonoBehaviour
{
    public Transform attackBar; // The moving pointer (red)


    public Transform sliderBar; // The background bar (black)
    public Transform greenBar; // The target zone (green)
    public Transform critBar; // crit zone (yellow)
    public float speed = 10f; // Speed of movement

    private float leftLimit, rightLimit;
    private bool movingRight = true;
    public bool Triggered { get; private set; } = false;
    private int val = 4;

    void Start()
    {
        // Get actual width in world space
        float barWidth = sliderBar.GetComponent<Renderer>().bounds.size.x / 2;
        leftLimit = sliderBar.position.x - barWidth;
        rightLimit = sliderBar.position.x + barWidth;

        GetComponentInParent<Canvas>().worldCamera = Camera.main;
    }

    public int GetVal()
    {
        return val;
    }

    void Update()
    {
        

        // Move the AttackBar (pointer) back and forth
        float moveAmount = speed * Time.deltaTime;
        if (movingRight)
        {
            attackBar.position += Vector3.right * moveAmount;
            if (attackBar.position.x >= rightLimit)
            {
                movingRight = false;
                val = CheckHit();
                Triggered = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            val = CheckHit();
            Triggered = true;
        }

        if (Triggered == true)
        {
            Combat.SetTempSliderRead(val);
            Destroy(transform.parent.gameObject);
            return;
        }

    }

    

    int CheckHit()
    {
        float center = greenBar.position.x; // Center of the green bar
        float greenWidth = greenBar.GetComponent<Renderer>().bounds.size.x / 2;
        float yellowWidth = critBar.GetComponent<Renderer>().bounds.size.x / 2;

        if (attackBar.position.x >= (center - yellowWidth) && attackBar.position.x <= (center + yellowWidth))
        {
            Debug.Log("Yellow bar was hit. Crit detected");
            speed = 0f;
            return 2;
        } else if (attackBar.position.x >= (center - greenWidth) && attackBar.position.x <= (center + greenWidth))
        {
            Debug.Log("Green bar was hit. Attack detected");
            speed = 0f;
            return 1;
        } else
        {
            Debug.Log("Miss, skill issue detected");
            speed = 0f;
            return 0;
        }
        
    }
}
