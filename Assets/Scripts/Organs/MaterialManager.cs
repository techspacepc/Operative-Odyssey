using System;
using System.Collections.Generic;
using System.Linq;
using Tags;
using UnityEditor;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public Dictionary<VisibilityManager.ManagedObjects, Material> managedMaterials = new();

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

        string[] visibilityManagedObjects = Enum.GetNames(typeof(VisibilityManager.ManagedObjects));

        GameObject[] organs = GameObject.FindGameObjectsWithTag(Tag.Organ);

        foreach (GameObject organ in organs)
            if (organ.TryGetComponent(out Renderer renderer)) materials.Add(renderer.sharedMaterial);

        foreach (Material material in materials)
        {
            bool matchFound = false;
            foreach (string enumName in visibilityManagedObjects)
            {
                if (material.name.Contains(enumName, StringComparison.OrdinalIgnoreCase))
                {
                    matchFound = true;
                    break;
                }
            }
            if (!matchFound)
                throw new KeyNotFoundException($"No matching name was found for the {nameof(VisibilityManager.ManagedObjects)} enum." +
                    $" Please ensure that the material name matches exactly one enum name.\nThe available enum names are: {string.Join(", ", visibilityManagedObjects)}.");

            Material copiedMaterial = new(material);
            AssetDatabase.CreateAsset(copiedMaterial, $"Assets/Resources/{copiedMaterial.name}Transparant.asset");
        }
        AssetDatabase.SaveAssets();
    }

    private void Awake()
    {
        string[] organTypes = Enum.GetNames(typeof(Organs.OrganType));
        string[] managedObjects = Enum.GetNames(typeof(VisibilityManager.ManagedObjects));
        if (!organTypes.All(organType => managedObjects.Contains(organType)))
        {
            throw new MissingMemberException($"Not all enum members of {nameof(Organs.OrganType)} are in the {nameof(VisibilityManager.ManagedObjects)} enum." +
                $" Please make sure that ALL enum members of {nameof(Organs.OrganType)} are within the {nameof(VisibilityManager.ManagedObjects)} enum.");
        }

        Material[] materials = (Material[])Resources.LoadAll(string.Empty);

        foreach (Material material in materials)
        {
            string materialName = material.name.Replace("Transparant", string.Empty).Trim();

            if (Enum.TryParse(materialName, true, out VisibilityManager.ManagedObjects materialEnum))
                managedMaterials[materialEnum] = material;
            else
                throw new MissingReferenceException($"No matching enum member found for the material: {materialName}");
        }
    }
}