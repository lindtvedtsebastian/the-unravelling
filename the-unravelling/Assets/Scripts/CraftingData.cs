using System;
using UnityEngine;


public enum CraftingType {
    Turret,
    Wall
}

public struct CraftingRecipe {
    readonly CraftTurret turret;
    readonly CraftWall wall;
}

readonly struct CraftTurret {
    readonly int stone;
    readonly int wood;

    public CraftTurret(int a = 1, int b = 2)
    {
        stone = a;
        wood = b;
    }
}

readonly struct CraftWall {
    readonly int stone;
    readonly int wood;

    public CraftWall(int a = 2, int b = 2)
    {
        stone = a;
        wood = b;
    }
}

[Serializable]
[CreateAssetMenu(fileName = "CraftingData", menuName = "Crafting/CraftingData", order = 2)]
public class CraftingData : ScriptableObject {
        
        public int id;

        public string craftingName;

        public int craftingAmount = 1;

        public CraftingType type;

        public Sprite preview;

        public GameObject manifestation;
}
