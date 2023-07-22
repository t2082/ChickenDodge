using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private float moveSpeed = 9f;
    Rigidbody2D rb;
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public GameObject gameOver;
    public TextMeshProUGUI score;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Lấy Rigibody của Player
        StartCoroutine(IncreaseMoveSpeedOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        //Điều kiện nếu chuột click thì sẽ di chuyển
        if (Input.GetMouseButton(0))
        {
            //Lấy tọa độ chuột
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Nếu tọa độ x khi chuột dang click < 0 -> sang trái
            if (touchPos.x < 0)
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            }
            else
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 0, transform.eulerAngles.z);
            }
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -10);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

    }

    //Bất cứ khi nào xảy ra va chạm
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Block") // Nếu va chạm với tag tên "Block"
        {
            Time.timeScale = 0;
            score.gameObject.SetActive(false);
            AudioManager.instance.StopSound(AudioManager.instance.backgroundPlay);
            gameOver.SetActive(true);
            AudioManager.instance.PlaySound(AudioManager.instance.hurtSound, 1);

        }else if(collision.gameObject.tag == "Wall") { 
            AudioManager.instance.PlaySound(AudioManager.instance.chickSound, 0.1f);
        }
    }

    private IEnumerator IncreaseMoveSpeedOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);
            moveSpeed += 0.1f;
        }
    }


    public void resetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0); // Tải lại trò chơi
    }
}
