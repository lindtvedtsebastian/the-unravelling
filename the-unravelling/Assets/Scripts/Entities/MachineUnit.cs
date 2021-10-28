using UnityEngine;

/// <summary>
/// Machine unit for `Machine` prefab. See Unity inspector for variables attached to this object.
/// </summary>
public class MachineUnit : BaseUnit, IClickable {
    public void OnDamage() {
        health -= 50;
    }
}
