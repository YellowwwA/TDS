using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    private ObjectPool objectPool;

    public float moveSpeed = 1.5f;
    public float jumpForce = 8f; // ���� ���� ����
    public float moveAfterJump = 5f; // ���� �� ���� �̵� �Ÿ�
    public float pushForce = 4000f; // ���� �� �Ʒ� ���Ͱ� �и��� ��
    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isPushing = false;
    Vector3 currentPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2f; // �߷� ����
        objectPool = FindObjectOfType<ObjectPool>(); // ObjectPool ã��
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
        // �������� �̵� (���� ���� �ƴ� ����)
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

            if (xDifference > 0 && yDifference < 0.1f && yDifference > -0.5f && !isJumping && (Random.value < 0.7f)) // ���� �����ʿ� �ְ� �������� ���� ����
            {
                StartCoroutine(JumpOver(collision.gameObject));
            }
            else
            {
                foreach (ContactPoint2D contact in collision.contacts) //�������� ���� ����� ��Ҵٸ�
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
        rb.velocity = new Vector2(0, jumpForce); // ���� ����
        yield return new WaitForSeconds(0.2f); // �ְ��� ���� ���

        rb.velocity = new Vector2(-moveAfterJump, rb.velocity.y);   // �������� �̵��ϸ鼭 �ڿ������� ����

        yield return new WaitForSeconds(0.4f); // ���� ���� ���
        rb.gravityScale = 100f; // ���� ���� ���� �߷� ����

        rb.velocity = new Vector2(-3, 3); // �ѹ��� �밢������ ����

        yield return new WaitForSeconds(0.6f); // ���� �ð�
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
            //Vector2 targetPosition = rbB.position + new Vector2(pushForce, 0); // ���������� �̵�
            yield return new WaitForSeconds(0.5f);
            isPushing=false;
        }
    }

    private void Dead()
    {
        objectPool.ReturnMonster(gameObject);
    }
}
