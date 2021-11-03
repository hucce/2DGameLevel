using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed = 0.5f;

    public float playerJump = 100;

    private bool jump = false;

    // 1프레임 단위로 실행
    private void Update()
    {
        Move();

        Jump();
    }

    private void Move()
    {
        // 키보드의 입력 값을 받은 float 값
        float h = Input.GetAxis("Horizontal");

        // 아무것도 안눌렀다. 0, 오른쪽 방향키 > +1, 왼쪽 방향키 > -1

        // 벡터3 새로 만들고
        Vector3 vector = new Vector3();
        // 벡터의 X의 값에 1을 넣는다.

        // 각 프레임 당 걸리는 시간 1,0,0
        vector.x = h * playerSpeed * Time.deltaTime;
        // 60초에 60프레임 > 1프레임당 1초

        // 트렌스폼 변환?
        transform.Translate(vector);

        // h가 입력값 음수면 왼쪽키를 눌렀다.
        if (h < 0)
        {
            // 왼쪽키를 눌렀을 때 > 이동
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
            // 이동 없음
            GetComponent<Animator>().SetBool("Run", false);
        }
        else
        {
            // 오른쪽키를 눌렀을 때 > 이동
            // 반대로
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
        // 플레이어가 점프상태가 아닐 때
        if(jump == false)
        {
            // 인풋매니저에서 Jump라고 하는 이름의 인풋을 가져와서 그 키가 눌렸다면
            if (Input.GetButtonDown("Jump") == true)
            {
                // Y의 playerJump값 만큼 물리적으로 힘을 주도록 함
                Vector2 vector2 = new Vector2(0, playerJump);
                GetComponent<Rigidbody2D>().AddForce(vector2);

                // 점프가 되었으니 점프 상태를 true로 바꿈
                jump = true;

                // 이동상태를 false로 바꾸고, 점프상태를 true로 바꾼다.
                GetComponent<Animator>().SetBool("Run", false);
                GetComponent<Animator>().SetBool("Jump", true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌의 테그가 플렛폼이라면
        if(collision.collider.tag == "Platform")
        {
            // 땅에 충돌하면 jump 상태를 false로 바꾼다.
            jump = false;

            // 땅에 충돌하면 애니메이터의 Jump 상태를 바꾼다.
            GetComponent<Animator>().SetBool("Jump", false);
        }
    }
}