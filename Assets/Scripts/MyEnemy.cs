using UnityEngine;

public class MyEnemy : MonoBehaviour
{ // Выводим всё в public, чтобы смотреть, что происходит с переменными в процессе работы.
	public int Health, AttackDamage; // Количество жизней и сила атаки
	public float MinDistance, Speed, ReloadTime; // Дистанция на которой атакует, скорость, время перезарядки.
	public GameObject Target, StartBullet; // Цель (помещаем туда игрока, когда тот подходит).
	public bool Angry; // Проверка сагрили ли мы противника
	// Проверка, смотрит ли вперед (направо) и проверка, идёт ли сейчас перезарядка
	bool IsForward = true, Couldown = false;
	Vector3 Dir = new Vector3(1, 0);
	void FixedUpdate()
	{
		// Если противник нас заметил входим в режим злости
		if (Angry)
		{
			// смотрим, где находится игрок x < 0 игрок слева, x > 0 справа
			float x = Target.transform.position.x - transform.position.x;
			// разворачиваемся в зависимости от того, где игрок и куда мы смотрим
			if (x < 0 && IsForward)
				Flip();
			else if (x > 0 && !IsForward)
				Flip();
			// Если дистанция позволяет атакуем
			if (Vector3.Distance(transform.position, Target.transform.position) <= MinDistance)
				Engage(); // Метод атаки
			// Если мы далековато, двигаемся к цели
			else
				transform.position += Dir * Speed * Time.deltaTime;
		}
		// Если мы не озлоблены завершаем работу кадра
		else
			return;
	}
	// Метод, нанесения урона противнику
	public void Hurt(int Damage)
	{
		print("Ouch: " + Damage); // Выводим в консоль сообщение о количестве урона
		Health -= Damage; // Отнимаем жизни
		if (Health <= 0) // Если жизней вдруг <= 0, умираем
			Die();
	}
	// Просто удаляемся со сцены, можно заспаунить пикапик.
	void Die()
	{
		Destroy(gameObject);
	}
	// Метод атаки
	void Engage()
	{
		// print(x);
		// Если мы не на перезарядке, то не атакуем
		if (!Couldown)
		{
			Couldown = true; // Включение перезарядки
			Invoke("Reload", ReloadTime); // Вызываем таймер перезарядки
		}
	}
	// перезарядка
	void Reload()
	{
		Couldown = false;
	}
	// Касание нашего триггера
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == 9) // Если этот триггер игрок
		{
			Target = collision.gameObject; // Делаем ссылку на игрока
			Angry = true; // Становимся агрессивными
		}
	}
	// разворот
	void Flip()
	{
		IsForward = !IsForward; // Запоминаем, что мы больше не смотрим вправо
		Dir.x *= -1; // Меняем направление движения
		Vector3 V = transform.localScale; // Берём новую структуру Vector3 и копируем её с нашего scale
		V.x *= -1; // И меняем её ось x в другую сторону
		// Присваиваем свою структуру, так как напрямую к localScale.x нам не обратиться
		transform.localScale = V;
	}
}
