using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This class manages the instructions to give the user in the first scene.
/// </summary>
public class TutorialManager : MonoBehaviour
{
    int instructionIndex = 0;
    [SerializeField] Canvas instructionCanvas;
    [SerializeField] TextMeshProUGUI instructionText;
    [SerializeField] TutorialTriggerCollider startTrigger;
    [SerializeField] Canvas toolCanvas;
    [SerializeField] List<StepInstructions> stepInstructions = new List<StepInstructions>();

    [System.Serializable]
    private class StepInstructions
    {
        [TextArea(1, 20)]
        public List<string> instructions = new List<string>();
        [SerializeField] int index;
    }

    // Disable Main Canvas
    // Let the user to explore the level.
    // put a trigger collider in the scene. Indicate to go there with a bouncing arrow. 
    // Tell the user to move and rotate with the left thumbstick
    // When the user touches the trigger, go to next instruction.
    // activate haptics to indicate to look at the controllers
    // Disable all buttons, except the info
    // Indicate towards the info page. Text: "This is the information of the project and what you have to know to design your project.
    // Text: Let's start
    // Move arrow to build tab. Disable walls and furniture.
    // Move arrow to dropdown, Tell to choose a room from dropdown menu. Show only bedroom as interactable.
    // Move arrow to add button.
    // when choosen room type. Text: Place the block on the grid.
    // Edit the size of the room, moving the handles. When you finish editing the size, select again the object.
    // Move arrow to walls. Text: It is time to add some walls.
    // Text: Choose a block to place on grid.
    // Text: Rotate the direction with the rigth thumbstick.
    // Text: Add some more.
    // After placing five walls.
    // Move arrow to furniture tab.
    // Text: Decorate this room with some furniture.
    // After placing three blocks
    // Text: Keep designing this room. When you finish, you are ready to be a great designer.
    // Move arrow to help tab.
    // Enable all buttons.

    private void Start()
    {
        
    }

    private void NextInstruction()
    {
        instructionIndex++;
        TellInstruction();
    }

    private void TellInstruction()
    {
        switch (instructionIndex)
        {
            case 0:
                FirstStep();
                break;
            case 1:
                break;
            default:
                Debug.LogError("Instruction index out of bounds. Set int between 0 and 10");
                break;
        }
    }

    IEnumerator FirstStep()
    {
        toolCanvas.gameObject.SetActive(false);
        instructionText.text = stepInstructions[instructionIndex].instructions[0];
        yield return new WaitForSeconds(2f);
        instructionText.text = stepInstructions[instructionIndex].instructions[1];
        // wait until tutorial trigger collider is true;
        yield return new WaitUntil(() => startTrigger.HasEntered);
        NextInstruction();
    }


    
}
