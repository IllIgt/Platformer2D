using UnityEngine;
using System.Collections;

public class MineBomb : MonoBehaviour
{
	public int DamageForce = 1000;
	public float DamageRadius = 2f;
	public float WaitingTime = 5f;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(BombDetanation());
	}
	
	// Update is called once per frame
	void FixUpdate ()
	{
		
	
	}


	IEnumerator BombDetanation()
	{
		yield return new WaitForSeconds(WaitingTime);
		BombExplode();
	}

	void BombExplode()
	{
		Collider2D[] allColliders = Physics2D.OverlapCircleAll(transform.position, DamageRadius);
		foreach (Collider2D enemyCollider in allColliders) 
		{
			Rigidbody2D rBody = enemyCollider.GetComponent<Rigidbody2D>();

				if(rBody!=null)
				{
					Vector3 bombVector = rBody.transform.position - transform.position;
					rBody.AddForce (bombVector*DamageForce);
					if(rBody.tag == "Enemy")
					{
					rBody.gameObject.GetComponent<MyEnemy> ().Hurt((int)(DamageForce*1.5));
					}
				}

		}
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy") // Если объект с которым мы столкнулись имеет тэг Enemy
		{
			BombExplode ();
		}
	}
}
