using System.Numerics;

namespace Game.Objects
{
    public enum InteractableType
    {
        lever,
        chest,
        enemy,
        door,
        vase,

    }
    public interface IInteractable 
    {
        InteractableType Type { get; set; }
        UnityEngine.Vector3 Vector3 { get; }
        void Interact();

    }
}