using System;
using Godot;

public partial class Player : CharacterBody2D
{



	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	const float Speed = 130.0f;
	const float JumpVelocity = -300.0f;
	Vector2 velocity;



	public override void _Ready()
	{
	}


	public void moveRight()
	{
		velocity.X += Speed;
		GD.Print("Moving RIGHT");
	}

	public void moveLeft()
	{
		velocity.X -= Speed;
		GD.Print("Moving LEFT");
	}


	public void jump()
	{
		if (IsOnFloor())
		{
			this.velocity.Y = JumpVelocity;
		}
	}



	public void attack()
	{



	}

	void CheckHorizontalInput(double delta)
	{
		Vector2 velocity = Velocity;
		float HorizontalMovement = Input.GetAxis("left", "right");
		if (HorizontalMovement != 0)
		{
			velocity.X = HorizontalMovement * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();

	}

	void CheckVerticalInput(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
		}

		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}
		float horizontalMovement = Input.GetAxis("left", "right");
		if (horizontalMovement != 0)
		{
			velocity.X = horizontalMovement * Speed;
		}
		Velocity = velocity;
		MoveAndSlide();
	}


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		

		CheckHorizontalInput(delta);
		CheckVerticalInput(delta);

		// Gradually stop the player when no input is detected
		velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed * (float)delta);
		Velocity = velocity;
		MoveAndSlide();

	}
}


