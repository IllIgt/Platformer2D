using UnityEngine;
using System.Collections;

public class PlayerControl: MonoBehaviour{
	public float Acceleration; // Скорость движения, а в дальнейшем ускорение
	public float forceOfJump;
	public int BombReloadTime;
	public GameObject Bullet,StartBullet, Bomb, BombPosition; // Наш снаряд которым будем стрелять и точка, где он создаётся
	Vector3 Dir = new Vector3 (0 , 0 ,0); // Направление движения
	private bool _bombIsReload = true;


	void FixedUpdate()
	{

		bool isGroundChecked = Physics2D.Linecast (transform.position, transform.Find ("groundCheck").position, 1 << LayerMask.NameToLayer ("Ground"));
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) // Если нажата стрелка вправо или D
			GetComponent<Rigidbody2D>().AddForce(Vector2.right*Acceleration);
		else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) // Иначе, если нажата стрелка влево или A
			GetComponent<Rigidbody2D>().AddForce(Vector2.left * Acceleration);
		else // Если у нас ничего не нажато
			Dir.x = 0; // Стоим на месте
		if (Input.GetKey (KeyCode.W)&&isGroundChecked || Input.GetKey (KeyCode.UpArrow)&&isGroundChecked)
			GetComponent<Rigidbody2D>().AddForce(transform.up*forceOfJump);
		else
			Dir.y = 0;
		transform.position += Dir * Time.deltaTime; // Передаём нашему transform движение
		if (Input.GetButtonDown("Fire1")&&GameObject.FindWithTag("Bullet")==null) // Если нажата кнопка выстрела
			Instantiate(Bullet, StartBullet.transform.position, transform.rotation); // Создаём Bullet в точке StartBullet

		if (Input.GetKey (KeyCode.LeftAlt)&&_bombIsReload==true)
			{
				Instantiate (Bomb, BombPosition.transform.position, transform.rotation);
				StartReload ();
			}

			
	}
	IEnumerator BombReloading()
	{
		yield return new WaitForSeconds(BombReloadTime);
		_bombIsReload = true;
	} 
	void StartReload()
	{
		_bombIsReload = false;
		StartCoroutine (BombReloading());
	}

}