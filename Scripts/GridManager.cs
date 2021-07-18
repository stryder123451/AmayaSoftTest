using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class GridManager : MonoBehaviour
{
    public int Mode;
    private GameObject gameOverScreen;
    private TMP_Text objectiveText;
    private GameObject target;
    private string targetName;
    private int difficulty=1;
    public List<string> dubles;
    public List<Sprite> dublesSign;
    private Button restartButton;
    // Start is called before the first frame update
    void Start()
    {
       dubles = new List<string>();
       dublesSign = new List<Sprite>();
       gameOverScreen = GameObject.Find("GameOverScreen");
       gameOverScreen.SetActive(false);
       restartButton = gameOverScreen.GetComponentInChildren<Button>();
       restartButton.gameObject.SetActive(false);
       objectiveText = GameObject.Find("TargetText").GetComponent<TMP_Text>();
       objectiveText.gameObject.SetActive(false);
       CreateLevel(difficulty);  
    }


    private void GenerateGrid(int rows, int col, float tileSize)
    {
        Mode = Random.Range(0, 2);
        GameObject gridTile = (GameObject)Instantiate(Resources.Load("Block"));
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < col; j++)
            {
                GameObject tile = Instantiate(gridTile, transform);
                float posX = j * tileSize;
                float posY = i * -tileSize;
                tile.transform.position = new Vector2(posX, posY);
            }
        }
        Destroy(gridTile);
        transform.position = Vector2.zero;
        StartCoroutine(MainTask());
    }


    // Update is called once per frame
    void Update()
    {
       
    }

    private void SetTarget()
    {
        BoxCollider2D[] boxColliders = GetComponentsInChildren<BoxCollider2D>();
        int targetInd = Random.Range(0, boxColliders.Length);
        target = boxColliders[targetInd].gameObject;
        SetUniqueSign();
        objectiveText.gameObject.SetActive(true);
        objectiveText.text = "Find " + targetName;       
    }


    private void SetUniqueSign()
    {
        Image image = target.GetComponentInChildren<Image>();
        if (image.TryGetComponent<Image>(out var sign))
        {
            if (sign.sprite != null)
            {
                targetName = sign.sprite.name;
                dubles.Add(targetName);
            }
        }
        if (target.TryGetComponent<Effects>(out var effects))
        {
            effects.isTarget = true;
        }
    }

    IEnumerator MainTask()
    {
        yield return new WaitForSeconds(0.2f);
        SetTarget();
    }

    public void NewLevel()
    {
        StartCoroutine(RestartLevel());
    }

    public void Restart()
    {
        difficulty = 0;
        NewLevel();
    }

    private void Clear()
    {
        BoxCollider2D[] boxColliders = GetComponentsInChildren<BoxCollider2D>();
        foreach (var x in boxColliders)
        {
            Destroy(x.gameObject);
        }
        objectiveText.gameObject.SetActive(false);
        if (difficulty != 4)
        {
            difficulty++;
        }
    }

    private void CreateLevel(int index)
    {
        switch (index)
        {
            case 1:
                GenerateGrid(1, 3, 1.25f);
                break;
            case 2:
                GenerateGrid(2, 3, 1.25f);
                break;
            case 3:
                GenerateGrid(3, 3, 1.25f);
                break;
            case 4:
                StartCoroutine(GameOverScreen());
                break;
        }
       
    }

    IEnumerator GameOverScreen()
    {
        Clear();
        gameOverScreen.SetActive(true);
        objectiveText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        restartButton.gameObject.SetActive(true);
    }

    IEnumerator RestartLevel()
    {       
        yield return new WaitForSeconds(1f);
        gameOverScreen.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        Clear();
        CreateLevel(difficulty);
    }
}
