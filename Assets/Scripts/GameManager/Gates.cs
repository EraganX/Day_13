using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameManager.Instance.survive += 1;
            if (GameManager.Instance.survive>10)
            {
                GameManager.Instance.EnableGameOver();
                Time.timeScale = 0;
            }
            collision.gameObject.SetActive(false);
        }
    }
}
