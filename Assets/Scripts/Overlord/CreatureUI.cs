using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using StarterAssets;

public class CreatureUI : MonoBehaviour
{
    [Header("Color Settings")]
    public string stringForCreature = "greenCreature";

    [SerializeField] private Image greenCritter;
    [SerializeField] private Image yellowCritter;
    [SerializeField] private Image redCritter;

    [SerializeField] private TextMeshProUGUI greenNum;
    [SerializeField] private TextMeshProUGUI yellowNum;
    [SerializeField] private TextMeshProUGUI redNum;

    private int square = 0;
    private Image[] selectionSquares;
    private void Start()
    {
        selectionSquares = new Image[] { greenCritter, yellowCritter, redCritter };
    }
    void Update()
    {
        GetInput();
        UpdateNumbers();
        UpdateSquares(square);
        DefineSquare();
    }
    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            square = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            square = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            square = 2;
        }
        //scroll down to go to next bar
        if (Input.mouseScrollDelta.y == -1)
        {
            if (square < 2)
            {
                square++;
            }
            else
            {
                square = 0;
            }
        }
        //scroll up to go to previous bar
        else if (Input.mouseScrollDelta.y == 1)
        {
            if (square > 0)
            {
                square--;
            }
            else
            {
                square = 2;
            }
        }
        //reset bar to default
        if (Input.GetKeyDown(KeyCode.Q))
        {
            square = 0;
        }
    }
    void UpdateSquares(int index)
    {
        for (int i = 0; i < selectionSquares.Length; i++)
        {
            selectionSquares[i].enabled = (i == index);
        }
    }
    void DefineSquare()
    {
        if (square == 0)
        {
            stringForCreature = "greenCreature";
        }
        else if (square == 1)
        {
            stringForCreature = "yellowCreature";
        }
        else if (square == 2)
        {
            stringForCreature = "redCreature";
        }
    }
    void UpdateNumbers()
    {
        greenNum.text = LevelData.greenCreature.ToString();
        yellowNum.text = LevelData.yellowCreature.ToString();
        redNum.text = LevelData.redCreature.ToString();
    }
}
