using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Controls controls;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed;
    private InputAction movementInput;
    private InputAction aimInput;
    private bool isAiming = false;
    Vector2 movement;
    Vector2 aim;
    Vector3 direction;
    Camera cam;
    private bool canMove=true;
    [SerializeField] Transform body;
    [SerializeField] private Animator playerAnim;
    public Transform Body => body;
    public void SetAiming(bool val)
    {
        isAiming = val;
        playerAnim.SetBool("isAiming", isAiming);
    }
    [SerializeField] private float rotationSpeed;
    public void SetCanMove(bool val)
    {
        canMove=val;
        if(val==false)
        playerAnim.SetBool("isMoving", val);
    }
    //[SerializeField] Transform testSphere;
    private void Awake()
    {
        controls = new Controls();
    }
    private void OnEnable()
    {
        movementInput = controls.Player.Movement;
        aimInput = controls.Player.Aim;
        controls.Player.Fire.Enable();
        movementInput.Enable();
        aimInput.Enable();
    }
    private void OnDisable()
    {
        movementInput.Disable();
        aimInput.Disable();
        controls.Player.Fire.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }
    private void GetInput()
    {
        movement = movementInput.ReadValue<Vector2>();
        //if(movement.magnitude>0.1f)
        aim = aimInput.ReadValue<Vector2>();

    }
    private void Movement() {

        if (canMove)
        {
            playerAnim.SetBool("isMoving", movement.magnitude > 0.1f);
            direction = new Vector3(movement.x, 0, movement.y);
            characterController.Move(direction * Time.deltaTime * speed);
            body.forward = Vector3.RotateTowards(body.forward, body.forward + direction, rotationSpeed*Time.deltaTime,0f);
        }
    }
    float gravity=0;
    [SerializeField] private float gravityValue = -1f;
    private void Gravity()
    {
        gravity += gravityValue * Time.deltaTime;
        characterController.Move(new Vector3(0, gravity, 0));
        if (characterController.isGrounded) gravity = 0;

    }
    [SerializeField] private Vector3 aimOffset;
    Vector3 point;
    public Vector3 AimPoint;
    private void Rotation()
    {
        if (isAiming)
        {
            Ray ray = cam.ScreenPointToRay(aim);
            Plane ground = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;
            if (ground.Raycast(ray, out rayDistance))
            {
                point = ray.GetPoint(rayDistance);
                point = new Vector3(point.x, transform.position.y, point.z) + aimOffset;
                AimPoint = point;
                //testSphere.position = point;
                body.LookAt(point);
            }
        }
         
    }


    private void Update()
    {

        GetInput();
        Movement();
        Gravity();
        Rotation();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
    }
}
