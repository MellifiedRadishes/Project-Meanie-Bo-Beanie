using System;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    //Speed Variables
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float Gravity;
    private float yVelocity;

    //Controller Variables
    private CharacterController Controller;
    private float VerticalInput;
    private float HorizontalInput;

    private bool isMoving;

    public Boolean InCutscene;

    //Visuals
    [SerializeField] private GameObject model;
    private Animator animator;
    void Start()
    {
        //Creates Character Controller Component
        Controller = gameObject.GetComponent<CharacterController>();
        InCutscene = false;
        animator = model.GetComponent<Animator>();
        isMoving = false;
    }

    void Update()
    {
        VerticalInput = 0;
        HorizontalInput = 0;
        if (InCutscene)
        {
            Controller.enabled = false;
            isMoving = false;
        }
        else
        {
            //Reads User Input
            VerticalInput = Input.GetAxis("Vertical");
            HorizontalInput = Input.GetAxis("Horizontal");
            UpdateIsMoving();

            //Enables the Character Controller
            Controller.enabled = true;
            Move();
        }
        UpdateModel();
    }

    private void Move() {
        //Calculates Player Gravity
        CalcGravity();

        Vector3 MoveDirection = Vector3.ClampMagnitude(new Vector3(HorizontalInput, -yVelocity, VerticalInput), 1);
        Controller.Move(Time.deltaTime * (MoveDirection * MoveSpeed));
    }

    private void CalcGravity() {
        if (Controller.isGrounded) {
            yVelocity = 0;
        } else {
            yVelocity += Gravity;
        }
    }

    private void UpdateModel() {
        
        if (isMoving)
        {
            animator.SetTrigger("WALK");
        }
        else {
            animator.ResetTrigger("WALK");
        }
        ModelDirectionUpdate();
    }

    private void UpdateIsMoving() {
        if (VerticalInput == 0 && HorizontalInput == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }

    private void ModelDirectionUpdate() {
        if (HorizontalInput > 0) {
            model.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else if (HorizontalInput < 0) {
            model.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    /*====SETTERS/GETTERS====*/
    public Boolean GetCutscene()
    {
        return InCutscene;
    }

    public void SetCutscene(Boolean boolean) {
        InCutscene = boolean;
    }

    public Boolean GetIsMoving()
    {
        return isMoving;
    }

    public void SetIsMoving(Boolean boolean)
    {
        isMoving = boolean;
    }

}
