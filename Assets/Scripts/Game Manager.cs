using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject block; // Block đưa vào
    public float maxX; // Giới hạn block thả xuống trên trục X
    public Transform spawnPoint; // Điểm Spawnpoint đưa vào
    private float spawnRate = 1.5f; //Tần số xuất hiện block
    private float minSpawnRate = 0.4f;

    bool gameStarted = false;

    public GameObject tapText;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI finalScore;

    private int score = 0;

    private void Start()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.backgroundPlay, 1, true);
        AudioManager.instance.StopSound(AudioManager.instance.backgroundPlay);
        AudioManager.instance.PlaySound(AudioManager.instance.backgroundSound, 1, true);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !gameStarted)
        {
            scoreText.gameObject.SetActive(true);
            AudioManager.instance.StopSound(AudioManager.instance.backgroundSound);
            AudioManager.instance.PlaySound(AudioManager.instance.backgroundPlay, 0.7f, true);
            StartSpawnBlock(spawnRate);
            gameStarted = true;
            tapText.SetActive(false);
        }
    }   

    private void SpawnBlock()
    {
        Vector3 spawnPos = spawnPoint.position;

        spawnPos.x = Random.Range(-maxX, maxX);

        Instantiate(block, spawnPos, Quaternion.identity);

        score++;
        scoreText.text = score.ToString();
        finalScore.text = "Your score: " + score.ToString();

        // Giảm thời gian spawnRate sau mỗi lần spawn
        spawnRate = Mathf.Max(spawnRate - 0.01f, minSpawnRate);
        // Gọi lại hàm StartSpawnBlock để cập nhật thời gian giữa các lần gọi SpawnBlock
        StartSpawnBlock(spawnRate);
    }

    private void StartSpawnBlock(float spawnRate)
    {
        // Hủy bỏ lần gọi trước nếu có
        CancelInvoke("SpawnBlock");
        // Gọi hàm SpawnBlock sau mỗi spawnRate giây
        InvokeRepeating("SpawnBlock", spawnRate, spawnRate);
    }

}
