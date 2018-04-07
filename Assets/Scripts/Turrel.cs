using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrel : MonoBehaviour
{
	public float FlipTime = 2f;

	public float Health; 
	public float Damage;
	public float Cooldown;
	private Vector2 Dir = new Vector2 (1, 0);
	public float RaycastDistance;
	public float StopDistance;

	public float MaxSpeed;
	public float MoveForce;

	public GameObject Target, Bullet, StartBullet;
//	private Vector3 _targetPos;

	public bool IsAngry = false;
	public float AngryTime = 3f;
	private float _lastAngryTime;

	private Rigidbody2D _rigidbody;
	public LayerMask LayerMask;

//	private float _startPosX; 
	private bool _isReload = true;

	public Vector3 dir{get{return Dir;}}


	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
//		_startPosX = transform.position.x;

		InvokeRepeating("AutoFlip", 2f, FlipTime);
	}   

	private void AutoFlip()
	{
		if (!IsAngry && Mathf.Abs(_rigidbody.velocity.x) < 0.1f)
			Flip();
	} 

	private void FixedUpdate()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * Mathf.Sign(transform.localScale.x), RaycastDistance, LayerMask);
		if (hit.collider != null)
		{
			IsAngry = true;
			_lastAngryTime = Time.time;
			Target = hit.collider.gameObject;
//			_targetPos = hit.point;
			Debug.DrawLine(transform.position, hit.point, Color.red);
		}
		else
		{
			Debug.DrawRay(transform.position, transform.right * RaycastDistance * Mathf.Sign(transform.localScale.x), Color.green);
		}

		if (Time.time - _lastAngryTime > AngryTime)
			IsAngry = false;

		if (IsAngry)
		{
//			MoveToPos(_targetPos);

			if ((Target.transform.position - transform.position).sqrMagnitude <= StopDistance * StopDistance)
				Attack();
		}
		else
		{
//			MoveToPos(new Vector3(_startPosX, 0, 0));
		}
	}

//	private void MoveToPos(Vector3 pos)
//	{
//		float deltaX = pos.x - transform.position.x;
//
//		if (Mathf.Abs(deltaX) < 0.1f)
//		{
//			_rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
//			return;
//		}
//
//		if (Mathf.Sign(deltaX) != Mathf.Sign(transform.localScale.x))
//		{
//			Flip();
//		}
//
//		_rigidbody.AddForce(transform.right * Mathf.Sign(deltaX) * MoveForce);
//		if (Mathf.Abs(_rigidbody.velocity.x) > MaxSpeed)
//			_rigidbody.velocity = new Vector2(Mathf.Sign(deltaX) * MaxSpeed, _rigidbody.velocity.y);
//	}

	private void Flip()
	{
			Dir.x *= -1; 
			Vector3 V = transform.localScale; // Берём новую структуру Vector3 и копируем её с нашего scale
			V.x *= -1; 
			transform.localScale = V;
	}

	private void Attack()
	{
		if(_isReload)
		{
			Instantiate(Bullet, StartBullet.transform.position, transform.rotation);
			StartReload ();
		}
	}
		
	IEnumerator BulletReloading()
	{
		yield return new WaitForSeconds(Cooldown);
		_isReload = true;
	} 
	void StartReload()
	{
		_isReload = false;
		StartCoroutine (BulletReloading());
	}
	// Метод, нанесения урона противнику
	public void Hurt(int Damage)
	{
		Health -= Damage; 
		if (Health <= 0) 
			Die();
	}
	// Просто удаляемся со сцены, можно заспаунить пикапик.
	void Die()
	{
		Destroy(gameObject);
	}
	// Метод атаки
}


