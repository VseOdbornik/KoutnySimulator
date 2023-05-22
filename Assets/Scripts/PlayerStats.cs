using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : PlayerComponent
{
    int _health = 100;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
            player.healthController.SetValue(_health);
            player.healthAmountText.text = _health + "/" + _maxHealth;
        }
    }
    int _maxHealth = 100;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set 
        {
            _maxHealth = value;
            player.healthController.SetMaxValue(_health);
            player.healthAmountText.text = _health + "/" + _maxHealth;
        }
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
