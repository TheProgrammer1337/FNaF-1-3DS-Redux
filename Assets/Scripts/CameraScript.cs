using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{

    public GameObject KitchenText;


    public GameObject SpriteHolder;
    public Sprite ButtonClicked;
    public Sprite ButtonUnClicked;

    public GameObject LowerCanvas;
    public GameObject Office;

    public Movement Movement;
    public string currentbutton;
    private bool isrunning;
    private bool isrunning2;
    public int Night;
    public GameObject buttonobject;
    public GameObject cachedbutton;
    public GameObject DividedStatic;
    public string cameratransfer;
    public string buttonpublic = "CAM1A";
    public AudioSource CameraBlip;
    // Use this for initialization
    void Start()
    {
        Night = PlayerPrefs.GetInt("Night", 1);
    }

    // Update is called once per frame
    public void buttonclick(GameObject buttonclicked)
    {
        if (cachedbutton != null)
        {
            cachedbutton.GetComponent<Image>().sprite = ButtonUnClicked;
        }
        buttonclicked.GetComponent<Image>().sprite = ButtonClicked;
        CameraBlip.Play();
        cachedbutton = buttonclicked;
    }
    public void OnButtonClick(string button)
    {
        SpriteHolder.GetComponent<Image>().color = new Color(256, 256, 256);
        buttonpublic = button;
        if (Movement.CameraIsntUp == false)
        {
            DividedStatic.SetActive(true);

        }
        KitchenText.SetActive(false);

        if (button != "CAM2A")
        {
            StopCoroutine("CAM2AAnimation");
            isrunning = false;
        }
        if (button != "CAM2B")
        {
            StopCoroutine("CAM2BAnimation");
            isrunning2 = false;
        }
        if (button == "CAM1A")
        {
            if (Movement.BonnieLocation == "CAM1A" && Movement.ChicaLocation == "CAM1A")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM1A/CAM1A");
            }
            else if (Movement.BonnieLocation != "CAM1A" && Movement.ChicaLocation == "CAM1A")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM1A/CAM1ABonnieMissing");
            }
            else if (Movement.BonnieLocation == "CAM1A" && Movement.ChicaLocation != "CAM1A")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM1A/CAM1AChicaMissing");
            }
            else if (Movement.FreddyLocation != "CAM1A")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM1A/CAM1AEmpty");
            }
            else if (Movement.BonnieLocation != "CAM1A" && Movement.ChicaLocation != "CAM1A")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM1A/CAM1AFreddy");
            }

        }
        if (button == "CAM1B")
        {
            if (Movement.BonnieLocation != "CAM1B" && Movement.ChicaLocation != "CAM1B")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM1B/CAM1B");
            }

            else if (Movement.BonnieLocation == "CAM1B" && Movement.ChicaLocation != "CAM1B")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM1B/CAM1BBonnie");
            }

            else if (Movement.BonnieLocation != "CAM1B" && Movement.ChicaLocation == "CAM1B")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM1B/CAM1BChica");
            }
            else if (Movement.BonnieLocation == "CAM1B")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM1B/CAM1BBonnie");
            }
        }
        if (button == "CAM1C")
        {
            if (Movement.FoxyLocation == 0)
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM1C/CAM1CStage0");
            }
            else if (Movement.FoxyLocation == 1)
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM1C/CAM1CStage1");
            }
            else if (Movement.FoxyLocation == 2)
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM1C/CAM1CStage2");
            }
            else if (Movement.FoxyLocation == 3)
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM1C/CAM1CStage3");
            }
            else if (Movement.FoxyLocation == 4)
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM1C/CAM1CStage4");
            }
        }
        if (button == "CAM2A")
        {
            if (isrunning == false)
            {
                StartCoroutine("CAM2AAnimation");
            }
            if (Movement.BonnieLocation == "CAM2A")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM2A/CAM2ABonnie");
            }
            else
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM2A/CAM2A");
            }
        }
        if (button == "CAM2B")
        {

            if (Movement.BonnieLocation == "CAM2B")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM2B/CAM2BBonnie1");
                if (isrunning2 == false && Night >= 4)
                {
                    StartCoroutine("CAM2BAnimation");
                }
            }
            else
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM2B/CAM2B");
            }
        }
        if (button == "CAM3")
        {

            if (Movement.BonnieLocation == "CAM3")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM3/CAM3Bonnie");
            }
            else
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM3/CAM3");
            }
        }
        if (button == "CAM4A")
        {

            if (Movement.ChicaLocation == "CAM4A")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM4A/CAM4AChica1");

            }
            else
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM4A/CAM4A");
            }
        }
        if (button == "CAM4B")
        {

            if (Movement.ChicaLocation == "CAM4B")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM4B/CAM4BChica1");
            }
            else
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM4B/CAM4B");
            }
        }
        if (button == "CAM5")
        {
            if (Movement.BonnieLocation == "CAM5")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM5/CAM5Bonnie");
            }
            else
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM5/CAM5");

            }
        }
        if (button == "CAM6")
        {
            SpriteHolder.GetComponent<Image>().color = new Color(0, 0, 0);
            KitchenText.SetActive(true);
        }
        if (button == "CAM7")
        {
            if (Movement.ChicaLocation == "CAM7")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM7/CAM7Chica");
            }
            else
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM7/CAM7");
            }


        }
        Resources.UnloadUnusedAssets();
        currentbutton = button;
    }
    IEnumerator CAM2AAnimation()
    {
        isrunning = true;
        while (true)
        {
            SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM2A/CAM2A2"); ;
            yield return new WaitForSeconds(0.1f - Random.Range(-0.03f, 0.09f));
            if (Movement.BonnieLocation == "CAM2A")
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM2A/CAM2ABonnie");
            }
            else
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM2A/CAM2A");
            }
            yield return new WaitForSeconds(0.1f - Random.Range(-0.03f, 0.09f));
        }
    }
    IEnumerator CAM2BAnimation()
    {
        isrunning2 = true;
        while (Movement.BonnieLocation == "CAM2B")
        {
            SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM2B/CAM2BBonnie1");
            yield return new WaitForSeconds(0.1f - Random.Range(-0.03f, 0.09f));
            SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM2B/CAM2BBonnie2");
            yield return new WaitForSeconds(0.1f - Random.Range(-0.09f, 0.09f));
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM2B/CAM2BBonnie3");
                yield return new WaitForSeconds(0.1f - Random.Range(-0.09f, 0.09f));
            }
        }
        isrunning2 = false;
        SpriteHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cameras/CAM2B/CAM2B");
    }
    public IEnumerator Blackout()
    {
        OnButtonClick(buttonpublic);
        DividedStatic.SetActive(true);
        SpriteHolder.GetComponent<Image>().color = new Color(SpriteHolder.GetComponent<Image>().color.r, SpriteHolder.GetComponent<Image>().color.g, SpriteHolder.GetComponent<Image>().color.b, 0);
        yield return new WaitForSeconds(5);
        SpriteHolder.GetComponent<Image>().color = new Color(SpriteHolder.GetComponent<Image>().color.r, SpriteHolder.GetComponent<Image>().color.g, SpriteHolder.GetComponent<Image>().color.b, 1);

    }
}
