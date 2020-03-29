using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameArea : MonoBehaviour
{
    [System.Serializable]
    public class ButtonSpawnChance
    {
        public float chance;
        public GameObject button;
    }

    [SerializeField] float m_mainUIAbove = 325f;
    [SerializeField] float m_mainUIBelow = 150f;
    [SerializeField] Vector2Int m_gridSize;
    [SerializeField] List<ButtonSpawnChance> m_buttons;
    [SerializeField] Vector2Int m_maxButtonSize;
    [SerializeField] bool m_stopBigSquares;

    float m_gridWidth;
    Vector2 m_unitSize;

    RectTransform m_rect;
    Vector4 m_edge = new Vector4(1, 1, 1, 1);

    public void Start()
    {
        m_rect = GetComponent<RectTransform>();
        var scaleFactor = transform.root.GetComponent<Canvas>().scaleFactor; //GetComponent<Canvas>();

        float width = Screen.width / scaleFactor;
        float height = (Screen.height / scaleFactor) - m_mainUIAbove - m_mainUIBelow;

        m_gridWidth = width < height ? width : height;

        m_rect.sizeDelta = new Vector2(m_gridWidth, m_gridWidth);
        m_rect.localPosition = new Vector3(0, (m_mainUIBelow - m_mainUIAbove) / 2, 0);

        m_unitSize = new Vector2(m_gridWidth / (m_gridSize.x + 2), m_gridWidth / (m_gridSize.y + 2));   //+2 to account for boarder

        SetupDungeon();
    }

    public void SetupDungeon()
    {
        var spaceCheck = new bool[m_gridSize.x, m_gridSize.y];

        for (int i = 0; i < m_gridSize.x; i++)
        {
            for (int j = 0; j < m_gridSize.y; j++)
            {
                if (spaceCheck[i, j] == false)
                {
                    var newButton = Instantiate(PickButton(), transform).transform;

                    var buttonSize = GetButtonSize();

                    //Check space is clear
                    for (int heightCheck = buttonSize.y; heightCheck >= 0; heightCheck--)
                    {
                        if (j + heightCheck >= m_gridSize.y)
                        {
                            buttonSize.y = heightCheck - 1;
                        }
                        else
                        {
                            for (int widthCheck = buttonSize.x; widthCheck >= 0; widthCheck--)
                            {
                                if (i + widthCheck >= m_gridSize.x)
                                {
                                    buttonSize.x = widthCheck - 1;
                                }
                                else if (spaceCheck[i + widthCheck, j + heightCheck])
                                {
                                    buttonSize.y = heightCheck - 1;
                                }
                            }
                        }
                    }
                    
                    //Reserve space
                    for (int resWidth = 0; resWidth <= buttonSize.x; resWidth++)
                    {
                        for (int resHeight = 0; resHeight <= buttonSize.y; resHeight++)
                        {
                            spaceCheck[i + resWidth, j + resHeight] = true;
                        }
                    }
                    var buttonRect = newButton.GetComponent<RectTransform>();

                    buttonRect.sizeDelta = new Vector2(m_unitSize.x * (buttonSize.x + 1), m_unitSize.y * (buttonSize.y + 1));
                    buttonRect.anchoredPosition = new Vector3((i + 1) * m_unitSize.x, -(j + 1) * m_unitSize.y, 0);  //+1 to account for boarder

                    Debug.Log(i + ", " + j + "\n" + SpaceCheckToString(spaceCheck));
                }
            }
        }
    }

    private Vector2Int GetButtonSize()
    {
        Vector2Int buttonSize = new Vector2Int(Random.Range(0, m_maxButtonSize.x), Random.Range(0, m_maxButtonSize.y));

        if (m_stopBigSquares && (buttonSize.x == buttonSize.y && buttonSize.x == m_maxButtonSize.x - 1 && buttonSize.y == m_maxButtonSize.y - 1))
        {
            buttonSize = GetButtonSize();
        }

        return buttonSize;
    }

    private GameObject PickButton()
    {
        GameObject button = null;

        float totalChance = 0;
        for(int i= 0; i < m_buttons.Count; i++)
        {
            totalChance += m_buttons[i].chance;
        }

        float randomNumber = Random.Range(0f, totalChance);

        for(int i = 0; randomNumber > 0; i++)
        {
            randomNumber -= m_buttons[i].chance;

            if(randomNumber <= 0)
            {
                button = m_buttons[i].button;
            }
        }

        return button;
    }

    private void SetupDoor()
    {

    }

    private string SpaceCheckToString(bool[,] spaceCheck)
    {
        string room = string.Empty;

        for(int j =0; j < spaceCheck.GetLength(1); j++)
        {
            for (int i = 0; i < spaceCheck.GetLength(0); i++)
            {
                room += spaceCheck[i, j] ? "." : " ";
            }
                room += "\n";
        }

        return room;
    }
}
