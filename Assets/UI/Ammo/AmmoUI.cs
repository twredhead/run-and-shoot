using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    PlayerAttack playerAttack;

    TMP_Text ammoText;

    void Awake() 
    {
        ammoText = GetComponent<TMP_Text>();    
    }

    void Start() 
    {
        playerAttack = FindObjectOfType<PlayerAttack>();    
    }

    void Update() 
    {
        ShowAmmoAmount();
    }

    void ShowAmmoAmount()
    {
        ammoText.text = playerAttack.AmmoAmmount.ToString();
    }
}
