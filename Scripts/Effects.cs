using System.Collections;
using UnityEngine;

public class Effects : MonoBehaviour
{
    private Animator animator;
    private ParticleSystem particle;
    public bool isTarget;
    private GridManager grid;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        particle = GetComponentInChildren<ParticleSystem>();
        grid = FindObjectOfType<GridManager>();
        animator.Play("BounceEffect");
        particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTarget)
            {
                Success();
            }
            else
            {
                animator.Play("easeInBounce");
            }
        }
    }

    private void Success()
    {
        animator.Play("bounce");
        particle.Play();
        StartCoroutine(GameOver());
        grid.dublesSign.Clear();
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<GridManager>().NewLevel();
    }


}
