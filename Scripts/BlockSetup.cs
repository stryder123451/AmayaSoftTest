using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlockSetup : MonoBehaviour
{
    [SerializeField] private Sprite[] letters;
    [SerializeField] private Sprite[] numbers;
    private GridManager grid;
    private char[] _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<GridManager>();
        ChangeColor();
        SetSign();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ChangeColor()
    {
        if (TryGetComponent<SpriteRenderer>(out var sprite))
        {
            sprite.color = Color.white;
        }
    }

    private void SetSign()
    {
        switch (grid.Mode)
        {
            case 0:
                SetLetter();
                break;
            case 1:
                SetNumber();
                break;
        }
    }

    private void SetLetter()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            letters[i].name = _letters[i].ToString();
        }
        Transform[] ts = GetComponentsInChildren<Transform>();
        foreach (var x in ts)
        {
            if (x.TryGetComponent<Image>(out var image))
            {
                var unique = letters[Random.Range(0, letters.Length - 1)];

                if (grid.dublesSign.Contains(unique))
                {
                    Debug.Log("Duble");
                    image.sprite = letters[Random.Range(0, letters.Length - 1)];
                   // NewSprite(letters, image,2);
                }
                else
                {
                    image.sprite = unique;
                }
                grid.dublesSign.Add(image.sprite);
                break;
            }
        }
    }

    private void SetNumber()
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i].name = (i + 1).ToString();
        }
        Transform[] ts = GetComponentsInChildren<Transform>();
        foreach (var x in ts)
        {
            if (x.TryGetComponent<Image>(out var image))
            {
                var unique = numbers[Random.Range(0, numbers.Length - 1)];
                if (grid.dublesSign.Contains(unique))
                {
                   Debug.Log("Duble");
                   image.sprite = numbers[Random.Range(0, numbers.Length - 1)];
                   //StartCoroutine(NewSprite(numbers, image,2));
                }
                else
                {
                    image.sprite = unique;
                }
                grid.dublesSign.Add(image.sprite);
                if (image.sprite.name == "7" || image.sprite.name == "8")
                {
                    Quaternion rot = image.transform.rotation;
                    rot.eulerAngles = new Vector3(0, 0, -90);
                    image.transform.rotation = rot;
                }
                break;
            }
        }
    }

   

    IEnumerator NewSprite(Sprite [] sprites, Image image, double time)
    {
        Sprite a = null;
          
        while (time >= 0)
        {
            a = sprites[Random.Range(0, sprites.Length-1)];
            if (!grid.dublesSign.Contains(a))
            {
                image.sprite = a;
            }
            else
            {
                a = null;
            }
            time -= Time.deltaTime;
        }
        yield return null;
    } // Unstable
}

