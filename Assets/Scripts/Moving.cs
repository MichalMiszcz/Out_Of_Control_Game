using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    float moveHorizontal, moveVertical, time123 = 0f, attackTime = 0f;
    
    public static float stressLevel = 1;

    public float speed = 2f, jumpForce = 10f;

    bool canJump = true, canPress = true, crazy = false, canMove = true, gettingDamage = false;

    int jump = 1, life = 5;

    Animator playerAnim;

    public GameObject stresLevelUI, DeadMenu, lifeUI, damageSound, jumpSound, particles;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();

        DeadMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");

        if(moveHorizontal > 0)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

            playerAnim.SetFloat("Speed123", 1f);
        }
        else if(moveHorizontal < 0)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 180f, 0);

            playerAnim.SetFloat("Speed123", -1f);
        }

        time123 += Time.deltaTime;

        if(time123 >= 0.2499f)
        {
            playerAnim.SetBool("isJumping", false);
        }

        stressLevel += Time.deltaTime;

        if(stressLevel <= 100f)
        {
            stresLevelUI.transform.localScale = new Vector3(stressLevel / 100, 1, 1);
        }

        int numer = 0;

        if(stressLevel >= 100f)
        {
            numer++;

            gameObject.GetComponent<Rigidbody2D>().gravityScale = -0.15f;

            canJump = false;

            Shooting123.canShoot = false;

            playerAnim.SetBool("isJumping", true);

            canMove = false;
        }

        if(stressLevel >= 103f)
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
        }

        if(stressLevel >= 105f)
        {
            crazy = true;
        }

        if(crazy == true)
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = true;

            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f)) * 200f);

            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;

            stressLevel = 1f;

            playerAnim.SetBool("isJumping", false);

            numer = 0;

            crazy = false;
        }

        if (life >= 0)
        {
            lifeUI.transform.localScale = new Vector3((float)life/5, 1, 1);
        }

        if(life <= 0)
        {
            DeadMenu.SetActive(true);

            Instantiate(particles, gameObject.transform.position, gameObject.transform.rotation);

            Destroy(gameObject);
        }

        if(gettingDamage == true)
        {
            attackTime += Time.deltaTime;
        }
        else
        {
            attackTime = 0f;
        }

        if(attackTime >= 0.1f)
        {
            life--;

            stressLevel += 5f;

            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100f));

            attackTime = -0.9f;

            Instantiate(damageSound, gameObject.transform.position, gameObject.transform.rotation);
        }

        if(Input.GetButton("Cancel"))
        {
            Application.LoadLevel(0);
        }
    }

    private void FixedUpdate()
    {
        if(canMove == true)
        {
            gameObject.transform.Translate(new Vector2(Mathf.Abs(moveHorizontal) * speed * Time.fixedDeltaTime, 0f));
        }

        if (Input.GetButton("Jump") && canJump == true && canPress == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce * jump));

            Instantiate(jumpSound, gameObject.transform.position, gameObject.transform.rotation);

            canPress = false;

            time123 = 0f;

            playerAnim.SetBool("isJumping", true);

            jump++;

            stressLevel += 5f;
        }

        if(Input.GetButtonUp("Jump"))
        {
            canPress = true;
        }

        if(jump >= 2)
        {
            canJump = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            jump = 1;

            canJump = true;

            canMove = true;

            Shooting123.canShoot = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyWeapon")
        {
            gettingDamage = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kolec")
        {
            life = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gettingDamage = false;
    }
}
