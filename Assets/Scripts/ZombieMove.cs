using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    private ObjectPool objectPool;

    public float moveSpeed = 1.5f;
    public float jumpForce = 8f; // 점프 높이 조절
    public float moveAfterJump = 5f; // 점프 후 왼쪽 이동 거리
    public float pushForce = 4000f; // 착지 시 아래 몬스터가 밀리는 힘
    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isPushing = false;
    Vector3 currentPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2f; // 중력 유지
        objectPool = FindObjectOfType<ObjectPool>(); // ObjectPool 찾기
        if (gameObject.CompareTag("Zombie"))
            currentPos.z = -1;
        else if(gameObject.CompareTag("ZombieB"))
            currentPos.z = -2;
        else if(gameObject.CompareTag("ZombieC"))
            currentPos.z = -3;
        transform.position = new Vector3(transform.position.x, transform.position.y, currentPos.z);
    }

    private void Update()
    {
        // 왼쪽으로 이동 (점프 중이 아닐 때만)
        if (!isJumping)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, currentPos.z);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag(collision.gameObject.tag))
        {
            float xDifference = transform.position.x - collision.transform.position.x;
            float yDifference = transform.position.y - collision.transform.position.y;

            if (xDifference > 0 && yDifference < 0.1f && yDifference > -0.5f && !isJumping && (Random.value < 0.7f)) // 내가 오른쪽에 있고 점프하지 않은 상태
            {
                StartCoroutine(JumpOver(collision.gameObject));
            }
            else
            {
                foreach (ContactPoint2D contact in collision.contacts) //누군가가 나의 윗면과 닿았다면
                {
                    if ((contact.normal.y > 0.5f) && !isPushing)
                    {
                        //StartCoroutine(PushR(collision.gameObject));
                    }
                }
            }
        }
    }

    private System.Collections.IEnumerator JumpOver(GameObject otherMonster)
    {
        isJumping = true;
        rb.velocity = new Vector2(0, jumpForce); // 위로 점프
        yield return new WaitForSeconds(0.2f); // 최고점 도달 대기

        rb.velocity = new Vector2(-moveAfterJump, rb.velocity.y);   // 왼쪽으로 이동하면서 자연스럽게 착지

        yield return new WaitForSeconds(0.4f); // 착지 직전 대기
        rb.gravityScale = 100f; // 빠른 착지 위해 중력 조절

        rb.velocity = new Vector2(-3, 3); // 한번더 대각선으로 점프

        yield return new WaitForSeconds(0.6f); // 착지 시간
        rb.gravityScale = 2f;
        isJumping = false;
    }

    IEnumerator PushR(GameObject otherMonster)
    {
        Rigidbody2D rbB = otherMonster.gameObject.GetComponent<Rigidbody2D>();
        if (rbB != null)
        {
            isPushing = true;
            yield return new WaitForFixedUpdate();
            //rbB.AddForce(new Vector2(pushForce, 0), ForceMode2D.Force);
            //Vector2 targetPosition = rbB.position + new Vector2(pushForce, 0); // 오른쪽으로 이동
            yield return new WaitForSeconds(0.5f);
            isPushing=false;
        }
    }

    private void Dead()
    {
        objectPool.ReturnMonster(gameObject);
    }
}
