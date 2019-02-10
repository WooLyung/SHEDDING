using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    private struct Components
    {
        public Image image;
        public RectTransform rectTransform;
        public Text text;
    }

    public enum SceneState
    {
        start1,
        start2,
        wait,
        end1,
        end2
    }

    public GameObject[] paint = new GameObject[4];
    public GameObject title;
    public GameObject panel;
    public GameObject touchToStart;
    public SceneState state = SceneState.start1;

    private Components[] Cpaint;
    private Components Ctitle, Cpanel, CtouchToStart;

    private void Awake()
    {
        Cpaint = new Components[4];
        Ctitle = new Components();
        Cpanel = new Components();
        CtouchToStart = new Components();

        Cpaint[0].rectTransform = paint[0].GetComponent<RectTransform>();
        Cpaint[1].rectTransform = paint[1].GetComponent<RectTransform>();
        Cpaint[2].rectTransform = paint[2].GetComponent<RectTransform>();
        Cpaint[3].rectTransform = paint[3].GetComponent<RectTransform>();
        Ctitle.text = title.GetComponent<Text>();
        CtouchToStart.text = touchToStart.GetComponent<Text>();
        Cpanel.rectTransform = panel.GetComponent<RectTransform>();
        Cpanel.image = panel.GetComponent<Image>();
    }

    private void Update()
    {
        switch (state)
        {
            case SceneState.start1:
                Color color = new Color(Cpanel.image.color.r, Cpanel.image.color.g, Cpanel.image.color.b, Cpanel.image.color.a - Time.deltaTime);
                Cpanel.image.color = color;

                if (color.a <= 0)
                    state = SceneState.start2;

                break;

            case SceneState.start2:
                if (Cpaint[0].rectTransform.position.y >= ConversPos('y', 1142))
                    Cpaint[0].rectTransform.position = new Vector3(Cpaint[0].rectTransform.position.x, Cpaint[0].rectTransform.position.y - Time.deltaTime * ConversPos('y', 750), Cpaint[0].rectTransform.position.z);
                if (Cpaint[1].rectTransform.position.y >= ConversPos('y', 1333))
                    Cpaint[1].rectTransform.position = new Vector3(Cpaint[1].rectTransform.position.x, Cpaint[1].rectTransform.position.y - Time.deltaTime * ConversPos('y', 700), Cpaint[1].rectTransform.position.z);
                if (Cpaint[2].rectTransform.position.y >= ConversPos('y', 1019))
                    Cpaint[2].rectTransform.position = new Vector3(Cpaint[2].rectTransform.position.x, Cpaint[2].rectTransform.position.y - Time.deltaTime * ConversPos('y', 550), Cpaint[2].rectTransform.position.z);
                if (Cpaint[3].rectTransform.position.y >= ConversPos('y', 1218))
                    Cpaint[3].rectTransform.position = new Vector3(Cpaint[3].rectTransform.position.x, Cpaint[3].rectTransform.position.y - Time.deltaTime * ConversPos('y', 640), Cpaint[3].rectTransform.position.z);

                if (Cpaint[0].rectTransform.position.y <= ConversPos('y', 1142)
                    && Cpaint[1].rectTransform.position.y <= ConversPos('y', 1333)
                    && Cpaint[2].rectTransform.position.y <= ConversPos('y', 1019)
                    && Cpaint[3].rectTransform.position.y <= ConversPos('y', 1218))
                    state = SceneState.wait;

                break;

            case SceneState.wait:
                if (Input.GetMouseButtonDown(0))
                    state = SceneState.end1;

                break;

            case SceneState.end1:
                Ctitle.text.color = new Color(1, 1, 1, Ctitle.text.color.a - Time.deltaTime * 2);
                CtouchToStart.text.color = new Color(0.2f, 0.2f, 0.2f, CtouchToStart.text.color.a - Time.deltaTime * 2);

                if (Ctitle.text.color.a <= 0)
                    state = SceneState.end2;

                break;

            case SceneState.end2:
                if (Cpaint[0].rectTransform.position.y >= ConversPos('y', 460))
                    Cpaint[0].rectTransform.position = new Vector3(Cpaint[0].rectTransform.position.x, Cpaint[0].rectTransform.position.y - Time.deltaTime * ConversPos('y', 750), Cpaint[0].rectTransform.position.z);
                if (Cpaint[1].rectTransform.position.y >= ConversPos('y', 460))
                    Cpaint[1].rectTransform.position = new Vector3(Cpaint[1].rectTransform.position.x, Cpaint[1].rectTransform.position.y - Time.deltaTime * ConversPos('y', 700), Cpaint[1].rectTransform.position.z);
                if (Cpaint[2].rectTransform.position.y >= ConversPos('y', 460))
                    Cpaint[2].rectTransform.position = new Vector3(Cpaint[2].rectTransform.position.x, Cpaint[2].rectTransform.position.y - Time.deltaTime * ConversPos('y', 550), Cpaint[2].rectTransform.position.z);
                if (Cpaint[3].rectTransform.position.y >= ConversPos('y', 460))
                    Cpaint[3].rectTransform.position = new Vector3(Cpaint[3].rectTransform.position.x, Cpaint[3].rectTransform.position.y - Time.deltaTime * ConversPos('y', 640), Cpaint[3].rectTransform.position.z);

                if (Cpaint[0].rectTransform.position.y <= ConversPos('y', 460)
                    && Cpaint[1].rectTransform.position.y <= ConversPos('y', 460)
                    && Cpaint[2].rectTransform.position.y <= ConversPos('y', 460)
                    && Cpaint[3].rectTransform.position.y <= ConversPos('y', 460))
                    SceneManager.LoadScene("Level");

                break;
        }
    }

    private float ConversPos(char axis, float scale)
    {
        return (axis == 'x') ? scale / 720.0f * Screen.width : scale / 1240.0f * Screen.height;
    }
}
