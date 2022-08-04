using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    public GameObject player;
    public GameObject dieEffect;
    public float speed;
    private float distance;


    public int maxHealth = 5;
    private int currHealth;

    public Animator animator;

    public HealthBar healthBar;
    

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currHealth);
     
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void TakeDamge(int damage)
    {
        currHealth -= damage;

        animator.SetTrigger("Hurt");
        healthBar.UpdateHealthBar(maxHealth, currHealth);

        if (currHealth <= 0)
        {
            Die();
            Instantiate(dieEffect, transform.position, Quaternion.identity);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
