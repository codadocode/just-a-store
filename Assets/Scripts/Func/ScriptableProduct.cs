using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Produto", menuName = "Produto", order = 1)]
public class ScriptableProduct : ScriptableObject
{
    [SerializeField]
    private string nome;
    [SerializeField]
    private float preco;
    [SerializeField]
    private GameObject prefab;
    public ScriptableProduct(string nome, float preco)
    {
        this.nome = nome;
        this.preco = preco;
    }
    public string getNome()
    {
        return this.nome;
    }
    public void setNome(string nome)
    {
        this.nome = nome;
    }
    public float getPreco()
    {
        return this.preco;
    }
    public void setPreco(float preco)
    {
        this.preco = preco;
    }
    public GameObject instancePrefab()
    {
        GameObject tmpInstancePrefab = Instantiate<GameObject>(prefab);
        tmpInstancePrefab.GetComponent<Product>().disableGravity();
        return tmpInstancePrefab;

    }
}
