using UnityEngine.XR.Interaction.Toolkit;

namespace Tags
{
    public static class Tag
    {
        public static readonly string Untagged = nameof(Untagged);
        public static readonly string Respawn = nameof(Respawn);
        public static readonly string Finish = nameof(Finish);
        public static readonly string EditorOnly = nameof(EditorOnly);
        public static readonly string MainCamera = nameof(MainCamera);
        public static readonly string Player = nameof(Player);
        public static readonly string GameController = nameof(GameController);
        public static readonly string Anchor = nameof(Anchor);
        public static readonly string Scalpel = nameof(Scalpel);
        public static readonly string Tray = nameof(Tray);
        public static readonly string Torso = nameof(Torso);
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