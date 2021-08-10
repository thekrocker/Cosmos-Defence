using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float enemyHealth = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laserBeam;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationExplosion = 1f;
    [SerializeField] AudioClip enemyLaserSound;
    [SerializeField] AudioClip enemyDeathSound;
    [SerializeField] [Range(0, 1)] float deathVolume = 1f;
    [SerializeField] int scoreValue = 30;

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

        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
            laserBeam,
            transform.position,
            Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        PlayEnemyLaserSound();
    }

    private void PlayEnemyLaserSound()
    {
        AudioSource.PlayClipAtPoint(enemyLaserSound, Camera.main.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hasar almasını sağladık sayı olarak. 
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) { return; }

        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        enemyHealth -= damageDealer.GetDamage();  // enemyHealth = enemyHealth - damageDealer.GetDamage();
        damageDealer.Hit();

        if (enemyHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {

        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);

        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationExplosion);
        AudioSource.PlayClipAtPoint(enemyDeathSound, Camera.main.transform.position, deathVolume);





    }
}
