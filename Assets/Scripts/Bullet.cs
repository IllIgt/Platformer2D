using UnityEngine;


public class Bullet : MonoBehaviour
{
	//public GameObject BOOM; // Объект, который спаунится после взрыва
	public int Damage; // Урон, который мы нанесём
	public float Speed, LifeTime; // Скорость и время жизни снаряда
	Vector3 Dir = new Vector3 (0,0,0); // Направление полёта
	Vector3 knifeRotation = new Vector3 (0,0,-10);
    private GameObject Player;
    void Start()
    {
        Dir.x = Speed; // Говорим, что всегда летим по оси х
        Destroy(gameObject, LifeTime); // Говорим, что этот объект уничтожится через установленное время
        Player = GameObject.Find("hero");


    }
    void FixedUpdate()
    {
        transform.Rotate(knifeRotation);
        if (Player.GetComponent<PlayerControl>().Fasing)
        {
            transform.position += Dir;// Движение снаряда
            knifeRotation = new Vector3(0, 0, -10);
        }
        else
        {
            transform.position -= Dir;
            knifeRotation = new Vector3(0, 0, 10);
        }
        Debug.Log("Jg jg" + transform.position);

	}
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy") // Если объект с которым мы столкнулись имееттэг Enemy
		{
			collision.GetComponent<MyEnemy>().Hurt(Damage); // Вызываем метод урона и говорим сколько урона
			//Instantiate(BOOM, transform.position, transform.rotation); // Спауним объект, который симулирует взрыв
			Destroy(gameObject); // Уничтожаем пулю
		}
		else if(collision.gameObject.tag == "Turrel")
		{
			collision.gameObject.GetComponent<Turrel>().Hurt(Damage);
			Destroy(gameObject);
		}
	}
}

