using UnityEngine;

[CreateAssetMenu(fileName = "Dirt" ,menuName = "Scriptable Objects/World/Dirt")]
public class Dirt : BitmaskableWorldEntity
{
    /// <summary>
    /// A overridden function for determining if a tile should be part of the bitmask or not
    /// <summary>
    /// <param name="y">The position of the tile to be checked in the y axis</param>
    /// <param name="x">The position of the tile to be checked in the x axis</param>
    /// <param name="id">The id of the tile having its mask calculated</param>
    /// <returns>Wheter or not the tile that was checked is part of the bitmask or not</returns>
    public override bool BitmaskPredicate(int y, int x, int id) {
        return IsWorldPosTile(y, x, id) || IsWorldPosTile(y,x,GameIDs.DIRT);
    }
}
