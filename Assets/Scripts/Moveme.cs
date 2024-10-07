using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;

public class Moveme : MonoBehaviour
{
    [SerializeField] private float fallSpeed;
    private Rigidbody2D physicsBody;
    private static bool touchingGround;
    private void Start()
    {
        physicsBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
        transform.Translate(Input.GetAxis("Horizontal") *
                                    Time.deltaTime * 10, 0, 0 );

        if((Input.GetKeyDown(KeyCode.UpArrow)|| 
                Input.GetKeyDown(KeyCode.W))&& touchingGround){
            physicsBody.AddForce(Vector2.up * fallSpeed, ForceMode2D.Impulse);
            touchingGround = false;
        }

        if((Input.GetKey(KeyCode.DownArrow)|| 
                Input.GetKey(KeyCode.S))&& !touchingGround){
            physicsBody.AddForce(Vector2.down/2, ForceMode2D.Impulse);
            Debug.Log("Going Down");
        }
        Debug.Log(touchingGround);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name == "Square"){
            touchingGround = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name == "Square"){
            touchingGround = true;
        }
    }
}
