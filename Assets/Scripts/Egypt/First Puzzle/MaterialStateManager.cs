using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MaterialStateManager
{
    public static int[] actualMaterial = new int[4];

    public static GameObject door;

    public static void Initialize(GameObject doorObj)
    {
        actualMaterial = new int[] { 0, 0, 0, 0 };
        door = doorObj;
    }

    public static void CheckStatus() {
        if (actualMaterial[0] == 1 && actualMaterial[1] == 2 && actualMaterial[2] == 3 && actualMaterial[3] == 4) {
            if (door != null) {
                GameObject.Destroy(door);
            }
        }
    }

}
