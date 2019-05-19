using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Player player;

    private void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        healthText.text = player.GetHealth().ToString();
    }
}
