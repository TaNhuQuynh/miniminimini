using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    //private void Start()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    private void Update()
    {
        foreach (FloatingText txt in floatingTexts)
        {
            txt.UpdateFloatingText();
        }
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingTexts = GetFloatingText();

        floatingTexts.txt.text = msg;
        floatingTexts.txt.fontSize = fontSize;
        floatingTexts.txt.color = color;

        floatingTexts.go.transform.position = Camera.main.WorldToScreenPoint(position); // transfer world space to sceen space so we can use it in the UI
        floatingTexts.motion = motion;
        floatingTexts.duration = duration;

        floatingTexts.Show();
    }

    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.active);

        if (txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;
    }
}
