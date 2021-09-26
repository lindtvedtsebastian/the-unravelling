using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName = "Bitmaskable World Entity" ,menuName = "Scriptable Objects/World/Bitmaskable World Entity")]
public class BitmaskableWorldEntity : WorldEntity
{
    private bool checkNorth = false;
    private bool checkWest = false;
    private bool checkEast = false;
    private bool checkSouth = false;
    private bool northExists = false;
    private bool westExists = false;
    private bool eastExists = false;
    private bool southExists = false;

    public TileBase SetSprite(int x, int y) {
        calculateBitmask(x, y);
        return this.sprites[0];
    }

    public int calculateBitmask(int x, int y) {
        identifyDirections(x, y);
        int bitmask = calculateCardinals(x, y);
        bitmask += calculateCorners(x, y);
        return bitmask;
    }

    private void identifyDirections(int x, int y) {
        checkNorth = y - 1 > 0;
        checkWest = x - 1 > 0;
        checkEast = x + 1 < WorldData.Get.worldSize;
        checkSouth = y + 1 < WorldData.Get.worldSize;
    }

    
    public int calculateCardinals(int x, int y) {
        int bitmask = 0;

        if (checkNorth && WorldData.Get.map[x,y-1] != this.id) {
            bitmask += WorldData.N;
            northExists = true;
        }
        if (checkWest && WorldData.Get.map[x-1,y] != this.id) {
            bitmask += WorldData.W;
            westExists = true;
        }
        if (checkEast && WorldData.Get.map[x+1,y] != this.id) {
            bitmask += WorldData.E;
            eastExists = true;
        }
        if (checkSouth && WorldData.Get.map[x,y+1] != this.id) {
            bitmask += WorldData.S;
            southExists = true;
        }
        return bitmask;
    }

    public int calculateCorners(int x, int y) {
        int bitmask = 0;
        
        if ((checkNorth && checkWest && WorldData.Get.map[x-1, y-1] != this.id)
            && (northExists || westExists)) {
            bitmask += WorldData.NW;
        }
        if ((checkNorth && checkEast && WorldData.Get.map[x+1, y-1] != this.id)
            && (northExists || eastExists)) {
            bitmask += WorldData.NE;
        }
        if ((checkSouth && checkWest && WorldData.Get.map[x-1, y+1] != this.id)
            && (southExists || westExists)) {
            bitmask += WorldData.SW;
        }
        if ((checkSouth && checkEast && WorldData.Get.map[x+1, y+1] != this.id)
            && (southExists || eastExists)) {
            bitmask += WorldData.SE;
        }
        return bitmask;
    }
}
