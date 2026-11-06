using Godot;

/// <summary>
/// Side-view player controller: walk left and right, jump, and fall with gravity.
/// </summary>
public partial class Player : CharacterBody2D
{
	// Input action names, defined in Project Settings > Input Map.
	// StringName constants avoid creating a new StringName every frame.
	private static readonly StringName MoveLeft = "move_left";
	private static readonly StringName MoveRight = "move_right";
	private static readonly StringName Jump = "jump";
	private static readonly StringName Dash = "dash";

	// Animation names, defined in the SpriteFrames of the AnimatedSprite2D.
	private static readonly StringName AnimIdle = "idle";
	private static readonly StringName AnimMove = "move";
	private static readonly StringName AnimJumpStart = "jump_start";
	private static readonly StringName AnimRise = "rise";
	private static readonly StringName AnimFall = "fall";
	private static readonly StringName AnimLand = "land";
	private static readonly StringName AnimDash = "dash";

	/// <summary>Horizontal speed in pixels per second.</summary>
	[Export] public float Speed = 430f;

	/// <summary>Duration of a dash, in seconds.</summary>
	[Export] public float DashDuration = 0.2f;

	/// <summary>Time after a dash before you can dash again, in seconds.</summary>
	[Export] public float DashCooldown = 1f;

	/// <summary>Horizontal dash speed is the normal speed multiplied by this.</summary>
	[Export] public float DashSpeedMultiplier = 10f;

	/// <summary>Upward speed at the start of a jump. Negative because Y points down in Godot.</summary>
	[Export] public float JumpVelocity = -870f;

	/// <summary>Gravity is multiplied by this while falling, so jumps feel snappy instead of floaty.</summary>
	[Export] public float FallGravityMultiplier = 1.6f;

	/// <summary>Releasing jump while still going up multiplies the upward speed by this, giving a shorter jump.</summary>
	[Export] public float JumpCutMultiplier = 0.4f;

	/// <summary>Falling speed never goes above this, in pixels per second.</summary>
	[Export] public float MaxFallSpeed = 1170f;

	private AnimatedSprite2D _sprite;
	private float _dashDurationTimer = 0f;
	private float _dashCooldownTimer = 0f;
	private bool _wasOnFloor = true;

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		float dt = (float)delta;
		// -1 when only left is held, 1 when only right is held, 0 for none or both.
		float direction = Input.GetAxis(MoveLeft, MoveRight);
		Vector2 velocity = Velocity;

		if (_dashDurationTimer > 0)
		{
			_dashDurationTimer -= dt;
		}

		if (_dashCooldownTimer > 0)
		{
			_dashCooldownTimer -= dt;
		}

		if (!IsOnFloor())
		{
			// GetGravity() returns the project gravity (Project Settings > Physics > 2D > Default Gravity),
			// plus any Area2D that overrides it, e.g. a low-gravity zone added later.
			Vector2 gravity = GetGravity();
			if (velocity.Y > 0)
			{
				gravity *= FallGravityMultiplier;
			}
			velocity += gravity * dt;
			velocity.Y = Mathf.Min(velocity.Y, MaxFallSpeed);
		}

		if (Input.IsActionJustPressed(Jump) && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			_sprite.Play(AnimJumpStart);
		}
		if (Input.IsActionJustReleased(Jump) && velocity.Y < 0)
		{
			velocity.Y *= JumpCutMultiplier;
		}

		if (Input.IsActionJustPressed(Dash)
			&& IsOnFloor()
			&& _dashDurationTimer <= 0
			&& _dashCooldownTimer <= 0
			&& direction != 0)
		{
			velocity.X = direction * Speed * DashSpeedMultiplier;
			_sprite.Play(AnimDash);
			_dashDurationTimer = DashDuration;
			_dashCooldownTimer = DashCooldown;
		}

		if (_dashDurationTimer <= 0)
		{
			velocity.X = direction * Speed;
		}

		Velocity = velocity;
		MoveAndSlide();

		UpdateAnimation(direction);
	}

	private void UpdateAnimation(float direction)
	{
		if (direction != 0)
		{
			// The sheets face right, so mirror the sprite when moving left.
			_sprite.FlipH = direction < 0;
		}

		bool onFloor = IsOnFloor();
		bool justLanded = onFloor && !_wasOnFloor;
		_wasOnFloor = onFloor;

		if (!onFloor)
		{
			// Let the take-off finish before switching to the in-air pose.
			if (_sprite.Animation == AnimJumpStart && _sprite.IsPlaying())
			{
				return;
			}
			_sprite.Play(Velocity.Y < 0 ? AnimRise : AnimFall);
			return;
		}

		if (_sprite.Animation == AnimDash && _sprite.IsPlaying())
		{
			return;
		}

		if (direction != 0)
		{
			_sprite.Play(AnimMove);
			return;
		}

		if (justLanded)
		{
			_sprite.Play(AnimLand);
			return;
		}

		// Let the landing finish before going back to idle.
		if (_sprite.Animation == AnimLand && _sprite.IsPlaying())
		{
			return;
		}
		_sprite.Play(AnimIdle);
	}
}
