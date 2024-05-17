using Constants;
using Pathing;
using System;
using System.Collections.Generic;
using Tags;
using UnityEditor;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public Dictionary<string, Material> managedMaterials = new();

    private string RemoveInstanceNaming(string name) => name.Replace(Const.MaterialInstance, string.Empty);
    private string RemoveTransparentNaming(string name) => name.Replace(Const.MaterialTransparent, string.Empty);
    public string GetBaseMaterialName(string name) => RemoveTransparentNaming(RemoveInstanceNaming(name)).Trim();

    private Material FindMaterialByTag(string tag)
        => GameObject.FindGameObjectWithTag(tag).GetComponent<Renderer>().sharedMaterial;
    private List<string> FindMaterialNamesByTag(string tag)
    {
        List<string> materialNames = new();
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject gameObject in gameObjects)
            materialNames.Add(gameObject.GetComponent<Renderer>().sharedMaterial.name);

        return materialNames;
    }

    private void CreateVisibilityManagedMaterialsList(out List<string> visibilityManagedMaterials)
    {
        visibilityManagedMaterials = FindMaterialNamesByTag(Tag.Organ);
        visibilityManagedMaterials.Add(FindMaterialByTag(Tag.Torso).name);
        visibilityManagedMaterials.Add(FindMaterialByTag(Tag.Scalpel).name);
    }

    private MissingReferenceException MissingManagedMaterialReference(in Material material)
    {
        CreateVisibilityManagedMaterialsList(out List<string> visibilityManagedMaterials);

        string materialName = RemoveTransparentNaming(material.name);
        bool matchFound = false;
        foreach (string managedMaterialName in visibilityManagedMaterials)
        {
            if (materialName == managedMaterialName)
            {
                matchFound = true;
                break;
            }
        }
        if (matchFound) return null;
        else return new MissingReferenceException($"No matching {nameof(Material)} name was found for the {nameof(Material)} >{materialName}<." +
                $" Please ensure that the material name matches exactly one of the following material names: {string.Join(", ", visibilityManagedMaterials)}."); ;
    }

    [ContextMenu(nameof(CreateTransparentMaterials))]
    private void CreateTransparentMaterials() // You do actually have to change the materials to transparent manually, afaik you cannot do that through code.
    {
        List<Material> materials = new()
        {
            FindMaterialByTag(Tag.Torso),
            FindMaterialByTag(Tag.Scalpel)
        };

        GameObject[] organs = GameObject.FindGameObjectsWithTag(Tag.Organ);

        foreach (GameObject organ in organs)
            if (organ.TryGetComponent(out Renderer renderer)) materials.Add(renderer.sharedMaterial);
            else throw new InvalidOperationException($"{nameof(GameObject)} >{organ}<, tagged with the {Tag.Organ} tag, did not have a {nameof(Renderer)} attached to it." +
                $" Please make sure ONLY {nameof(GameObject)}s with the {nameof(Renderer)} component AND the {nameof(Organs.OrganIdentifier)} component, are assigned with the {Tag.Organ} tag.");

        foreach (Material material in materials)
        {
            MissingReferenceException exception = MissingManagedMaterialReference(material);
            if (exception != null) throw exception;

            Material copiedMaterial = new(material);
            AssetDatabase.CreateAsset(copiedMaterial, $"{Path.ResourcesFull}/{copiedMaterial.name}{Const.MaterialTransparent}.asset");
        }
        AssetDatabase.SaveAssets();
    }

    private void Awake()
    {
        Material[] materials = Resources.LoadAll<Material>(Path.ResourcesShort);

        foreach (Material material in materials)
        {
            MissingReferenceException exception = MissingManagedMaterialReference(material);
            if (exception != null) throw exception;

            managedMaterials[RemoveTransparentNaming(material.name)] = material;
        }
    }
}