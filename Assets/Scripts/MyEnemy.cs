using UnityEngine;

public class MyEnemy : MonoBehaviour
{ // Выводим всё в public, чтобы смотреть, что происходит с переменными в процессе работы.
	public int Health, AttackDamage; // Количество жизней и сила атаки
	public float MinDistance, Speed, ReloadTime,RayDistance; // Дистанция на которой атакует, скорость, время перезарядки.
	public GameObject Target, StartBullet; // Цель (помещаем туда игрока, когда тот подходит).
	public bool Angry; // Проверка сагрили ли мы противника
	// Проверка, смотрит ли вперед (направо) и проверка, идёт ли сейчас перезарядка
	bool IsForward = false, Couldown = false;
	Vector3 Dir = new Vector3(1, 0);
	public Vector3 FinalPatrolPoint = new Vector3 (7, 0);//Приращение к вектору патрулирования
	private Vector3 _startPosition;//Стартовая позиция Патрульного
	private Vector3 _patrolVector;//Стартовая позиции и приращения
    private Vector2 _rayVectorDir = new Vector2();
    private Vector3 _rayVectorStart = new Vector3();

	private void Start()
	{
		_startPosition.Set((float)transform.position.x,(float)transform.position.y,0);
		_patrolVector = _startPosition + FinalPatrolPoint;

	}

	void FixedUpdate()
	{
        FindHero();
        if (Angry) {
			float x = Target.transform.position.x - transform.position.x;
			ChangeDirection(x);
			if (Vector3.Distance (transform.position, Target.transform.position) <= MinDistance)
				Engage (); 
			else
				transform.position += Dir * Speed * Time.deltaTime;
		}
		else
			Patrol();
	}
	// Метод, нанесения урона противнику
	public void Hurt(int Damage)
	{
		print("Ouch: " + Damage);
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
	void Engage()
	{

		if (!Couldown)
		{
			Couldown = true; 
			Invoke("Reload", ReloadTime); 
		}
	}
	// перезарядка
	void Reload()
	{
        Couldown = false;
	}

    private void FindHero()
    {
        RaycastHit2D rkHit = Physics2D.Raycast(transform.position, Vector2.right);
        for (int y = -2; y < 10; y++)
        {
            _rayVectorStart.Set(transform.position.x,transform.position.y+(y * 0.5f), 0);
            _rayVectorDir.Set(1, 0);
            Debug.DrawRay(_rayVectorStart, Dir*RayDistance, Color.red);
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == 9) 
		{
			Target = collision.gameObject; 
			Angry = true; 
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		Angry = false;
	}
	// разворот
	void Flip()
	{
		IsForward = !IsForward; // Запоминаем, что мы больше не смотрим вправо
		Dir.x *= -1; 
		Vector3 V = transform.localScale; // Берём новую структуру Vector3 и копируем её с нашего scale
		V.x *= -1; 
		transform.localScale = V;
	}
	//Изменяем направление взгляда
	void ChangeDirection(float x)
	{
		if (x < 0 && !IsForward)
			Flip ();
		else if (x > 0 && IsForward)
			Flip ();
	}
	//изменяем вектор при патрулировании	
	void changePosition(Vector3 resultVector)
	{
		if (resultVector == _startPosition + FinalPatrolPoint) {
			Flip ();
			_patrolVector = _startPosition;
		} else 
		{
			Flip ();
			_patrolVector = _startPosition + FinalPatrolPoint;
		}
	}
	void Patrol()
	{
		float x = Mathf.Abs(_patrolVector.x - transform.position.x);
		transform.position += Dir * Speed * Time.deltaTime;
		if (x >= 0 && x < 0.2)
			changePosition (_patrolVector);
		//Если вышли за предел зоны патрулирования определяем направдение и возвращяемся на старт
		if (transform.position.x<_startPosition.x||transform.position.x>_startPosition.x+FinalPatrolPoint.x)
		{
			float z = _startPosition.x - transform.position.x;
			ChangeDirection (z);
			Vector3.MoveTowards(transform.position, _startPosition, Speed*Time.deltaTime);
		}
	}
}
