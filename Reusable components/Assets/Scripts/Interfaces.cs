using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMovements
{
    void MoveInput(Vector2 direction, bool sprinting = false);
}

interface IJump
{
    bool onGround { get; }
    void JumpInput(bool jumpDown, bool jumpHold = false);
    void GroundCheck();    
}

interface IProjectile
{
    void Projectile(float Range, float fallDistance, float arch, bool bounce);
}
