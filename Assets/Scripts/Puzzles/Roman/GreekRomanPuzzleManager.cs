using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GreekRomanPuzzleManager
{
    public static AudioClip resolved;
    public static GameObject door;
    public static GameObject congratulations;
    public static GameObject[] puzzles;

    public static bool leverPuzzle = false;
    public static int mapPuzzle = 0;

    public static void Initialize(GameObject doorObj, AudioClip resolvedObj, GameObject congratulationsObj, GameObject[] puzzlesObj) {
        door = doorObj;
        resolved = resolvedObj;
        congratulations = congratulationsObj;
        puzzles = puzzlesObj;
    }

    public static void CheckStatus() {
        if (leverPuzzle && mapPuzzle == 2) {
            if (door != null) {
                AudioSource.PlayClipAtPoint(resolved, door.transform.position);
                GameObject.Destroy(door);
                congratulations.SetActive(true);
                foreach (var item in puzzles)
                {
                    item.SetActive(false);
                }
            }
        }
    }
}
