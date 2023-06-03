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

    float _health = 100;
    public float Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
            healthController.SetValue(_health);
            healthAmountText.text = _health + "/" + _maxHealth;
        }
    }

    float _maxHealth = 100;
    public float MaxHealth
    {
        get { return _maxHealth; }
        set 
        {
            _maxHealth = value;
            healthController.SetMaxValue(_health);
            healthAmountText.text = _health + "/" + _maxHealth;
        }
    }

    float _stamina;
    public float Stamina
    {
        get { return _stamina; }
        set 
        { 
            _stamina = Mathf.Clamp(value, 0, _maxStamina);
        }
    }

    float _maxStamina;
    public float MaxStamina
    {
        get { return _maxStamina; }
        set { _maxStamina = value; }
    }

    float _walkSpeed = 1;
    public float WalkSpeed
    {
        get { return _walkSpeed; }
        set { _walkSpeed = value; }
    }

    float _jumpForce = 1;
    public float JumpForce
    {
        get { return _jumpForce; }
        set { _jumpForce = value; }
    }

    float _damage = 10;
    public float Damage
    {
        get { return _damage; }
        set { _jumpForce = value; }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
