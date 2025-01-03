using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public Sprite Frame1, Frame2, Frame3, Frame4;
    public GameObject Pointer;
    public GameObject NewsPaper;
    public GameObject LowerCanvas;
    public GameObject NightIndicator;
    private bool newgame;
    // Use this for initialization
    void Start()
    {
        StartCoroutine("Animation");
        NightIndicator.GetComponent<Text>().text = "Night " + PlayerPrefs.GetInt("Night").ToString();
    }
    public int position = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (position > -1)
            {
                position -= 1;
                Pointer.transform.localPosition -= new Vector3(0, 53);
                NightIndicator.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (position < 0)
            {
                position += 1;
                Pointer.transform.localPosition += new Vector3(0, 53);
                NightIndicator.SetActive(false);
            }

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (newgame == false)
            {
                switch (position)
                {
                    default:
                        break;
                    case 0:
                        StartCoroutine("NewGame");
                        break;
                }
            }
            else
            {
                SceneManager.LoadSceneAsync("Office");
            }

        }
    }
    IEnumerator NewGame()
    {
        yield return null;
        LowerCanvas.SetActive(false);
        NewsPaper.SetActive(true);
        yield return new WaitForSeconds(3);
        newgame = true;
    }
    IEnumerator Animation()
    {
        while (true)
        {

            yield return new WaitForSeconds(0.01f + Random.Range(0.5f, 3f));
            int rand = Random.Range(0, 10);
            if (rand == 0)
            {
                gameObject.GetComponent<Image>().sprite = Frame3;
                yield return new WaitForSeconds(0.1f);
                gameObject.GetComponent<Image>().sprite = Frame4;
                yield return new WaitForSeconds(0.1f);
            }
            gameObject.GetComponent<Image>().sprite = Frame2;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<Image>().sprite = Frame1;
            yield return new WaitForSeconds(0.1f);

        }

    }
}
