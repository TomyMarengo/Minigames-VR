using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreekRomanPuzzleManager : MonoBehaviour
{

    private LeverPuzzle leverPuzzle;
    private RotateMap[] rotatePuzzles;

    // Start is called before the first frame update
    void Start()
    {
        leverPuzzle = GetComponentInChildren<LeverPuzzle>();
        rotatePuzzles = GetComponentsInChildren<RotateMap>();

    }

    // Update is called once per frame
    void Update()
    {
        if(leverPuzzle.getPuzzleDone() && rotatePuzzles[0].getPuzzleDone() && rotatePuzzles[1].getPuzzleDone())
        {
            Debug.Log("All puzzles completed!");
        }
    }
}
