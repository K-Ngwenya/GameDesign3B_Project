using UnityEngine;

public class JumpingState:State
{
    bool grounded;

    float gravityValue;
    float jumpHeight;
    float playerSpeed;

    Vector3 airVelocity;

    public JumpingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
	{
		character = _character;
		stateMachine = _stateMachine;
	}

    public override void Enter()
	{
		base.Enter();

		grounded = false;
        gravityValue = character.gravityValue;
        jumpHeight = character.jumpHeight;
        playerSpeed = character.playerSpeed;
        gravityVelocity.y = 0;

        character.animator.SetFloat("speed", 0);
        character.animator.SetTrigger("jump");
        Jump();
	}
	public override void HandleInput()
	{
		base.HandleInput();

        input = moveAction.ReadValue<Vector2>();
    }

	public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (grounded)
		{
            stateMachine.ChangeState(character.landing);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
		if (!grounded)
		{

            velocity = character.playerVelocity;
            airVelocity = new Vector3(input.x, 0, input.y);

            /*velocity = velocity.x  + velocity.z;
            velocity.y = 0f;
            airVelocity = airVelocity.x + airVelocity.z;
            airVelocity.y = 0f;*/
            character.controller.Move(gravityVelocity * Time.deltaTime+ (airVelocity*character.airControl+velocity*(1- character.airControl))*playerSpeed*Time.deltaTime);
        }

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
    }

    void Jump()
    {
        gravityVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }

}

