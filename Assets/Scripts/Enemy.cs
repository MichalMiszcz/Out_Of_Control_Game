using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;

    float distanceX, distanceY;

    public float x, y, speed;

    int life = 3;

    public GameObject particles, sound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distanceX = gameObject.transform.position.x - player.transform.position.x;

        distanceY = gameObject.transform.position.y - player.transform.position.y;

        if(life <= 0)
        {
            Instantiate(particles, gameObject.transform.position, gameObject.transform.rotation);            

            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100f));

            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(Mathf.Abs(distanceX) < x && Mathf.Abs(distanceY) < y && Mathf.Abs(distanceX) > 1f)
        {
            gameObject.transform.Translate(new Vector3(speed * Time.fixedDeltaTime, 0, 0));
        }

        if(distanceX < 0)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if(distanceX > 0)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 180f, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Instantiate(sound, gameObject.transform.position, gameObject.transform.rotation);

            life--;
        }

        if(collision.gameObject.tag == "Kolec")
        {
            life = 0;
        }
    }
}
