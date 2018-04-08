using UnityEngine;


public class EnemyBullet : MonoBehaviour
{
	//public GameObject BOOM; // Объект, который спаунится после взрыва
	public int Damage; // Урон, который мы нанесём
	public float Power, LifeTime; // Скорость и время жизни снаряда
	private Vector2 _Dir;
	private GameObject Hero;
	void Start ()
	{
		Destroy(gameObject, LifeTime); // Говорим, что этот объект уничтожится через установленное время
		Hero= GameObject.Find("hero");
	}
	void FixedUpdate ()
	{
		if (transform.position.x-Hero.transform.position.x >= 0)
			_Dir = Vector2.left;
		else
			_Dir = Vector2.right;
		gameObject.GetComponent<Rigidbody2D> ().AddForce (_Dir*Power);
	}
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player") // Если объект с которым мы столкнулись имееттэг Enemy
		{
			collision.GetComponent<PlayerControl>().Hurt(Damage); // Вызываем метод урона и говорим сколько урона
			//Instantiate(BOOM, transform.position, transform.rotation); // Спауним объект, который симулирует взрыв
			Destroy(gameObject); // Уничтожаем пулю
		}
	}
}


