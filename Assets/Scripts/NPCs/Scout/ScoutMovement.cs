using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ScoutMovement : MonoBehaviour
{
    private GameObject Player;
    private PlayerMovementScript PlayerMovement;
    private Vector3 velocity;
    [SerializeField] private float maxSpeed;
    NavMeshAgent Nav;
    [SerializeField] public GameObject model;
    Animator animator;
    private bool isMoving;

        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameObject.transform.parent = GameObject.Find("PERSISTENTOBJECTS").transform;
        Nav = GetComponent<NavMeshAgent>();

        Nav.updatePosition = false;
        Nav.updateRotation = false;

        animator = model.GetComponent<Animator>();

        Player = GameObject.Find("Player");
        PlayerMovement = Player.GetComponent<PlayerMovementScript>();
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerMovement.GetCutscene()) {
            FollowPlayer();
            UpdateSprite();
            SpriteDirectionUpdate();
        }


    }

    void FollowPlayer() {
        Nav.SetDestination(Player.transform.position);

        isMoving = false;
        if (Vector3.Distance(Player.transform.position, transform.position) > 2)
        {
            if (Vector3.Distance(Player.transform.position, transform.position) > 1.9) {
                isMoving = true;
            }
                
            transform.position = Vector3.SmoothDamp(transform.position, Nav.nextPosition, ref velocity, Time.deltaTime, maxSpeed);
        }
    }

    public void TeleportToPlayer() {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y - .4f, Player.transform.position.z);
    }

    private void UpdateSprite()
    {
        if (isMoving)
        {
            animator.SetTrigger("Moving");
            
        }
        else {
            animator.ResetTrigger("Moving");
        }
        
    }

    private void SpriteDirectionUpdate()
    {
        if (Player.transform.position.x < transform.position.x)
        {
            model.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Player.transform.position.x > transform.position.x)
        {
            model.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

}
