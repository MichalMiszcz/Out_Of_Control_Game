using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int numerLevelu;

    public GameObject outro;

    // Start is called before the first frame update
    void Start()
    {
        outro.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(LoadLevel123(numerLevelu));
        }
    }

    IEnumerator LoadLevel123(int scene)
    {
        outro.SetActive(true);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(numerLevelu);
    }
}
