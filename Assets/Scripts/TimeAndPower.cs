using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeAndPower : MonoBehaviour
{
    public float Time = 360;
    public int PowerLeft = 101;
    public float PowerDrain = 6;
    public int PowerUsage = 1;
    public GameObject TimeCounter;
    public GameObject PowerCounter;
    public GameObject PowerCounterBar;

    public Sprite PowerBar1;
    public Sprite PowerBar2;
    public Sprite PowerBar3;
    public Sprite PowerBar4;
    public Sprite PowerBar5;
    int index;
    private string[] timeTexts = { "12 AM", "1 AM", "2 AM", "3 AM", "4 AM", "5 AM", "6 AM" };
    bool triggered = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (PowerUsage)
        {
            case 1:
                PowerCounterBar.GetComponent<Image>().sprite = PowerBar1;
                break;
            case 2:
                PowerCounterBar.GetComponent<Image>().sprite = PowerBar2;
                break;
            case 3:
                PowerCounterBar.GetComponent<Image>().sprite = PowerBar3;
                break;
            case 4:
                PowerCounterBar.GetComponent<Image>().sprite = PowerBar4;
                break;
            case 5:
                PowerCounterBar.GetComponent<Image>().sprite = PowerBar5;
                break;
            default:
                break;
        }

        Time -= UnityEngine.Time.deltaTime;
        if (Time < 0 && triggered == false)
        {
            triggered = true;
            PlayerPrefs.SetInt("Night", PlayerPrefs.GetInt("Night", 1) + 1);
            PlayerPrefs.Save();
            SceneManager.LoadSceneAsync("Win");
        }
        index = Mathf.Clamp(6 - Mathf.CeilToInt(Time / 60), 0, 6);
        TimeCounter.GetComponent<Text>().text = timeTexts[index];

        PowerUsage = Mathf.Clamp(PowerUsage, 1, 5);

        PowerDrain -= UnityEngine.Time.deltaTime;

        if (PowerDrain <= 0)
        {
            PowerLeft -= 1;
            PowerCounter.GetComponent<Text>().text = "Power left: " + PowerLeft.ToString() + "%";
            PowerDrain = GetPowerDrainRate(PowerUsage);
        }
    }

    float GetPowerDrainRate(int usage)
    {
        switch (usage)
        {
            case 1:
                return 9.6f;
            case 2:
                return 4.8f;
            case 3:
                return 3.2f;
            case 4:
                return 2.4f;
            case 5:
                return 1.2f; 
            default:
                return 9.6f;
        }
    }
}

