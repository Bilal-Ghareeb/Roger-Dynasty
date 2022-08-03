using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private float distance;


    public int maxHealth = 5;
    private int currHealth;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
     
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void TakeDamge(int damage)
    {
        currHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        GetComponent<Renderer>().enabled = false;
    }

   
}
