using Godot;
using System;
using System.Reflection;

public partial class Knight : CharacterBody2D
{
	private const int Speed = 200;
	private const int JumpForce = -600;
	private const float Gravity = 1000.0f;
	private bool isAttack = false;
	private Vector2 velocity = new Vector2();
	private AnimatedSprite2D animation;

	private void _on_ready()
	{
		setAnimation();
	}


	public override void _PhysicsProcess(double delta)
	{
		move(delta);
		attack(delta);
		MoveAndSlide();
	}


	private void setAnimation()
	{
		animation = GetNode<AnimatedSprite2D>(CharacterNode.NameCharacterNode.Knight);

		animation.Play(CharacterAnimation.NameCharacterAnimation.Knight_Idle);
	}

	private void move(double delta)
	{
		if (isAttack) return;
		if (Input.IsActionPressed("ui_right"))
		{
			animation.Play(CharacterAnimation.NameCharacterAnimation.Knight_Run);
			animation.FlipH = false;
			velocity.X = Speed;
		}
		else if (Input.IsActionPressed("ui_left"))
		{
			animation.Play(CharacterAnimation.NameCharacterAnimation.Knight_Run);
			animation.FlipH = true;
			velocity.X = -Speed;
		}
		else
		{
			animation.Play(CharacterAnimation.NameCharacterAnimation.Knight_Idle);
			velocity.X = 0;
		}

		if (IsOnFloor())
		{
			if (Input.IsActionJustPressed("ui_up"))
			{
				velocity.Y = JumpForce;
			}
			else
			{
				velocity.Y = 0;
			}
		}
		else
		{
			string animationName = velocity.Y > 0 ? CharacterAnimation.NameCharacterAnimation.Knight_Fall : CharacterAnimation.NameCharacterAnimation.Knight_Jump;
			animation.Play(animationName);
			velocity.Y += Gravity * (float)delta;
		}

		Velocity = velocity;
	}

	private void attack(double delta)
	{
		if (Input.IsActionJustPressed("ui_attack"))
		{
			isAttack = true;
			animation.Play(CharacterAnimation.NameCharacterAnimation.Knight_Attack);
		}
	}
}




