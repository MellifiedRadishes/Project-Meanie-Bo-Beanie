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
    private Boolean InCutscene;
    private SCENE oldScene;
    
    //Visuals
    private SpriteRenderer SR;

    void Start()
    {
        //Creates Character Controller Component
        Controller = gameObject.GetComponent<CharacterController>();
        SR = gameObject.GetComponent<SpriteRenderer>();
        InCutscene = false;
    }

    void Update()
    {
        if (!InCutscene) {
            Move();
            SpriteDirectionUpdate();
        }
    }


    private void Move() {
        //Reads User Input
        VerticalInput = Input.GetAxis("Vertical");
        HortizontalInput = Input.GetAxis("Horizontal");

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

    private void SpriteDirectionUpdate() {
        if (HortizontalInput > 0) {
            SR.flipX = false;
        } else if (HortizontalInput < 0) {
            SR.flipX = true;
        }
    }

    /*====SETTERS/GETTERS====*/
    public void SetCutscene(Boolean boolean) {
        InCutscene = boolean;
    }
    public void SetOldScene(SCENE scene)
    {
        oldScene = scene;
    }
    public SCENE GetOldScene()
    {
        return oldScene;
    }

}
