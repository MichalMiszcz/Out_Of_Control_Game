using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting123 : MonoBehaviour
{
    public GameObject gun, bullet, sound;

    float timeToShoot;

    public float speed = 0.1f;

    public static bool canShoot = true;

    private void Update()
    {
        timeToShoot += Time.deltaTime;

        if (Input.GetButton("Fire1") && timeToShoot > speed && canShoot == true)
        {
            Instantiate(bullet, gun.transform.position, gun.transform.rotation);
            Instantiate(sound, gun.transform.position, gun.transform.rotation);

            timeToShoot = 0f;

            if(Moving.stressLevel <= 95f)
            {
                Moving.stressLevel += 5f;
            }
            else
            {
                Moving.stressLevel += 2f;
            }
        }
    }
}
