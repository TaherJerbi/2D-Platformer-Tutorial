using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    const float MAXHEALTH = 100f;
    float health;
    public Slider healthSlider;
    private void Start()
    {
        health = MAXHEALTH;
    }

    void Die()
    {
        GetComponent<CharacterConroller2D>().enabled = false;
        GetComponent<Animator>().SetBool("Dead", true);
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        if(health <= 0)
        {
            health = 0;
            Die();
        }
        healthSlider.value = health / MAXHEALTH;
        GetComponent<AudioSource>().Play();
    }
}
