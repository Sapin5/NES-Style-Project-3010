using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private Vector2 boxSize = new(1f, 1f);
    [SerializeField] private Vector2 boxLoc = new(1f, 1f);
    [SerializeField] private Vector2 boxLocHead = new(1f, 1f);
    [SerializeField]private float jumpForce, doubleJumpStr = 1f;
    [SerializeField]private bool touchingGround, doubleJump;
    private Rigidbody2D physicsBody;
    private string currentAction;

    private bool dashState;

    private void Awake(){
        dashState = GetComponent<Dash>().Dashstate();
    }

    public void Jumping(){
        if(physicsBody.velocity.y!=0){
            DoubleJump();
        }

        if(touchingGround){
            if((Input.GetKey(KeyCode.UpArrow)|| Input.GetKey(KeyCode.W))
                            && dashState){
                physicsBody.velocity = new Vector2(physicsBody.velocity.x, jumpForce);
                touchingGround = false;
                doubleJump = true;
            }
        }else{
            if((Input.GetKey(KeyCode.DownArrow)|| Input.GetKey(KeyCode.S)) && physicsBody.velocity.y != 0){
                physicsBody.AddForce(Vector2.down/2, ForceMode2D.Impulse);
            }
        }

        if(physicsBody.velocity.y != 0 && dashState){
            currentAction = "Jumping";
            touchingGround = false;
        }else if(TouchGround()){
            touchingGround = true;
        }
    }

    private void DoubleJump(){
        if(!touchingGround && doubleJump){
            if((Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W)) && dashState){
                physicsBody.velocity = new Vector2(physicsBody.velocity.x, jumpForce/doubleJumpStr);
                doubleJump = false;
            }
        }
    }

    private bool TouchGround(){
        RaycastHit2D temp  = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y)-boxLoc, boxSize, 0f, Vector2.down, boxLoc.y);

        if(temp){
            if(temp.transform.CompareTag("Ground")){
                Debug.Log("Collided with " + temp.transform.name);
                return true;
                }else{
                    return false;
                }
        }else{
            Debug.Log("Collided with nothing");
            return false;
        }

    }

    void OnDrawGizmos()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y)-boxLoc, boxSize);
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y)+boxLocHead, boxSize);
    }
}
