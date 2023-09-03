using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    // attack by position or entity transform
    public bool BahaviorUpdate(Vector3 targetPosition);
    public bool BehaviorUpdate(Transform targetEntity);
    //Attack must return information useful for decision-making
    public bool isActive();
    public bool onCooldown();
    public bool stopsMovement();
}
