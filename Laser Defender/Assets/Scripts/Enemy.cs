using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 0.1f;
    [SerializeField] AudioClip dieClip;
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0,1)] float dieClipVolume = 0.7f; 
    [SerializeField][Range(0, 1)] float shootingClipVolume = 0.7f;
    [SerializeField] int scoreValue = 69;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootingClip, Camera.main.transform.position, shootingClipVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(dieClip, Camera.main.transform.position, dieClipVolume);
            Destroy(gameObject, durationOfExplosion);
            FindObjectOfType<GameSession>().AddScore(scoreValue);
        }
    }
}
