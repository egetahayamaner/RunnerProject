using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Material Sky_001;
    public float moveSpeed = 5f;
    private Rigidbody rb;
    public int health = 100;
    Animator anim;
    // Start is called before the first frame update
    float speed = 0.1f;
    bool tapToStart = false;
    void OnCollisionEnter(Collision other)
    
    {
        if(other.gameObject.tag == "FinishLine")
        {
            anim.SetTrigger("isFinished");
            float clampedX = Mathf.Clamp(transform.position.x, -2f, 2f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
            tapToStart = false;
            anim.SetBool("isRunning", false);
            StartCoroutine(DelayedTeleport(2));
        }
        else if(other.gameObject.tag == "Level2"){
            RenderSettings.skybox = Sky_001;
            DynamicGI.UpdateEnvironment();
        }
    }
    void FixedUpdate()
    {
        if(tapToStart == true && health > 0) 
        {
        anim.SetBool("isRunning", true);
        transform.Translate(Vector3.forward * speed);//movement code
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0, 0) * moveSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, rb.velocity.z);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = 100;
        anim = this.GetComponent<Animator>(); 
        print(health);
    }

    // Stops the jumping animation
    void stopJump()
    {
        anim.SetBool("isJumping", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            tapToStart = true;
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isJumping", true);
        }
        else if (Input.GetKeyDown(KeyCode.D) && health > 0)
        {
            this.transform.Translate(0.01f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A) && health > 0){
            this.transform.Translate(-0.01f, 0 ,0);
        }
        
        if(health <= 0){

            anim.SetTrigger("isDead");
        }
        float clampedX = Mathf.Clamp(transform.position.x, -2f, 2f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        if (transform.position.y < -5)
        {
            anim.SetTrigger("isDead");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Spider")
        {
            health -= 50;
            PlayerPrefs.SetInt("HealthValue", health);
            print(health);

        }
        else if(other.gameObject.tag == "Fire")
        {
            health -= 25;
            PlayerPrefs.SetInt("HealthValue", health);
            print(health);
            
        }
        else if(other.gameObject.tag == "instantKill")
        {
            health -= 100;
            PlayerPrefs.SetInt("HealthValue", health);
            print(health);
        }
    }
    IEnumerator DelayedTeleport(float delayTime)
    {
        yield return new WaitForSeconds(3f);
        
        Vector3 newPosition = new Vector3(0f, 7f, 250f);
        transform.position = newPosition;
        
    }
}
