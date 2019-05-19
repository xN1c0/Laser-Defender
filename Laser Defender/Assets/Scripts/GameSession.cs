using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int score = 0;

    private void Awake()
    {
        SetUpSingletons();
    }

    private void SetUpSingletons()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
