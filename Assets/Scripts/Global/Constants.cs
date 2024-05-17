using UnityEngine.XR.Interaction.Toolkit;

namespace Constants
{
    public static class Const
    {
        public const string MaterialInstance = " (Instance)";
        public const string MaterialTransparent = "Transparent";
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
        public const string ResourcesFull = "Assets/Resources/TransparentMaterials";
        public const string ResourcesShort = "TransparentMaterials";
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