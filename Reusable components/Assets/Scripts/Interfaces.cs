using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMovements
{
    void MoveInput(float direction ,bool sprinting = false);
}

interface IJump // double jump als 2e class
{
    void JumpInput(bool isGrounded, bool jumpDown, bool jumpHold = false);
}

interface IAttack
{
    void Attack(bool attack, bool facingLeft);
}

interface IProjectile
{
    GameObject itParent { get; set; }
    void setParent(GameObject parent);
}

interface ITrajectory
{
    void facingLeft(bool facingLeft);
}

interface IHealth
{
    int HealthInt { get; }
    void TakeDamage(int damage, bool spriteFlippedX);
    bool knocked { get; }
    //void OnDeath();
}


interface IEnemy
{

}

interface IPlayer
{

}
