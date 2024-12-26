using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Player player;
    private Image healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        healthSlider = transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHealth()
    {
        float sliderPercent = (float)player.MaxHealth / player.currentHealth;
        healthSlider.fillAmount = sliderPercent;
    }

    //void UpdateExp()
    //{
    //    float sliderPercent = (float)GameManager.Instance.playerStats.characterData.currentExp / GameManager.Instance.playerStats.characterData.baseExp;
    //    expSlider.fillAmount = sliderPercent;
    //}
}
