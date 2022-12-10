using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Window_Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;


    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("XTemplate").GetComponent<RectTransform>();


        List<int> valueList = new List<int>() {50, 70, 68, 102, 100, 82, 100, 130, 120, 118, 117, 85, 78 };
        showGraph(valueList);
        //CreateCircle(new Vector2(299, 299));
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(1, 1); //DOT SIZE
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void showGraph(List<int> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        //float graphHeight = 1000; //figure out dynamics
        float yMaximum = 140f; //highest HR
        //float xSize = ((graphContainer.sizeDelta.x) / (float) valueList.Count) - graphContainer.sizeDelta.x; //learn to make dynamic based on size of list and dimension chosen
        float xSize = ((float) graphContainer.sizeDelta.x / (valueList.Count -1));
        Debug.Log(graphContainer.sizeDelta.x + "x");
        Debug.Log(graphContainer.sizeDelta.y + "y");
        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {
           float xPosition = i * xSize; //will be in two lists instead...
           float yPosition = (valueList[i] / yMaximum) * graphHeight; //normalize Value
           GameObject circleGameObject =  CreateCircle(new Vector2(xPosition, yPosition));
           if(lastCircleGameObject != null)
           {
              CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
           }
           lastCircleGameObject = circleGameObject;

            //can do with time if needed
            //RectTransform labelX = Instantiate(labelTemplateX);
            //labelX.SetParent(graphContainer);
            //labelX.gameObject.SetActive(true);
            //labelX.anchoredPosition = new Vector2(xPosition, -20f);
            //labelX.GetComponent<Text>().text = i.ToString();

        }
        //hard put this
        int seperatorCount = 10;
        for (int i = 0; i <= seperatorCount; i++)
        {
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.transform.SetParent(graphContainer, false);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            float normalizedValue = i * 1f / seperatorCount;
            labelX.anchoredPosition = new Vector2(-95f, normalizedValue * graphHeight);
            labelX.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue * yMaximum).ToString();
            labelX.transform.localScale = new Vector3(1, 1, 1);
        }

    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1f, 1f, 0.5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 6f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * 0.5f;

        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        rectTransform.localEulerAngles = new Vector3(0, 0, n);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
