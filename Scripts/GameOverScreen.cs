using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (TryGetComponent<Animator>(out var animator))
        {
            animator.Play("GameOverScreen");
        }
    }

}
