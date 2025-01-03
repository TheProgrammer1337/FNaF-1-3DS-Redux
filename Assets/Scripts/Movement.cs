using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public int Night;
    public int BonnieDifficulty;
    public int ChicaDifficulty;
    public int FreddyDifficulty;
    public int FoxyDifficulty;

    public string BonnieLocation;
    public string ChicaLocation;
    public string FreddyLocation;
    public int FoxyLocation;
    public float FoxyMovementCountdownBonus;
    public float FoxyMovementCountdown;

    public GameObject LowerCanvas;
    public GameObject Office;
    public Image CameraObject;
    public GameObject UpperCanvas;

    public AudioSource FanNoise;
    public AudioSource Ambience;
    public AudioSource CameraSound;
    public TimeAndPower power;
    public CameraScript CameraScript;
    public GameObject CameraStatic;
    public GameObject CameraStaticPrefab;
    public GameObject BonnieJumpscare;
    public GameObject Black;
    public AudioSource StaticDeath;
    public bool bonnieinside = false;
    bool chicainside = false;
    public bool CameraIsntUp = true;
    public GameObject Tablet;
    public GameObject DividedStatic;
    float cooldown;
    bool once = false;
    public bool ReadyForFreddy;

    void Update()
    {
        if (ReadyForFreddy == true)
        {
            if (CameraIsntUp == true && BonnieLocation != "CAM1A" && ChicaLocation != "CAM1A")
            {
                StartCoroutine("FreddyMovement");
                ReadyForFreddy = false;
            }
        }

        // Foxy code
        {
            if (CameraIsntUp == !true)
            {
                FoxyMovementCountdownBonus = UnityEngine.Random.Range(0.83f, 16.67f);
            }
            if (CameraIsntUp == !false)
            {
                if (FoxyMovementCountdownBonus > 0)
                {
                    FoxyMovementCountdownBonus -= Time.deltaTime;
                }
                else
                {
                    FoxyMovementCountdown -= Time.deltaTime;
                }
            }
            if (FoxyMovementCountdown < 0)
            {
                if (Random.Range(0, 21) <= FoxyDifficulty)
                {
                    FoxyLocation += 1;
                    CameraScript.OnButtonClick(CameraScript.currentbutton);
                }
                FoxyMovementCountdown = 5.01f;
            }
        }
        // End of Foxy code

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (cooldown < 0)
            {
                StartCoroutine("Camera");
                cooldown = 0.27f;
            }
        }
        cooldown -= Time.deltaTime;

    }
    IEnumerator Camera()
    {
        CameraIsntUp = !CameraIsntUp;
        Tablet.SetActive(true);
        if (CameraIsntUp == true)
        {
            Resources.UnloadUnusedAssets();
            Tablet.GetComponent<Animator>().SetBool("Up", false);
            CameraObject.enabled = !CameraObject.IsActive() == true;
            LowerCanvas.SetActive(false);
            CameraStatic.SetActive(!CameraStatic.active);
            Office.SetActive(true);
            if (bonnieinside == true)
            {
                foreach (GameObject child in GameObject.FindGameObjectsWithTag("Respawn"))
                {
                    Destroy(child);
                    Debug.Log(child.name);
                }
                GameObject bonniejumpscare = Instantiate(BonnieJumpscare, UpperCanvas.transform);
                Tablet.transform.SetAsLastSibling();
                yield return new WaitForSeconds(0.3f);
                Destroy(Tablet);
                yield return new WaitForSeconds(0.3f);
                DividedStatic.SetActive(true);
                DividedStatic.transform.SetAsLastSibling();
                StaticDeath.Play();

                DestroyImmediate(bonniejumpscare, true);

                CameraStatic.SetActive(true);
                CameraStatic.GetComponent<Animator>().Play("NewAnim");
                CameraStatic.transform.SetAsLastSibling();
            }
            else if (chicainside == true)
            {
                foreach (GameObject child in GameObject.FindGameObjectsWithTag("Respawn"))
                {
                    DestroyImmediate(child);
                    Debug.Log(child.name);
                }
                Instantiate(BonnieJumpscare, UpperCanvas.transform);
                Instantiate(CameraStaticPrefab, UpperCanvas.transform);
                Tablet.transform.SetAsLastSibling();
                CameraStatic.SetActive(true);
                CameraStatic.transform.SetAsLastSibling();

            }

        }
        else
        {
            Tablet.GetComponent<Animator>().SetBool("Up", true);

        }
        yield return new WaitForSeconds(0.24f);
        Tablet.SetActive(false);
        if (CameraIsntUp == true)
        {

            LowerCanvas.SetActive(false);
            try
            {
                Office.SetActive(true);
                CameraObject.enabled = false;
            }
            catch
            {

            }
            if (chicainside != true && bonnieinside != true)
            {
                CameraStatic.SetActive(false);
            }
            else
            {
                CameraStatic.SetActive(true);
            }


        }
        else
        {
            DividedStatic.SetActive(true);

            LowerCanvas.SetActive(true);
            Office.SetActive(false);
            CameraObject.enabled = true;
            CameraStatic.SetActive(true);


            if (once == false)
            {
                CameraScript.OnButtonClick("CAM1A");
                once = true;
            }

            if (Office.GetComponent<Office>().leftlighton == true)
            {
                power.PowerUsage -= 1;
                Office.GetComponent<Office>().leftlighton = false;
                Office.GetComponent<Office>().LeftLightBar.GetComponent<Image>().sprite = Office.GetComponent<Office>().LeftLightBarNone;
            }
            if (Office.GetComponent<Office>().rightlighton == true)
            {
                power.PowerUsage -= 1;
                Office.GetComponent<Office>().rightlighton = false;
                Office.GetComponent<Office>().RightLightBar.GetComponent<Image>().sprite = Office.GetComponent<Office>().RightLightBarNone;
            }
            Office.GetComponent<Office>().LightSound.Stop();
            Office.GetComponent<Office>().gameObject.GetComponent<Image>().sprite = Office.GetComponent<Office>().NoLight;



        }

        FanNoise.volume = 0.15f;
        Ambience.volume = 0.5f;
        CameraSound.Play();
        if (CameraObject != null)
        {
            if (CameraObject.IsActive() == false)
            {
                power.PowerUsage -= 1;
                FanNoise.volume = 1f;
                Ambience.volume = 1f;
            }
            else
            {
                power.PowerUsage += 1;
                FanNoise.volume = 0.25f;
                Ambience.volume = 0.8f;
            }
        }

    }
    void Start()
    {
        StartCoroutine("FreddyMovementCheck");
        StartCoroutine("BonnieMovement");
        StartCoroutine("ChicaMovement");
        Night = PlayerPrefs.GetInt("Night", 1);
        if (Night == 1)
        {
            BonnieDifficulty = 0;
            ChicaDifficulty = 0;
            FreddyDifficulty = 0;
            FoxyDifficulty = 0;

        }

        if (Night == 2)
        {
            BonnieDifficulty = 3;
            ChicaDifficulty = 1;
            FreddyDifficulty = 0;
            FoxyDifficulty = 1;


        }

        if (Night == 3)
        {
            BonnieDifficulty = 0;
            ChicaDifficulty = 5;
            FreddyDifficulty = 1;
            FoxyDifficulty = 2;

        }

        if (Night == 4)
        {
            BonnieDifficulty = 2;
            ChicaDifficulty = 4;
            FreddyDifficulty = 2;
            FoxyDifficulty = 6;

        }

        if (Night == 5)
        {
            BonnieDifficulty = 5;
            ChicaDifficulty = 7;
            FreddyDifficulty = 3;
            FoxyDifficulty = 5;


        }

        if (Night == 6)
        {
            BonnieDifficulty = 10;
            ChicaDifficulty = 12;
            FreddyDifficulty = 4;
            FoxyDifficulty = 16;

        }

        if (Night == 7)
        {
            BonnieDifficulty = PlayerPrefs.GetInt("BonnieDifficulty", BonnieDifficulty);
            ChicaDifficulty = PlayerPrefs.GetInt("ChicaDifficulty", ChicaDifficulty);
            FreddyDifficulty = PlayerPrefs.GetInt("FreddyDifficulty", FreddyDifficulty);
            FoxyDifficulty = PlayerPrefs.GetInt("FoxyDifficulty", FoxyDifficulty);
        }
    }

    IEnumerator BonnieMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(4.97f);
            string prevlocation = BonnieLocation;
            int rand = Random.Range(0, 21);
            Debug.Log("BonnieMoveAttempt");
            if (rand < BonnieDifficulty)
            {
                switch (BonnieLocation)
                {
                    case "CAM1A": // stage
                        rand = Random.Range(0, 2);
                        if (rand == 1)
                        {
                            // backstage
                            BonnieLocation = "CAM5";
                        }
                        else
                        {
                            // party room
                            BonnieLocation = "CAM1B";
                        }
                        break;
                    case "CAM5": // backstage
                        rand = Random.Range(0, 2);
                        if (rand == 1)
                        {
                            // backstage
                            BonnieLocation = "CAM5";
                        }
                        else
                        {
                            // west hallway
                            BonnieLocation = "CAM2A";
                        }
                        break;

                    case "CAM1B": // party room
                        rand = Random.Range(0, 2);
                        if (rand == 1)
                        {
                            // backstage
                            BonnieLocation = "CAM5";
                        }
                        else
                        {
                            // west hallway
                            BonnieLocation = "CAM2A";
                        }
                        break;

                    case "CAM2A": // west hallway
                        rand = Random.Range(0, 2);
                        if (rand == 1)
                        {
                            // west corner
                            BonnieLocation = "CAM2B";
                        }
                        else
                        {
                            // supply closet
                            BonnieLocation = "CAM3";
                        }
                        break;

                    case "CAM2B": // west corner
                        rand = Random.Range(0, 2);
                        if (rand == 1)
                        {
                            // left door
                            BonnieLocation = "LEFTDOOR";
                        }
                        else
                        {
                            // supply closet
                            BonnieLocation = "CAM3";
                        }
                        break;
                    case "CAM3": // supply closet
                        rand = Random.Range(0, 2);
                        if (rand == 0)
                        {
                            BonnieLocation = "CAM2A";
                        }
                        else if (rand == 1)
                        {
                            // left door
                            BonnieLocation = "LEFTDOOR";
                        }

                        break;

                    case "LEFTDOOR":
                        StopCoroutine("BonnieMovement");
                        BonnieLocation = "INSIDE";
                        bonnieinside = true;
                        break;
                    default:
                        break;
                }
                if (BonnieLocation == CameraScript.buttonpublic || prevlocation == CameraScript.buttonpublic)
                {
                    CameraScript.StartCoroutine("Blackout");
                }
                prevlocation = BonnieLocation;
            }

            Debug.Log(BonnieLocation.ToString());
        }
    }
    IEnumerator ChicaMovement()
    {
        while (true)
        {
            string prevloc2 = ChicaLocation;
            yield return new WaitForSeconds(4.98f);
            int rand = Random.Range(0, 21);
            Debug.Log("ChicaMoveAttempt");
            if (rand < ChicaDifficulty)
            {
                switch (ChicaLocation)
                {
                    case "CAM1A": // stage
                        //dining hall
                        ChicaLocation = "CAM1B";
                        break;
                    case "CAM1B": // dining hall
                        rand = Random.Range(0, 2);
                        if (rand == 1)
                        {
                            // kitchen
                            ChicaLocation = "CAM6";
                        }
                        else
                        {
                            // bathrooms
                            ChicaLocation = "CAM7";
                        }
                        break;

                    case "CAM6": // kitchen
                        rand = Random.Range(0, 2);
                        if (rand == 1)
                        {
                            // bathrooms
                            ChicaLocation = "CAM7";
                        }
                        else
                        {
                            // east hallway
                            ChicaLocation = "CAM4A";
                        }
                        break;

                    case "CAM7": // bathrooms
                        rand = Random.Range(0, 2);
                        if (rand == 1)
                        {
                            // east hallway
                            ChicaLocation = "CAM4A";
                        }
                        else
                        {
                            // kitchen
                            ChicaLocation = "CAM6";
                        }
                        break;

                    case "CAM4A": // east hallway
                        rand = Random.Range(0, 2);
                        if (rand == 1)
                        {
                            // dining hall
                            ChicaLocation = "CAM1B";
                        }
                        else
                        {
                            // east corner
                            ChicaLocation = "CAM4B";
                        }
                        break;
                    case "CAM4B": // east corner
                        rand = Random.Range(0, 2);
                        if (rand == 0)
                        {
                            // east hallway
                            ChicaLocation = "CAM4A";
                        }
                        else
                        {
                            // right door
                            ChicaLocation = "RIGHTDOOR";
                        }

                        break;

                    case "RIGHTDOOR":
                        StopCoroutine("ChicaMovement");
                        ChicaLocation = "INSIDE";
                        chicainside = true;
                        ChicaLocation = null;
                        break;
                    default:
                        break;
                }
                if (ChicaLocation == CameraScript.buttonpublic || prevloc2 == CameraScript.buttonpublic)
                {
                    CameraScript.StartCoroutine("Blackout");
                }
                prevloc2 = ChicaLocation;
            }

            Debug.Log(BonnieLocation.ToString());
        }
    }
    IEnumerator FreddyMovementCheck()
    {
        while (true)
        {
            Debug.Log("FreddyMovementCheck");
            yield return new WaitForSeconds(3.01f);
            if (CameraIsntUp == true)
            {
                int rand = Random.Range(0, 21);
                if (rand < FreddyDifficulty)
                {
                    StartCoroutine("FreddyCountdown");
                }
            }
            else
            {
                Debug.Log("FreddyMovementCheckFail");
            }
        }



    }
    IEnumerator FreddyCountdown()
    {
        float waitTime = Mathf.Max(0, 16f - 1.67f * FreddyDifficulty);
        yield return new WaitForSeconds(waitTime);
        ReadyForFreddy = true;
    }
    IEnumerator FreddyMovement()
    {
            yield return new WaitForSeconds(0.1f);
            if (CameraIsntUp == true)
            {
                switch (FreddyLocation)
                {
                    case "CAM1A":
                        FreddyLocation = "CAM1B";
                        break;
                    case "CAM1B":
                        FreddyLocation = "CAM7";
                        break;
                    case "CAM7":
                        FreddyLocation = "CAM6";
                        break;
                    case "CAM6":
                        FreddyLocation = "CAM4A";
                        break;
                    case "CAM4A":
                        FreddyLocation = "CAM4B";
                        break;
                    default:
                        break;
                }
            }
        }

    }
