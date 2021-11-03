using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed = 0.5f;

    public float playerJump = 100;

    private bool jump = false;

    // 1������ ������ ����
    private void Update()
    {
        Move();

        Jump();
    }

    private void Move()
    {
        // Ű������ �Է� ���� ���� float ��
        float h = Input.GetAxis("Horizontal");

        // �ƹ��͵� �ȴ�����. 0, ������ ����Ű > +1, ���� ����Ű > -1

        // ����3 ���� �����
        Vector3 vector = new Vector3();
        // ������ X�� ���� 1�� �ִ´�.

        // �� ������ �� �ɸ��� �ð� 1,0,0
        vector.x = h * playerSpeed * Time.deltaTime;
        // 60�ʿ� 60������ > 1�����Ӵ� 1��

        // Ʈ������ ��ȯ?
        transform.Translate(vector);

        // h�� �Է°� ������ ����Ű�� ������.
        if (h < 0)
        {
            // ����Ű�� ������ �� > �̵�
            GetComponent<Animator>().SetBool("Run", true);
            Vector3 vector2 = new Vector3();
            vector2 = transform.localScale;

            if (vector2.x > 0)
            {
                vector2.x = -vector2.x;
            }

            transform.localScale = vector2;
        }
        else if (h == 0)
        {
            // �̵� ����
            GetComponent<Animator>().SetBool("Run", false);
        }
        else
        {
            // ������Ű�� ������ �� > �̵�
            // �ݴ��
            GetComponent<Animator>().SetBool("Run", true);
            Vector3 vector2 = new Vector3();
            vector2 = transform.localScale;

            if (vector2.x < 0)
            {
                vector2.x = -vector2.x;
            }
            transform.localScale = vector2;
        }
    }

    private void Jump()
    {
        // �÷��̾ �������°� �ƴ� ��
        if(jump == false)
        {
            // ��ǲ�Ŵ������� Jump��� �ϴ� �̸��� ��ǲ�� �����ͼ� �� Ű�� ���ȴٸ�
            if (Input.GetButtonDown("Jump") == true)
            {
                // Y�� playerJump�� ��ŭ ���������� ���� �ֵ��� ��
                Vector2 vector2 = new Vector2(0, playerJump);
                GetComponent<Rigidbody2D>().AddForce(vector2);

                // ������ �Ǿ����� ���� ���¸� true�� �ٲ�
                jump = true;

                // �̵����¸� false�� �ٲٰ�, �������¸� true�� �ٲ۴�.
                GetComponent<Animator>().SetBool("Run", false);
                GetComponent<Animator>().SetBool("Jump", true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� �ױװ� �÷����̶��
        if(collision.collider.tag == "Platform")
        {
            // ���� �浹�ϸ� jump ���¸� false�� �ٲ۴�.
            jump = false;

            // ���� �浹�ϸ� �ִϸ������� Jump ���¸� �ٲ۴�.
            GetComponent<Animator>().SetBool("Jump", false);
        }
    }
}