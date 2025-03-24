using UnityEngine;
using UnityEngine.AI;

public class CompanionMovement : MonoBehaviour
{
    public GameObject Player;
    public Vector3 TargetLocation;
    NavMeshAgent Nav;
    private SpriteRenderer SR;

        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Nav = GetComponent<NavMeshAgent>();
        Nav.updateRotation = false;

        SR = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        SpriteDirectionUpdate();

    }

    void FollowPlayer() {
        if (Vector3.Distance(Player.transform.position, transform.position) > 2)
        {
            Nav.SetDestination(Player.transform.position);
        }
    }

    private void SpriteDirectionUpdate()
    {
        if (Player.transform.position.x < transform.position.x)
        {
            SR.flipX = false;
        }
        else if (Player.transform.position.x > transform.position.x)
        {
            SR.flipX = true;
        }
    }

}
