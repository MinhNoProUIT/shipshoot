using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : BaseMonoBehaviour
{
    [SerializeField] protected List<Transform> stars = new List<Transform>();
    [SerializeField] protected LoadUI loadUI;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPoints();
        LoadUI();
    }

    protected virtual void LoadUI(){
         if (this.loadUI != null) return;
        this.loadUI = transform.parent.GetComponent<LoadUI>();
        Debug.LogWarning(transform.name + ": LoadUI", gameObject);
    }

    protected virtual void LoadPoints()
    {
        if (this.stars.Count > 0) return;
        foreach (Transform point in transform)
        {
            foreach (Transform star in point)
            {
                this.stars.Add(star);
            }
        }
    }

   protected virtual void SetCurrentStars(int count)
    {
        StartCoroutine(SetStarsCoroutine(count));
    }

    protected IEnumerator SetStarsCoroutine(int count)
    {
        int temp = 0;
        foreach (Transform star in stars)
        {
            if (temp >= count) break;
            star.gameObject.SetActive(true); // Kích hoạt ngôi sao
            temp++;
            yield return new WaitForSeconds(1f); // Tạm dừng 0.5 giây
        }
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        SetCurrentStars(this.loadUI.GetStarByTime());
    }
}
