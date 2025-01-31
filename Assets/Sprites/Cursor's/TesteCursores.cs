using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteCursores : MonoBehaviour
{
    public Texture2D[] cursors;  // Array para armazenar diferentes texturas de cursores
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    private int currentCursorIndex = 0;

    void Start()
    {
        ChangeCursor(currentCursorIndex);  // Define o cursor padrão ao iniciar

    }

    void Update()
    {
        //// Verifica as teclas do numpad e altera o cursor
        //if (Input.GetKeyDown(KeyCode.Keypad1))
        //{
        //    ChangeCursor(0);  // Cursor 1
        //}
        //else if (Input.GetKeyDown(KeyCode.Keypad2))
        //{
        //    ChangeCursor(1);  // Cursor 2
        //}
        //else if (Input.GetKeyDown(KeyCode.Keypad3))
        //{
        //    ChangeCursor(2);  // Cursor 3
        //}
        //else if (Input.GetKeyDown(KeyCode.Keypad4))
        //{
        //    ChangeCursor(3);  // Cursor 4
        //}
        //else if (Input.GetKeyDown(KeyCode.Keypad5))
        //{
        //    ChangeCursor(4);  // Cursor 5
        //}
        //else if (Input.GetKeyDown(KeyCode.Keypad6))
        //{
        //    ChangeCursor(5);  // Cursor 6
        //}
        //else if (Input.GetKeyDown(KeyCode.Keypad7))
        //{
        //    ChangeCursor(6);  // Cursor 7
        //}
        //else if (Input.GetKeyDown(KeyCode.Keypad8))
        //{
        //    ChangeCursor(7);  // Cursor 8
        //}
        //else if (Input.GetKeyDown(KeyCode.Keypad9))
        //{
        //    ChangeCursor(8);  // Cursor 9
        //}
        //else if (Input.GetKeyDown(KeyCode.Keypad0))
        //{
        //    ChangeCursor(9);  // Cursor 10 (se necessário)
        //}

        //// Verifica o scroll do mouse e altera o cursor
        //float scroll = Input.GetAxis("Mouse ScrollWheel");
        //if (scroll > 0f)
        //{
        //    ScrollCursorUp();
        //}
        //else if (scroll < 0f)
        //{
        //    ScrollCursorDown();
        //}
    }

    void ChangeCursor(int cursorIndex)
    {
        if (cursorIndex >= 0 && cursorIndex < cursors.Length)
        {
            currentCursorIndex = cursorIndex;
            Cursor.SetCursor(cursors[currentCursorIndex], hotSpot, cursorMode);
        }
        else
        {
            Debug.LogWarning("Índice de cursor inválido!");
        }
    }

    void ScrollCursorUp()
    {
        int nextIndex = currentCursorIndex + 1;
        if (nextIndex >= cursors.Length)
        {
            nextIndex = 0; // Volta ao primeiro cursor se ultrapassar o último
        }
        ChangeCursor(nextIndex);
    }

    void ScrollCursorDown()
    {
        int prevIndex = currentCursorIndex - 1;
        if (prevIndex < 0)
        {
            prevIndex = cursors.Length - 1; // Vai ao último cursor se passar do primeiro
        }
        ChangeCursor(prevIndex);
    }
}
