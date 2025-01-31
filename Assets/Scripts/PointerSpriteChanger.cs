using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointerSpriteChanger 
{
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;
    [field: SerializeField] public Texture2D Attack {  get; private set; }
    [field: SerializeField] public Texture2D Dialogue {  get; private set; }
    [field: SerializeField] public Texture2D Default {  get; private set; }
    [field: SerializeField] public Texture2D Lever { get; private set; }
    //[field: SerializeField] public Texture2D Interaction { get; private set; }
    [field: SerializeField] public Texture2D Question { get; private set; }

    public void AttackPointer()
    {
        UpdateCursor(Attack);
    }
    public void DialoguePointer()
    {
        UpdateCursor(Dialogue);
    }
    public void ResetPointer()
    {
        UpdateCursor(Default);
    }
    public void LeverPointer()
    {
        UpdateCursor(Lever);
    }
    public void QuestionPointer()
    {
        UpdateCursor(Question);
    }

    private void UpdateCursor(Texture2D texture)
    {
        Cursor.SetCursor(texture, hotSpot, cursorMode);
    }
}
