using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMovements
{
    void MoveInput(float direction ,bool sprinting = false);
}

interface IEntityController
{
    void DisableEntityControlls(float duration);
}

interface IJump // double jump als 2e class
{
    void JumpInput(bool isGrounded, bool jumpDown, bool jumpHold = false);
}

interface IAttack
{
    void Attack(bool attack);
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
    float HealthInt { get; }
    void TakeDamage(float damage);
}


interface IEnemy
{

}

interface IPlayer
{

}
