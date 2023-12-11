using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
	public float speed = 1f;
	public float wallAware = 0.5f;
	public LayerMask groundLayer;
	public float playerAware = 3f;
	public float aimingTime = 0.5f;
	public float shootingTime = 1.5f;
	public float minX;
	public float maxX;
	public float waitingTime = 2f;

	private AudioSource _audio;

	private Rigidbody2D _rigidbody;

	private GameObject _target;
	private Animator _animator;
	private Weapon _weapon;

	private Vector2 _movement;
	private bool _facingRight;
	private bool _isAttacking;

	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
		_weapon = GetComponentInChildren<Weapon>();
		_audio = GetComponent<AudioSource>();
	}


    // Start is called before the first frame update
    void Start()
    {
		if(transform.localScale.x < 0f)
		{
			_facingRight = false;
		}
		else if(transform.localScale.x > 0f)
		{
			_facingRight = true;
		}
	}

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.right;

		if(_facingRight == false)
		{
			direction = Vector2.left;
		}

		if(_isAttacking == false)
		{
			if(Physics2D.Raycast(transform.position, direction, wallAware, groundLayer))
			{
				Flip();
			}
		}
    }

	private void FixedUpdate()
	{
		float horizontalVelocity = speed;

		if(_facingRight == false)
		{
			horizontalVelocity = horizontalVelocity * -1f;
		}

		if(_isAttacking)
		{
			horizontalVelocity = 0f;
		}

		_rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
	}

	private void LateUpdate()
	{
		_animator.SetBool("Idle", _rigidbody.velocity == Vector2.zero);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if(_isAttacking == false && collision.CompareTag("Player") && gameObject.activeInHierarchy)
		{
			StartCoroutine("AimAndShoot");
		}
	}

	private void Flip()
	{
		_facingRight = !_facingRight;
		float localScaleX = transform.localScale.x;
		localScaleX = localScaleX * -1f;
		transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
	}

	private IEnumerator AimAndShoot()
	{
		_isAttacking = true;
		yield return new WaitForSeconds(aimingTime);

		_animator.SetTrigger("Shoot");

		yield return new WaitForSeconds(shootingTime);

		_isAttacking = false;
	}

	void CanShoot()
	{
		if(_weapon != null)
		{
			_weapon.Shoot();
		}

		//_audio.Play();
	}

	private void OnEnable()
	{
		_isAttacking = false;
	}

	private void OnDisable()
	{
		StopCoroutine("AimAndShoot");
		_isAttacking = false;
	}

	
	
}

/*
private void UpdateTarget()
	{
		// If first time, create target in the left
		if (_target  == null) {
			_target = new GameObject("Target");
			_target.transform.position = new Vector2(minX, transform.position.y);
			transform.localScale = new Vector3(-1, 1, 1);
			return;
		}

		// If we are in the left, change target to the right
		if (_target.transform.position.x == minX) {
			_target.transform.position = new Vector2(maxX, transform.position.y);
			transform.localScale = new Vector3(1, 1, 1);
		}

		// If we are in the right, change target to the left
		else if (_target.transform.position.x == maxX) {
			_target.transform.position = new Vector2(minX, transform.position.y);
			transform.localScale = new Vector3(-1, 1, 1);
		}
	}

	private IEnumerator PatrolToTarget()
	{
		// Coroutine to move the enemy
		while(Vector2.Distance(transform.position, _target.transform.position) > 0.05f) {
			//Actualizar Animator
			_animator.SetBool("Idle", false);

			// let's move to the target
			Vector2 direction = _target.transform.position - transform.position;
			float xDirection = direction.x;

			transform.Translate(direction.normalized * speed * Time.deltaTime);

			// IMPORTANT
			yield return null;
		}

		// At this point, i've reached the target, let's set our position to the target's one
		Debug.Log("Target reached");
		transform.position = new Vector2(_target.transform.position.x, transform.position.y);
		UpdateTarget();
		//Actualizar Animacion
		_animator.SetBool("Idle", true);

		//Disparar
		_animator.SetTrigger("Shoot");

		

		// And let's wait for a moment
		Debug.Log("Waiting for " + waitingTime + " seconds");
		yield return new WaitForSeconds(waitingTime); // IMPORTANT

		// once waited, let's restore the patrol behaviour
		Debug.Log("Waited enough, let's update the target and move again");
		
		StartCoroutine("PatrolToTarget");
	}


	
	*/
