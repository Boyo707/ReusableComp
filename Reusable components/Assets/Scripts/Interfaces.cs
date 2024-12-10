using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IEntityPhysics
{
    Rigidbody2D Rigidbody { get; }
    GroundDetection GroundDetection { get; }
    bool IsGrounded { get; }
    bool IsFacingRight { get; }
}

interface IMovements
{
    void MoveInput(float direction);
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
    GameObject ItParent { get; set; }
    void SetParent(GameObject parent);
}

interface ITrajectory
{
    void FacingLeft(bool facingLeft);
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
