using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public Player player;
    public float speed;
    public int curHealth;

    Rigidbody2D rigid;//속력을 바꿔야하기 때문에
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,1).normalized * 30 * Time.deltaTime);
    }



    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            curHealth--;
            if (curHealth <= 0)
            {
                gameObject.SetActive(false);
            }

        }
        if (collision.gameObject.tag == "Border")
        {
            
            gameObject.SetActive(false);
        }

    }

}
