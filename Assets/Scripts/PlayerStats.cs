using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : PlayerComponent
{
    [SerializeField] SliderController healthController;
    [SerializeField] TextMeshProUGUI healthAmountText;

    [SerializeField] SliderController staminaController;
    [SerializeField] TextMeshProUGUI staminaAmountText;

    int _health = 100;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
            healthController.SetValue(_health);
            healthAmountText.text = _health + "/" + _maxHealth;
        }
    }

    int _maxHealth = 100;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set 
        {
            _maxHealth = value;
            healthController.SetMaxValue(_health);
            healthAmountText.text = _health + "/" + _maxHealth;
        }
    }

    int _stamina;
    public int Stamina
    {
        get { return _stamina; }
        set 
        { 
            _stamina = Mathf.Clamp(value, 0, _maxStamina);
        }
    }

    int _maxStamina;
    public int MaxStamina
    {
        get { return _maxStamina; }
        set { _maxStamina = value; }
    }

    int _walkSpeed = 1;
    public int WalkSpeed
    {
        get { return _walkSpeed; }
        set { _walkSpeed = value; }
    }

    int _jumpForce = 1;
    public int JumpForce
    {
        get { return _jumpForce; }
        set { _jumpForce = value; }
    }
}
