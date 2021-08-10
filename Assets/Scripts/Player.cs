using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    //params
    [Header("PlayerMovement")]
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float padding = 1f;
    [SerializeField] int playerHealth = 200;

    [Header("Laser")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float laserFiringPeriod = 5f;
    [SerializeField] AudioClip playerDeathSound;
    [SerializeField] AudioClip playerLaserSound;
    SceneLoader sceneLoader;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationExplosion = 1f;
    [SerializeField] GameObject heart1;
    [SerializeField] GameObject heart2;

    Coroutine fireCoroutine;

    //cached ref


    //states

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    


    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundries();
        sceneLoader = FindObjectOfType<SceneLoader>();
        
        // Player nereye gidebilir nereye gidemez Limitleme işlemi. 

    }

    

    // Update is called once per frame
    void Update() // Movementlar update kısmında olmalı.
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            fireCoroutine = StartCoroutine(FireContinuously());  // Basılı tutunca Coroutine başlıyor.

        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);  // Tuşu salınca coRoutine bitiyor. 
        }


    }

    IEnumerator FireContinuously()
    {
        while (true)  // eğer devam ediyorsa basılı tutmaya..
        {
            GameObject laser = Instantiate
                (laserPrefab,
               transform.position,
               Quaternion.identity)  // Player nerdeyse oraya konumlandır kodu.
                as GameObject;
            AudioSource.PlayClipAtPoint(playerLaserSound, Camera.main.transform.position);


            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);  //ProjectileSpeed burada lazerin gidiş hızını ayarlar.

            yield return new WaitForSeconds(laserFiringPeriod);   //Bu method coroutine içerir. Yani şartlar karşılana kadar bir diğerini yapmamayı gösterir. Bu durumda laserFiringPeriod'u bekler.
        }                                                           // laserFiringPeriod ise hangi sıklıkla gideceğini ayarlamızı sağlar. Yani ne kadar sürede bir tetiklenmesi boşluğa basılı tutarken.
        

    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed; // time.deltaTime fps limitini ortadan kaldırır . Her bilgisayarda aynı şekilde hareket eder. Ama çok yavaş olduğu için variable ile çarptık.
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);   //EZBERLE!!  Mathf.Clamp(hareket değeri, min limit, max limit)
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);   //transform.position == şuanda bulunduğu pozisyon ve buna eklemeler yaparak pozisyonunu değiştiriyoruz.

        transform.position = new Vector2(newXPos, newYPos);

    }

    private void SetUpMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;   //// 0,0,0 = x,y,z sıralaması Buraları belirleyip ardından üstte Mathf.Clamp methoduyla kenarları limitledik.
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;


    }

    private void OnTriggerEnter2D(Collider2D collision) // 
    {
        // Hasar almasını sağladık sayı olarak. 
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) { return; }
        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        playerHealth -= damageDealer.GetDamage();  // playerHealth = playerHealth - damageDealer.GetDamage();
        damageDealer.Hit();


        if (playerHealth <= 100)
        {
            Destroy(heart2);


        }


        if (playerHealth <= 0)
        {
            
            Destroy(heart1);

            Die();

            




        }
    }

    private void Die()
    {
        sceneLoader.LoadGameOver();
        Destroy(gameObject, 0.1f);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationExplosion);
        AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position);

    }
}
