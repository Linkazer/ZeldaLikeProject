using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire : MonoBehaviour {

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public int Count = 20;

    public static Inventaire instance;

    void Awake()
    {
        instance = this;
    }

    public List<Objet> items = new List<Objet>();

    public void Add(Objet item)
    {
        items.Add(item);
        if(onItemChangedCallBack!=null)
        {
            onItemChangedCallBack.Invoke();
        } 
    }

    public void Remove(Objet item)
    {
        items.Remove(item);
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }

}
