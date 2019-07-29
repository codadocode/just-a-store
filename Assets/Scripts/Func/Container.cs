using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField]
    private int maxSize = 0;
    private List<ScriptableProduct> inventario;
    // Start is called before the first frame update
    void Start()
    {
        this.inventario = new List<ScriptableProduct>();
    }
    public void guardarItem(Product item)
    {
        if (inventario.Count < maxSize)
        {
            inventario.Add(item.getProduto());
            Destroy(item.gameObject);
            Debug.Log(inventario.ToArray().ToString());
        }else
        {
            Debug.Log("Container está cheio!");
        }
    }
    public void getItem()
    {
        if (inventario.Count > 0)
        {
            Debug.Log("Retirar Item!");
        }else
        {
            Debug.Log("Não há itens para retirar!");
        }
    }
}
