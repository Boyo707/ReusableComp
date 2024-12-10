using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IControllerInput
{
    public float HorizontalInput { get; }
    public bool DashInput { get; }

    public bool AttackMellee { get; }
    public bool AttackProjectile { get; }
    
    public bool JumpDown { get; }
    public bool JumpHold { get; }
}
