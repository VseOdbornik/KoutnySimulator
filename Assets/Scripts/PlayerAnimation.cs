using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : PlayerComponent
{
    [SerializeField] Animator head, torso, legs;
    [SerializeField] string headAnimation, torsoAnimation, legsAnimation;

    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_WALK = "Player_Walk";
    const string PLAYER_HOLD = "Player_Hold";
    const string PLAYER_HOLDWALK = "Player_HoldWalk";
    const string PLAYER_CROUCH = "Player_Crouch";
    const string PLAYER_HANG = "Player_Hang";

    void SetTorso(string newAnimation)
    {
        if (torsoAnimation == newAnimation) return;

        torso.Play(newAnimation);

        torsoAnimation = newAnimation;
    }

    void SetLegs(string newAnimation)
    {
        if (legsAnimation == newAnimation) return;

        legs.Play(newAnimation);

        legsAnimation = newAnimation;
    }

    public void Idle()
    {
        SetLegs(PLAYER_IDLE);
        SetTorso(PLAYER_IDLE);
    }    

    public void Walk()
    {
        SetTorso(PLAYER_IDLE);
        SetLegs(PLAYER_WALK);
    }

    public void Hold()
    {
        SetTorso(PLAYER_HOLD);
        SetLegs(PLAYER_HOLD);
    }

    public void HoldWalk()
    {
        SetTorso(PLAYER_HOLDWALK);
        SetLegs(PLAYER_HOLDWALK);
    }
    public void Hanging()
    {
        SetTorso(PLAYER_HANG);
        SetLegs(PLAYER_IDLE);
    }

    public void Crouch()
    {
        SetTorso(PLAYER_CROUCH);
        SetLegs(PLAYER_CROUCH);
    }
}
