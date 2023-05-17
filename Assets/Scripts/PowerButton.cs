using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerButton : MonoBehaviour
{
    public int Number;
    public int Power;
    public int RotateStatus;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Button>().onClick.AddListener(PowerOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PowerOn() {
        for (int i = 0; i < GameObject.FindObjectOfType<GridManager>().PowerOnHex.Length; i++) {
            string name = GetComponent<Image>().sprite.name;
            name = name.Substring(0, name.Length - 1);
            string onname = GameObject.FindObjectOfType<GridManager>().PowerOnHex[i].name;
            onname = onname.Substring(0, onname.Length - 1);
            if (name == onname) {
                GetComponent<Image>().sprite = GameObject.FindObjectOfType<GridManager>().PowerOnHex[i];
                Power = i;
            }
        }
        GridManager grid = GameObject.FindObjectOfType<GridManager>();
        if (!grid.Hexs[Number - 20].GetComponent<Image>().sprite.name.Contains("Hex_k"))
        {
            if (grid.Hexs[Number - 20].GetComponent<Image>().sprite.name.Contains("Hex_active"))
            {
                grid.Hexs[Number - 20].GetChild(0).GetComponent<TMP_Text>().text = (int.Parse(grid.Hexs[Number - 20].GetChild(0).GetComponent<TMP_Text>().text) + 1).ToString();
            }
            else {
                grid.Hexs[Number - 20].GetComponent<Image>().sprite = grid.NormalHex[1];
                grid.Hexs[Number - 20].GetChild(0).gameObject.SetActive(true);
                grid.Hexs[Number - 20].GetChild(0).GetComponent<TMP_Text>().text = "1";
            }
        }
        switch (Power) {
            case 1:
                if (!grid.Hexs[Number - 40].GetComponent<Image>().sprite.name.Contains("Hex_k"))
                {
                    if (grid.Hexs[Number - 40].GetComponent<Image>().sprite.name.Contains("Hex_active"))
                    {
                        grid.Hexs[Number - 40].GetChild(0).GetComponent<TMP_Text>().text = (int.Parse(grid.Hexs[Number - 40].GetChild(0).GetComponent<TMP_Text>().text) + 1).ToString();
                    }
                    else
                    {
                        grid.Hexs[Number - 40].GetComponent<Image>().sprite = grid.NormalHex[1];
                        grid.Hexs[Number - 40].GetChild(0).gameObject.SetActive(true);
                        grid.Hexs[Number - 40].GetChild(0).GetComponent<TMP_Text>().text = "1";
                    }
                }
                break;
            case 2:
                if (!grid.Hexs[Number + 20].GetComponent<Image>().sprite.name.Contains("Hex_k"))
                {
                    if (grid.Hexs[Number + 20].GetComponent<Image>().sprite.name.Contains("Hex_active"))
                    {
                        grid.Hexs[Number + 20].GetChild(0).GetComponent<TMP_Text>().text = (int.Parse(grid.Hexs[Number + 20].GetChild(0).GetComponent<TMP_Text>().text) + 1).ToString();
                    }
                    else
                    {
                        grid.Hexs[Number + 20].GetComponent<Image>().sprite = grid.NormalHex[1];
                        grid.Hexs[Number + 20].GetChild(0).gameObject.SetActive(true);
                        grid.Hexs[Number + 20].GetChild(0).GetComponent<TMP_Text>().text = "1";
                    }
                }
                break;
            default:
                break;
        }
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(RotatePower);
        RotateStatus = 0;
        grid.CheckEnd();
    }

    public void RotatePower() {
        transform.Rotate(0, 0, 60);
        if (RotateStatus == 5)
        {
            RotateStatus = 0;
        }
        else { 
            RotateStatus++;
        }
        switch (RotateStatus) {
            case 0:
                switch (Power) {
                    case 0:
                        OnePowerChange(-9, -10, -20, -20);
                        break;
                    case 1:
                        SecondPowerChange(-9, -10, -19, -19, -20, -20, -40, -40);
                        break;
                    case 2:
                        SecondPowerChange(-9, -10, 10, 9, -20, -20, 20, 20);
                        break;
                    default:
                        break;
                }
                break;
            case 1:
                switch (Power)
                {
                    case 0:
                        OnePowerChange(-20, -20, -10, -11);
                        break;
                    case 1:
                        SecondPowerChange(-20, -20, -40, -40, -10, -11, -21, -21);
                        break;
                    case 2:
                        SecondPowerChange(-20, -20, 20, 20, -10, -11, 11, 10);
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (Power)
                {
                    case 0:
                        OnePowerChange(-10, -11, 10, 9);
                        break;
                    case 1:
                        SecondPowerChange(-10, -11, -21, -21, 10, 9, 19, 19);
                        break;
                    case 2:
                        SecondPowerChange(-10, -11, 11, 10, 10, 9, -9, -10);
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch (Power)
                {
                    case 0:
                        OnePowerChange(10, 9, 20, 20);
                        break;
                    case 1:
                        SecondPowerChange(10, 9, 19, 19, 20, 20, 40, 40);
                        break;
                    case 2:
                        SecondPowerChange(10, 9, -9, -10, 20, 20, -20, -20);
                        break;
                    default:
                        break;
                }
                break;
            case 4:
                switch (Power)
                {
                    case 0:
                        OnePowerChange(20, 20, 11, 10);
                        break;
                    case 1:
                        SecondPowerChange(20, 20, 40, 40, 11, 10, 21, 21);
                        break;
                    case 2:
                        SecondPowerChange(20, 20, -20, -20, 11, 10, -10, -11);
                        break;
                    default:
                        break;
                }
                break;
            case 5:
                switch (Power)
                {
                    case 0:
                        OnePowerChange(11, 10, -9, -10);
                        break;
                    case 1:
                        SecondPowerChange(11, 10, 21, 21, -9, -10, -19, -19);
                        break;
                    case 2:
                        SecondPowerChange(11, 10, -10, -11, -9, -10, 10, 9);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        GridManager grid = GameObject.FindObjectOfType<GridManager>();

        grid.CheckEnd();
    }

    void OnePowerChange(int prevdif1, int prevdif2, int nextdif1, int nextdif2) {
        GridManager grid = GameObject.FindObjectOfType<GridManager>();

        int PrevNum = (Number / 10) % 2 == 1 ? Number + prevdif1 : Number + prevdif2;
        if (grid.Hexs[PrevNum].GetComponent<Image>().sprite.name.Contains("Hex_active"))
        {
            if (int.Parse(grid.Hexs[PrevNum].GetChild(0).GetComponent<TMP_Text>().text) > 1)
            {
                grid.Hexs[PrevNum].GetChild(0).GetComponent<TMP_Text>().text = (int.Parse(grid.Hexs[PrevNum].GetChild(0).GetComponent<TMP_Text>().text) - 1).ToString();
            }
            else if (int.Parse(grid.Hexs[PrevNum].GetChild(0).GetComponent<TMP_Text>().text) == 1)
            {
                grid.Hexs[PrevNum].GetChild(0).gameObject.SetActive(false);
                grid.Hexs[PrevNum].GetComponent<Image>().sprite = grid.NormalHex[0];
            }
        }
        int NextNum = (Number / 10) % 2 == 1 ? Number + nextdif1 : Number + nextdif2;
        if (grid.Hexs[NextNum].GetComponent<Image>().sprite.name.Contains("Hex_active"))
        {
            grid.Hexs[NextNum].GetChild(0).GetComponent<TMP_Text>().text = (int.Parse(grid.Hexs[NextNum].GetChild(0).GetComponent<TMP_Text>().text) + 1).ToString();
        }
        else if (grid.Hexs[NextNum].GetComponent<Image>().sprite.name.Contains("Hex_empty"))
        {
            grid.Hexs[NextNum].GetComponent<Image>().sprite = grid.NormalHex[1];
            grid.Hexs[NextNum].GetChild(0).gameObject.SetActive(true);
        }
    }

    void SecondPowerChange(int prev1dif1, int prev1dif2, int prev2dif1, int prev2dif2, int next1dif1, int next1dif2, int next2dif1, int next2dif2) {
        GridManager grid = GameObject.FindObjectOfType<GridManager>();

        int PrevNum1 = (Number / 10) % 2 == 1 ? Number + prev1dif1 : Number + prev1dif2;
        if (grid.Hexs[PrevNum1].GetComponent<Image>().sprite.name.Contains("Hex_active"))
        {
            if (int.Parse(grid.Hexs[PrevNum1].GetChild(0).GetComponent<TMP_Text>().text) > 1)
            {
                grid.Hexs[PrevNum1].GetChild(0).GetComponent<TMP_Text>().text = (int.Parse(grid.Hexs[PrevNum1].GetChild(0).GetComponent<TMP_Text>().text) - 1).ToString();
            }
            else if (int.Parse(grid.Hexs[PrevNum1].GetChild(0).GetComponent<TMP_Text>().text) == 1)
            {
                grid.Hexs[PrevNum1].GetChild(0).gameObject.SetActive(false);
                grid.Hexs[PrevNum1].GetComponent<Image>().sprite = grid.NormalHex[0];
            }
        }
        int PrevNum2 = (Number / 10) % 2 == 1 ? Number + prev2dif1 : Number + prev2dif2;
        if (grid.Hexs[PrevNum2].GetComponent<Image>().sprite.name.Contains("Hex_active"))
        {
            if (int.Parse(grid.Hexs[PrevNum2].GetChild(0).GetComponent<TMP_Text>().text) > 1)
            {
                grid.Hexs[PrevNum2].GetChild(0).GetComponent<TMP_Text>().text = (int.Parse(grid.Hexs[PrevNum2].GetChild(0).GetComponent<TMP_Text>().text) - 1).ToString();
            }
            else if (int.Parse(grid.Hexs[PrevNum2].GetChild(0).GetComponent<TMP_Text>().text) == 1)
            {
                grid.Hexs[PrevNum2].GetChild(0).gameObject.SetActive(false);
                grid.Hexs[PrevNum2].GetComponent<Image>().sprite = grid.NormalHex[0];
            }
        }

        int NextNum1 = (Number / 10) % 2 == 1 ? Number + next1dif1 : Number + next1dif2;
        if (grid.Hexs[NextNum1].GetComponent<Image>().sprite.name.Contains("Hex_active"))
        {
            grid.Hexs[NextNum1].GetChild(0).GetComponent<TMP_Text>().text = (int.Parse(grid.Hexs[NextNum1].GetChild(0).GetComponent<TMP_Text>().text) + 1).ToString();
        }
        else if (grid.Hexs[NextNum1].GetComponent<Image>().sprite.name.Contains("Hex_empty"))
        {
            grid.Hexs[NextNum1].GetComponent<Image>().sprite = grid.NormalHex[1];
            grid.Hexs[NextNum1].GetChild(0).gameObject.SetActive(true);
        }
        int NextNum2 = (Number / 10) % 2 == 1 ? Number + next2dif1 : Number + next2dif2;
        if (grid.Hexs[NextNum2].GetComponent<Image>().sprite.name.Contains("Hex_active"))
        {
            grid.Hexs[NextNum2].GetChild(0).GetComponent<TMP_Text>().text = (int.Parse(grid.Hexs[NextNum2].GetChild(0).GetComponent<TMP_Text>().text) + 1).ToString();
        }
        else if (grid.Hexs[NextNum2].GetComponent<Image>().sprite.name.Contains("Hex_empty"))
        {
            grid.Hexs[NextNum2].GetComponent<Image>().sprite = grid.NormalHex[1];
            grid.Hexs[NextNum2].GetChild(0).gameObject.SetActive(true);
        }
    }
}
