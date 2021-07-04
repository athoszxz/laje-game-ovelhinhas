using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Movement/Move With Arrows")]
[RequireComponent(typeof(Rigidbody2D))]
public class Move : Physics2DObject
{
	[Header("Input keys")]
	public Enums.KeyGroups typeOfControl = Enums.KeyGroups.ArrowKeys;


	[Header("Movement")]
	[Tooltip("Speed of movement")]
	public float speed = 5f;
	public Enums.MovementType movementType = Enums.MovementType.AllDirections;

	[Header("Orientation")]
	public bool orientToDirection = false;
	// The direction that will face the player
	public Enums.Directions lookAxis = Enums.Directions.Up;

	public passos passosVar;

	private Vector2 movement, cachedDirection;
	private float moveHorizontal;
	private float moveVertical;
	private Animator myAnimator;
	private Rigidbody2D myRb;
	public bool isWalking = false;

    private void Awake()
    {
		myAnimator = GetComponent<Animator>();
		myRb = GetComponent<Rigidbody2D>();
    }
    // Update gets called every frame

    void Update ()
	{	
		// Moving with the arrow keys
		if(typeOfControl == Enums.KeyGroups.ArrowKeys)
		{
			moveHorizontal = Input.GetAxis("Horizontal");
			moveVertical = Input.GetAxis("Vertical");
		}
		else
		{
			moveHorizontal = Input.GetAxis("Horizontal2");
			moveVertical = Input.GetAxis("Vertical2");
		}

		//zero-out the axes that are not needed, if the movement is constrained
		switch(movementType)
		{
			case Enums.MovementType.OnlyHorizontal:
				moveVertical = 0f;
				break;
			case Enums.MovementType.OnlyVertical:
				moveHorizontal = 0f;
				break;
		}
			
		movement = new Vector2(moveHorizontal, moveVertical);


		//rotate the GameObject towards the direction of movement
		//the axis to look can be decided with the "axis" variable
		if(orientToDirection)
		{
			if(movement.sqrMagnitude >= 0.1f)
			{
				cachedDirection = movement;
				myAnimator.SetBool("andando", true);
				isWalking = true;
			}
            else
            {
				myAnimator.SetBool("andando", false);
				isWalking = false;
			}
			Utils.SetAxisTowards(lookAxis, transform, cachedDirection);
		}
	}



	// FixedUpdate is called every frame when the physics are calculated
	void FixedUpdate ()
	{
		// Apply the force to the Rigidbody2d
		myRb.AddForce(movement * speed * 10f);
	}
}