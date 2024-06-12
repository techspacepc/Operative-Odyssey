using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Constants
{
    public static class Const
    {
        public const string MaterialInstance = " (Instance)";
        public const string MaterialTransparent = "Transparent";

        public const string SurfaceType = "RenderType";
    }
}

namespace Tags
{
    public static class Tag
    {
        public const string Untagged = nameof(Untagged);
        public const string Respawn = nameof(Respawn);
        public const string Finish = nameof(Finish);
        public const string EditorOnly = nameof(EditorOnly);
        public const string MainCamera = nameof(MainCamera);
        public const string Player = nameof(Player);
        public const string GameController = nameof(GameController);
        public const string Anchor = nameof(Anchor);
        public const string Scalpel = nameof(Scalpel);
        public const string Tray = nameof(Tray);
        public const string Torso = nameof(Torso);
        public const string Organ = nameof(Organ);
    }
}

namespace Pathing
{
    public static class Path
    {
        public const string TransparentShort = "TransparentMaterials";
        public const string TransparentLong = "Assets/Resources/" + TransparentShort;

        public const string CuttingSystem = nameof(CuttingSystem) + "/";
        public const string UncutMaterial = CuttingSystem + "Uncut";
        public const string Cutmaterial = CuttingSystem + "Cut";
        public const string IncisionObject = CuttingSystem + nameof(IncisionObject);
    }
}

namespace Extensions
{
    public static class Extensions
    {
        public static void RemoveComponent<T>(this GameObject gameObject) where T : Component
            => Object.DestroyImmediate(gameObject.GetComponent<T>());
    }
}

namespace InteractionLayerManagement
{
    public enum InteractionLayer
    {
        Default,
        OrganTray,
        OrganSocket,
        Teleport
    }

    public static class InteractionLayerManagement
    {
        public static int AddAllInteractionLayers(this XRGrabInteractable interactable) => interactable.interactionLayers = -1;
        public static int RemoveAllInteractionLayers(this XRGrabInteractable interactable) => interactable.interactionLayers = 0;
        public static int AddInteractionLayer(this XRGrabInteractable interactable, InteractionLayer interactionLayer) => interactable.interactionLayers |= 1 << (int)interactionLayer;
        public static int RemoveInteractionLayer(this XRGrabInteractable interactable, InteractionLayer interactionLayer) => interactable.interactionLayers &= ~(1 << (int)interactionLayer);
    }
}

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

namespace MessageSuppression
{
    public static class Suppress
    {
        public const string PendingJustification = "<Pending>";

        public readonly struct Style
        {
            public const string Category = "Style";
            public const string CheckId = "IDE1006:Naming Styles";
            public const string Justification = PendingJustification;
        }

        public readonly struct CodeQuality
        {
            public const string Category = "CodeQuality";
            public const string CheckId = "IDE0051:Remove unused private members";
            public const string Justification = PendingJustification;
        }
    }
}