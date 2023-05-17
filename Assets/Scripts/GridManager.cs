using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [Header("PARENT")]
    public Transform[] GridParent;
    [Header("PIECES")]
    public Sprite[] NormalHex;
    public Sprite[] MissHex;
    public Sprite[] PowerOnHex;
    public Sprite[] PowerOffHex;
    public Sprite CompleteHex;

    public List<Transform> Hexs = new List<Transform>();
    private List<int> warningpoint = new List<int>();
    public List<int> OnePowerPos = new List<int>();

    private int[] InitPos = { 4, 5, 13, 14 };
    public int randomnum;
    public int secondrnum;
    // Start is called before the first frame update
    void Start()
    {
        //Alpha and Item Instatiate
        for (int i = 0; i < 99; i++) {
            Transform Instance = Instantiate(GridParent[0].GetChild(0), GridParent[0]);
            Instance.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        }
        for (int i = 0; i < 89; i++) {
            Transform Instance = Instantiate(GridParent[1].GetChild(0), GridParent[1]);
            Instance.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        }
        //Get Hexs Array
        for (int i = 0; i < 10; i++) {
            HexsAdded(0, i * 10, (i + 1) * 10);
            if (i == 9) {
                break;
            }
            HexsAdded(1, i * 10, (i + 1) * 10);
        }
        //Random Goal Number Placement
        int randomscale = Random.Range(0, InitPos.Length);
        randomnum = Random.Range(InitPos[randomscale] * 10 + 2, InitPos[randomscale] * 10 + 8);
        Hexs[randomnum].GetComponent<Image>().sprite = MissHex[0];
        Hexs[randomnum].GetChild(0).gameObject.SetActive(true);
        Hexs[randomnum].GetChild(0).GetComponent<TMP_Text>().text = "3";

        int secondscale = Random.Range(2, 7);
        int secondnum = 0;
        if (InitPos[randomscale] < 10)
        {
            secondnum = InitPos[randomscale] + secondscale;
        }
        else {
            secondnum = InitPos[randomscale] - secondscale;
        }
        secondrnum = Random.Range(secondnum * 10 + randomnum % 10 - 1, secondnum * 10 + randomnum % 10 + 1);
        Hexs[secondrnum].GetComponent<Image>().sprite = MissHex[0];
        Hexs[secondrnum].GetChild(0).gameObject.SetActive(true);
        Hexs[secondrnum].GetChild(0).GetComponent<TMP_Text>().text = "3";
        //Random Power Placement
        int powercount = 0;
        for (int i = 0; i < Hexs.Count; i++) {
            if (Hexs[i].GetComponent<Image>().sprite.name == MissHex[0].name) {
                powercount += int.Parse(Hexs[i].GetChild(0).GetComponent<TMP_Text>().text);
            }
        }

        int first = 0;
        int second = 0;
        for(int i = 0; i < 2; i++) {
            int randombool = Random.Range(0, 2);
            if (i == 0)
            {
                first = randombool == 0 ? randomnum - 1 : randomnum + 1;
            }
            else {
                second = randombool == 0 ? secondrnum - 1 : secondrnum + 1;
            }
        }
        int firstpower = Random.Range(1, 3);
        int secondpower = Random.Range(1, 3);
        Hexs[first].GetComponent<Image>().sprite = PowerOffHex[firstpower];
        Hexs[second].GetComponent<Image>().sprite = PowerOffHex[secondpower];

        int fwarnings = firstpower == 1 ? 2 : 1;
        int swarnings = secondpower == 1 ? 2 : 1;
        AddPoint(warningpoint, first, fwarnings, (first / 10) % 2 == 1 ? 1 : 0);
        AddPoint(warningpoint, second, swarnings, (second / 10) % 2 == 1 ? 1 : 0);

        AddPoint(OnePowerPos, randomnum, 2, (randomnum / 10) % 2 == 1 ? 1 : 0);
        AdditionalPoint(OnePowerPos, randomnum);
        AddPoint(OnePowerPos, secondrnum, 2, (secondrnum / 10) % 2 == 1 ? 1 : 0);
        AdditionalPoint(OnePowerPos, secondrnum);
        for (int i = 0; i < warningpoint.Count; i++) {
            if (OnePowerPos.Contains(warningpoint[i])) {
                OnePowerPos.RemoveAt(OnePowerPos.IndexOf(warningpoint[i]));
            }
        }
        List<int> tmppos = new List<int>();
        tmppos = OnePowerPos.Distinct().ToList();
        tmppos.Remove(randomnum);
        tmppos.Remove(secondrnum);

        List<int> onepowerpos = new List<int>();
        for (int i = 0; i < 3; i++) {
            int pos = Random.Range(0, tmppos.Count);
            Hexs[tmppos[pos]].GetComponent<Image>().sprite = PowerOffHex[0];
            onepowerpos.Add(tmppos[pos]);
            tmppos.RemoveAt(pos);
        }
        

        //Add Inition Listener
        Hexs[first].AddComponent<PowerButton>().Number = first;
        Hexs[second].AddComponent<PowerButton>().Number = second;
        Hexs[first].GetComponent<Button>().onClick.AddListener(Hexs[first].GetComponent<PowerButton>().PowerOn);
        Hexs[second].GetComponent<Button>().onClick.AddListener(Hexs[second].GetComponent<PowerButton>().PowerOn);

        for (int i = 0; i < onepowerpos.Count; i++) {
            Hexs[onepowerpos[i]].AddComponent<PowerButton>().Number = onepowerpos[i];
            Hexs[onepowerpos[i]].GetComponent<Button>().onClick.AddListener(Hexs[onepowerpos[i]].GetComponent<PowerButton>().PowerOn);
        }
    }

    void AdditionalPoint(List<int> tmp, int pos) {
        if ((pos / 10) % 2 == 1)
        {
            tmp.Add(pos + 30);
            tmp.Add(pos + 31);
            tmp.Add(pos - 30);
            tmp.Add(pos - 29);
        }
        else
        {
            tmp.Add(pos + 30);
            tmp.Add(pos - 31);
            tmp.Add(pos - 30);
            tmp.Add(pos + 29);
        }
    }

    void AddPoint(List<int> tmp, int pos, int powertype, int type) {
        tmp.Add(pos + 20);
        tmp.Add(pos - 20);
        tmp.Add(pos + 10);
        tmp.Add(pos - 10);
        if (type == 1)
        {
            tmp.Add(pos - 9);
            tmp.Add(pos + 11);
            
        }
        else {
            tmp.Add(pos - 11);
            tmp.Add(pos + 9);
        }
        if (powertype == 2)
        {
            tmp.Add(pos + 21);
            tmp.Add(pos + 19);

            tmp.Add(pos - 19);
            tmp.Add(pos - 21);

            tmp.Add(pos + 40);
            tmp.Add(pos - 40);
        }   
    }

    void HexsAdded(int parentnum, int SCount, int ECount) {
        for (int i = SCount; i < ECount; i++)
        {
            Hexs.Add(GridParent[parentnum].GetChild(i));
        }
    }

    public void CheckEnd() {
        CheckEngine(randomnum);
        CheckEngine(secondrnum);
        if (Hexs[randomnum].GetComponent<Image>().sprite.name.Contains("Hex_complete") && Hexs[secondrnum].GetComponent<Image>().sprite.name.Contains("Hex_complete")) {
            Debug.Log("Completed!");
        }
    }

    void CheckEngine(int num) {
        int fnum = (num / 10) % 2;
        List<int> roundpoints = new List<int>();
        roundpoints.Add(-20);
        roundpoints.Add(20);
        if (fnum == 1)
        {
            roundpoints.Add(-10);
            roundpoints.Add(10);
            roundpoints.Add(-9);
            roundpoints.Add(11);
        }
        else
        {
            roundpoints.Add(-11);
            roundpoints.Add(-10);
            roundpoints.Add(9);
            roundpoints.Add(10);
        }
        int goalpoint = int.Parse(Hexs[num].GetChild(0).GetComponent<TMP_Text>().text);
        int currentpoint = 0;
        for (int i = 0; i < roundpoints.Count; i++)
        {
            if (Hexs[num + roundpoints[i]].GetChild(0).gameObject.activeSelf)
            {
                currentpoint += int.Parse(Hexs[num + roundpoints[i]].GetChild(0).GetComponent<TMP_Text>().text);
            }
        }
        Debug.Log(currentpoint);
        if (currentpoint > 0)
        {
            Hexs[num].GetComponent<Image>().sprite = MissHex[1];
            Hexs[num].GetChild(0).gameObject.SetActive(true);
        }
        if (currentpoint > goalpoint)
        {
            Hexs[num].GetComponent<Image>().sprite = MissHex[2];
            Hexs[num].GetChild(0).gameObject.SetActive(true);
        }
        if (currentpoint == goalpoint)
        {
            Hexs[num].GetComponent<Image>().sprite = CompleteHex;
            Hexs[num].GetChild(0).gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
