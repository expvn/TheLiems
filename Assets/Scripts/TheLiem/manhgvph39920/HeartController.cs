using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartController : MonoBehaviour
{
    [SerializeField] private int maxHeart = 3;
    [SerializeField] private int heart;
    private Player player;

    public static HeartController instance;

    [Header("Canvas")]
    [SerializeField] private GameObject heartParent;
    [SerializeField] private Image imageHeart;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < maxHeart; i++)
        {
            Instantiate(imageHeart,heartParent.transform);
        }
        player = GetComponent<Player>();
        heart = maxHeart;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            TruHP(2);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            CongHP(1);
        }
    }

    public void TruHP(int tru)
    {
        heart -= tru;
        if(heartParent.transform.childCount < tru)
            tru = heartParent.transform.childCount;

        for (int i = 0; i < tru; i++)
        {
            GameObject heartChild = heartParent.transform.GetChild(i).gameObject;
            if (heartChild != null)
            {
                Destroy(heartChild);
            }
        }
        if (heart <= 0)
        {
            player.Death();
        }
        
    }

    public void CongHP(int cong)
    {
        heart += cong;
        if (heart > maxHeart)
        {
            heart = maxHeart;
            return;
        }
        Instantiate(imageHeart, heartParent.transform);
    }
}
