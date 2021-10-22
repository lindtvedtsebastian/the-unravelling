using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBuilding : BaseBuilding {
    protected override void OnShouldDestroy() {
        Debug.Log("Hei");
        base.OnShouldDestroy();
    }
}