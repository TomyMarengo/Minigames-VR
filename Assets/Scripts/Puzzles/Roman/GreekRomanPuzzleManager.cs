using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GreekRomanPuzzleManager
{
    public static AudioClip resolved;
    public static GameObject door;

    public static bool leverPuzzle = false;
    public static bool mapPuzzle = false;

    public static void Initialize(GameObject doorObj, AudioClip resolvedObj) {
        door = doorObj;
        resolved = resolvedObj;
    }

    public static void CheckStatus() {
        if (leverPuzzle && mapPuzzle) {
            if (door != null) {
                AudioSource.PlayClipAtPoint(resolved, door.transform.position);
                GameObject.Destroy(door);
            }
        }
    }
}
