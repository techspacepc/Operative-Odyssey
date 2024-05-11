using Organs;
using System.Collections.Generic;
using Tags;
using UnityEditor;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    private Material FindMaterialByTag(string tag)
        => GameObject.FindGameObjectWithTag(tag).GetComponent<Renderer>().sharedMaterial;

    [ContextMenu(nameof(CreateTransparantMaterials))]
    private void CreateTransparantMaterials() // You do actually have to change the materials to transparant manually, afaik you cannot do that through code.
    {
        List<Material> materials = new()
        {
            FindMaterialByTag(Tag.Torso),
            FindMaterialByTag(Tag.Scalpel)
        };

        GameObject[] organs = GameObject.FindGameObjectsWithTag(Tag.Organ);

        foreach (GameObject organ in organs)
            if (organ.TryGetComponent(out Renderer renderer)) materials.Add(renderer.sharedMaterial);

        foreach (Material material in materials)
        {
            Material copiedMaterial = new(material);
            AssetDatabase.CreateAsset(copiedMaterial, $"Assets/Resources/{copiedMaterial.name}Transparant.asset");
        }
        AssetDatabase.SaveAssets();
    }
}