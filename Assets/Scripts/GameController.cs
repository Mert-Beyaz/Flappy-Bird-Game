using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Gokyuzu1;
    public GameObject Gokyuzu2;
    public float BackGroundSpeed = -1.5f;

    Rigidbody2D rb1;
    Rigidbody2D rb2;

    float length;

    public GameObject Pipe;
    public int PipeCount;
    GameObject[] Pipes;

    float time = 0;
    bool GameEnd;

    void Start()
    {
        rb1 = Gokyuzu1.GetComponent<Rigidbody2D>();
        rb2 = Gokyuzu2.GetComponent<Rigidbody2D>();

        rb1.velocity = new Vector2(BackGroundSpeed, 0);
        rb2.velocity = new Vector2(BackGroundSpeed, 0);

        length = Gokyuzu1.GetComponent<BoxCollider2D>().size.x;

        Pipes = new GameObject[5];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = Instantiate(Pipe, new Vector2(-20, -20), Quaternion.identity);
            Rigidbody2D PipeRb = Pipes[i].AddComponent<Rigidbody2D>();
            PipeRb.gravityScale = 0;
            PipeRb.velocity = new Vector2(BackGroundSpeed, 0);
        }

    }

    void Update()
    {
        if (!GameEnd)
        {
            if (Gokyuzu1.transform.position.x <= -length)
            {
                Gokyuzu1.transform.position += new Vector3(length * 2, 0);
            }
            if (Gokyuzu2.transform.position.x <= -length)
            {
                Gokyuzu2.transform.position += new Vector3(length * 2, 0);
            }


            time += Time.deltaTime;
            if (time > 2f)
            {
                time = 0;
                float PipeTransform = Random.Range(-3.9f, -5.1f);
                Pipes[PipeCount].transform.position = new Vector3(15.5f, PipeTransform);
                PipeCount++;

                if (PipeCount >= Pipes.Length)
                    PipeCount = 0;

            }
        }
    }

    public void GameOver()
    {
        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            rb1.velocity = Vector2.zero;
            rb2.velocity = Vector2.zero;
        }
        GameEnd = true;
    }
}
