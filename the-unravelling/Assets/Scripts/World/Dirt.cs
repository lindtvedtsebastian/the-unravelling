using UnityEngine;

[CreateAssetMenu(fileName = "Dirt" ,menuName = "Scriptable Objects/World/Dirt")]
public class Dirt : BitmaskableWorldEntity
{
    public override bool WorldCheck(int y, int x, int id) {
        return IsWorldPosTile(y, x, id) || IsWorldPosTile(y,x,GameData.Get.GRASS.id);
    }
}
