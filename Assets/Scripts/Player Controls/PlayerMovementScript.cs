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
    private float HortizontalInput;

    private float lastVerticalInput;
    private float lastHortizontalInput;

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
    }

    void Update()
    {
        VerticalInput = 0;
        HortizontalInput = 0;
        if (InCutscene)
        {
            Controller.enabled = false;
        }
        else
        {
            //Reads User Input
            VerticalInput = Input.GetAxis("Vertical");
            HortizontalInput = Input.GetAxis("Horizontal");
            //Enables the Character Controller
            Controller.enabled = true;
            Move();
        }
        UpdateModel();
    }

    private void Move() {
        //Calculates Player Gravity
        CalcGravity();

        Vector3 MoveDirection = Vector3.ClampMagnitude(new Vector3(HortizontalInput, -yVelocity, VerticalInput), 1);
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
        
        if (VerticalInput == 0 && HortizontalInput == 0)
        {
            animator.ResetTrigger("WALK");
        }
        else {
            animator.SetTrigger("WALK");
        }
        ModelDirectionUpdate();
    }

    private void ModelDirectionUpdate() {
        if (HortizontalInput > 0) {
            model.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else if (HortizontalInput < 0) {
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

}
