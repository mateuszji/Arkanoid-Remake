using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksManager : MonoBehaviour
{
    #region Singleton

    private static BricksManager _instance;
    public static BricksManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion

    public Sprite[] bricksRed;
    public Sprite[] bricksGreen;
    public Sprite[] bricksBlue;

    public Sprite getSprite(string color, int hits)
    {
        switch(color)
        {
            case "red":
                return bricksRed[hits - 1];
            case "green":
                return bricksGreen[hits - 1];
            case "blue":
                return bricksBlue[hits - 1];
            default:
                return null;
        }
    }

    public string generateColor()
    {
        string[] colorsS = { "red", "green", "blue" };
        int index = Random.Range(0, colorsS.Length);
        return colorsS[index];
    }
}
