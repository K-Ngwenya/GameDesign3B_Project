using UnityEngine;
using UnityEngine.InputSystem;

public class State
{
    public Character character;
    public StateMachine stateMachine;

    protected Vector3 gravityVelocity;
    protected Vector3 velocity;
    protected Vector2 input;

    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction sprintAction;
    public InputAction attackAction;

    public State(Character _character, StateMachine _stateMachine)
	{
        character = _character;
        stateMachine = _stateMachine;

        moveAction = character.playerInput.actions["Move"];
        jumpAction = character.playerInput.actions["Jump"];
        sprintAction = character.playerInput.actions["Sprint"];
        attackAction = character.playerInput.actions["Attack"];
    }

    public virtual void Enter()
    {
        //StateUI.instance.SetStateText(this.ToString());
		Debug.Log("Enter State: "+this.ToString());
    }

    public virtual void HandleInput()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Exit()
    {
    }
}

