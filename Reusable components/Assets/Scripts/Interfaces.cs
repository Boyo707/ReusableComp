using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMovements
{
    void MoveInput(Vector2 direction, bool sprinting = false);
}

interface IJump // double jump als 2e class
{
    bool onGround { get; }
    void JumpInput(bool jumpDown, bool jumpHold = false);
    void GroundCheck();    
}

interface IAttack
{
    void Attack(bool attack, bool facingLeft);
}

interface IProjectile
{
    void ProjectileSettings(float speed, bool facingLeft, float arch);
}

interface ITrajectory
{
    void facingLeft(bool facingLeft);
}

interface IHealth
{
    void TakeDamage(int damage);
    void OnDeath();
}


interface IEnemy
{

}

interface IPlayer
{

}
