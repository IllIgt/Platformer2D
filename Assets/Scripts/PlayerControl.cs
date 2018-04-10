using UnityEngine;
using System.Collections;

public class PlayerControl: MonoBehaviour{
	public float Acceleration; // Скорость движения, а в дальнейшем ускорение
	public float forceOfJump;
	public int BombReloadTime, Health = 5000;
	public GameObject Bullet,StartBullet, Bomb, BombPosition; // Наш снаряд которым будем стрелять и точка, где он создаётся
	Vector3 Dir = new Vector3 (0 , 0 ,0); // Направление движения
	private bool _bombIsReload = true;
	private Animator anim;
	private bool isFacingRight = true;
    private bool isGrounded = false;
    public Transform groundCheck;
    private float groundRadius = 5f;
    public LayerMask whatIsGround;
    public static PlayerControl Player;
    public AudioClip impact;


    public bool Fasing { get { return isFacingRight; } }
    public int health{get{return Health;}}

	void Start()
	{
        Player = this;
        anim = GetComponent<Animator>();
	}
	void FixedUpdate()
	{
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", isGrounded);
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
        if (!isGrounded)
            return;

        float move = Input.GetAxis("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs(move));
		if(move > 0 && !isFacingRight)

			Flip();
		else if (move < 0 && isFacingRight)
			Flip();
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) // Если нажата стрелка вправо или D
			GetComponent<Rigidbody2D>().AddForce(Vector2.right*Acceleration);
		else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) // Иначе, если нажата стрелка влево или A
			GetComponent<Rigidbody2D>().AddForce(Vector2.left * Acceleration);
		else // Если у нас ничего не нажато
			Dir.x = 0; // Стоим на месте
		transform.position += Dir * Time.deltaTime; // Передаём нашему transform движение
        if (Input.GetButtonDown("Fire1") && GameObject.FindWithTag("Bullet") == null) // Если нажата кнопка выстрела
        {
            AudioSource.PlayClipAtPoint(impact, transform.position);
            Instantiate(Bullet, StartBullet.transform.position, transform.rotation);
        } // Создаём Bullet в точке StartBullet
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            anim.SetBool("Ground", false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 600) * forceOfJump);
        }
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

	public void Hurt(int Damage)
	{
		Health -= Damage; 
		if (Health <= 0) 
			Die();
	}
	void Die()
	{
		Destroy(gameObject);
	}
	private void Flip()
	{
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}