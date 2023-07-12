using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EgyptianPuzzleManager
{
    public static int[] actualMaterial = new int[4];
    public static bool keypadPuzzle = false;

    public static GameObject door;
    public static AudioClip resolvedAll;
    public static AudioClip resolvedOne;

    public static void Initialize(GameObject doorObj, AudioClip resolvedAllObj, AudioClip resolvedOneObj)
    {
        actualMaterial = new int[] { 0, 0, 0, 0 };
        door = doorObj;
        resolvedAll = resolvedAllObj;
        resolvedOne = resolvedOneObj;
    }

    public static void CheckStatus() {
        if (door != null) {
            if (actualMaterial[0] == 1 && actualMaterial[1] == 2 && actualMaterial[2] == 3 && actualMaterial[3] == 4 && !keypadPuzzle) {
                AudioSource.PlayClipAtPoint(resolvedOne, door.transform.position);
            }
            else if (keypadPuzzle && !(actualMaterial[0] == 1 && actualMaterial[1] == 2 && actualMaterial[2] == 3 && actualMaterial[3] == 4)) {
                AudioSource.PlayClipAtPoint(resolvedOne, door.transform.position);
            }
            else if (actualMaterial[0] == 1 && actualMaterial[1] == 2 && actualMaterial[2] == 3 && actualMaterial[3] == 4 && keypadPuzzle) {
                AudioSource.PlayClipAtPoint(resolvedAll, door.transform.position);
                GameObject.Destroy(door);
            }
        }
    }
}
