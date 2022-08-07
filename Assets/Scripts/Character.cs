using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [Header("Controls")]
    public float playerSpeed = 2f;
    public float sprintSpeed = 4f;
    public float jumpHeight = 0.4f; 
    public float gravityMultiplier = 1.2f;
    public float rotationSpeed = 2f;

    [Header("Animation Smoothing")]
    [Range(0, 1)]
    public float speedDampTime = 0.1f;
    [Range(0, 1)]
    public float velocityDampTime = 0.9f;
    [Range(0, 1)]
    public float rotationDampTime = 0.2f;
    [Range(0, 1)]
    public float airControl = 0.3f;

    public StateMachine movementSM;
    public StandingState standing;
    public JumpingState jumping;
    public LandingState landing;
    public SprintState sprinting;
    //public CombatState combatting;
    //public AttackState attacking;

    [HideInInspector]
    public float gravityValue = -9.8f;
    [HideInInspector]
    public float normalColliderHeight;
    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public PlayerInput playerInput;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Vector3 playerVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        movementSM = new StateMachine();
        standing = new StandingState(this, movementSM);
        jumping = new JumpingState(this, movementSM);
        landing = new LandingState(this, movementSM);
        sprinting = new SprintState(this, movementSM);
        //combatting = new CombatState(this, movementSM);
        //attacking = new AttackState(this, movementSM);

        movementSM.Initialize(standing);

        normalColliderHeight = controller.height;
        gravityValue *= gravityMultiplier;
    }

    private void Update()
    {
        movementSM.currentState.HandleInput();

        movementSM.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        movementSM.currentState.PhysicsUpdate();
    }
}
