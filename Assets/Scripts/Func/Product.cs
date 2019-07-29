using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : Pickable
{
    [SerializeField]
    private ScriptableProduct produto;
    public ScriptableProduct getProduto()
    {
        return this.produto;
    }
}
