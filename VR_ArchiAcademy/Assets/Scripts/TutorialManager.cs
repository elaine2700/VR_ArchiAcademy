using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// This class manages the instructions to give the user in the first scene.
/// </summary>
[RequireComponent(typeof(TutorialEvents))]
public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    private class StepInstructions
    {
        [TextArea(1, 20)]
        public List<string> instructions = new List<string>();
    }

    int instructionIndex = 0;
    [SerializeField] GameObject instructionCanvas;
    [SerializeField] TextMeshProUGUI instructionText;
    [SerializeField] TutorialTriggerCollider startTrigger;
    [SerializeField] GameObject toolCanvas;
    [SerializeField] List<StepInstructions> stepInstructions = new List<StepInstructions>();
    [SerializeField] TabGroup mainTabsGroup;
    [SerializeField] TabGroup buildTabGroup;
    [SerializeField] Animator tutorialPointer;

    TutorialEvents tutorialEvents;
    int arrowIndex = 0;

    private void Awake()
    {
        tutorialEvents = GetComponent<TutorialEvents>();
    }

    private void OnEnable()
    {
        tutorialEvents.NextStep.AddListener(StartNewInstruction);
    }

    private void OnDisable()
    {
        tutorialEvents.NextStep.RemoveListener(StartNewInstruction);
    }

    private void Start()
    {
        StopAllCoroutines();
        tutorialEvents.NextInstruction();
    }

    private void StartNewInstruction()
    {
        StopAllCoroutines();
        StartCoroutine(TellInstruction());
        //TellInstruction();
    }

    // toDo haptics
    // toDo arrow Animation
    

    IEnumerator TellInstruction()
    {
        switch (instructionIndex)
        {
            case 0:
                yield return FirstStep();
                break;
            case 1:
                yield return SecondStep();
                break;
            case 2:
                yield return ThirdStep();
                break;
            case 3:
                yield return FourthStep();
                break;
            case 4:
                yield return FifthStep();
                break;
            case 5:
                yield return SixthStep();
                break;
            default:
                Debug.LogError("Instruction index out of bounds. Set int between 0 and 5");
                break;
        }

        instructionIndex++;
        if(instructionIndex >= stepInstructions.Count)
        {
            StopAllCoroutines();
        }
        else
        {
            tutorialEvents.NextInstruction();
        }
    }


    IEnumerator FirstStep()
    {
        // INTRODUCTION
        //This function lets the user explore the level and waits until they
        // go to the trigger area to start the next Instruction.
        Debug.Log("INTRODUCTION");

        yield return new WaitForSeconds(2f);

        // Disable Main Canvas
        toolCanvas.gameObject.SetActive(false);
        // Todo activate haptics to indicate to look at the controllers
        // Text: Explore the level;
        instructionText.text = stepInstructions[instructionIndex].instructions[0];
        yield return new WaitForSeconds(3f);

        // Text: Move and rotate with the left thumbstick.
        instructionText.text = stepInstructions[instructionIndex].instructions[1];
        yield return new WaitForSeconds(3f);

        // Text: Go towards the arrow
        instructionText.text = stepInstructions[instructionIndex].instructions[2];
        // wait until tutorial trigger collider is true;
        yield return new WaitUntil(() => startTrigger.HasEntered);
    }

    IEnumerator SecondStep()
    {
        // INFORMATION
        Debug.Log("INFORMATION");

        // todo activate haptics to indicate to look at the controllers
        
        // Activate tools canvas without pages.
        mainTabsGroup.ShowPages(false);
        toolCanvas.SetActive(true);
        // Disable all buttons, except the info
        TabButtons InfoButton = null;
        foreach (TabButtons tabButton in mainTabsGroup.tabButtons)
        {
            if (tabButton.ButtonName == "Information")
            {
                InfoButton = tabButton;
                continue;
            }    
            tabButton.MakeNonInteractable();
            if(tabButton == null)
            {
                Debug.LogError("InfoButton wasnt found. Do all TabButtons have names?." +
                    "This coroutine will never end.");
            }
        }

        // Animation: Indicate towards the info page.
        ShowArrow(true);
        // Text: "This is the information of the project and what you have to know to design your project.
        instructionText.text = stepInstructions[instructionIndex].instructions[0];
        // wait until player has pressed button.
        yield return new WaitUntil(() => InfoButton.IsSelected);
        
        mainTabsGroup.ShowPages(true);
    }

    IEnumerator ThirdStep()
    {
        // BUILD FLOOR
        Debug.Log("FLOOR");
        // todo haptics
        // todo Move arrow to build tab.
        ArrowGoToNextPosition();
        // Text: Let's start...
        instructionText.text = stepInstructions[instructionIndex].instructions[0];

        TabButtons buildTab = null;
        foreach(TabButtons tabButton in mainTabsGroup.tabButtons)
        {
            if (tabButton.name == "Build")
            {
                buildTab = tabButton;
                buildTab.MakeInteractable();
                break;
            }
            if(buildTab == null)
            {
                Debug.LogError("BuildTab wasnt found. Do all TabButtons have names?. " +
                    "This coroutine will never end");
            }
        }
        yield return new WaitUntil(() => buildTab.IsSelected);
        

        // Disable walls and furniture tab
        TabButtons floorTab = null;
        foreach(TabButtons tab in mainTabsGroup.tabButtons)
        {
            if(tab.name == "Floor")
            {
                floorTab = tab;
                continue;
            }
            tab.MakeNonInteractable();
            if(floorTab == null)
            {
                Debug.LogError("FloorTab wasnt found. Do all TabButtons have names?. " +
                    "This coroutine will never end");
            }
        }

        // Move arrow to dropdown
        ArrowGoToNextPosition();

        // Text: Choose a room from dropdown menu. (Show only bedroom as interactable)
        instructionText.text = stepInstructions[instructionIndex].instructions[1];
        // todo change to wait until
        yield return new WaitForSeconds(3f);
        // todo Move arrow to add button.
        // todo when choosen room type.
        // todo wait until button has been pressed.

        // Text: Place the block on the grid.
        instructionText.text = stepInstructions[instructionIndex].instructions[2];
        yield return new WaitForSeconds(3f);

        // Text: Edit the size of the room, moving the handles.
        instructionText.text = stepInstructions[instructionIndex].instructions[3];
        yield return new WaitForSeconds(3f);
        // Text: When you finish editing the size, select again the object.
        instructionText.text = stepInstructions[instructionIndex].instructions[3];
        yield return new WaitForSeconds(3f);
    }

    IEnumerator FourthStep()
    {
        // BUILD WALLS
        Debug.Log("WALLS");
        // Move arrow to walls.

        // Text: It is time to add some walls.
        instructionText.text = stepInstructions[instructionIndex].instructions[0];
        yield return new WaitForSeconds(3f);

        // Text: Choose a block to place on grid.
        instructionText.text = stepInstructions[instructionIndex].instructions[1];
        yield return new WaitForSeconds(3f);

        // Text: Rotate the direction with the rigth thumbstick.
        instructionText.text = stepInstructions[instructionIndex].instructions[2];
        yield return new WaitForSeconds(3f);

        // Text: Add some more.
        instructionText.text = stepInstructions[instructionIndex].instructions[3];
        yield return new WaitForSeconds(3f);

        // After placing five walls.
        yield return null;
    }

    IEnumerator FifthStep()
    {
        // ADD FURNITURE
        Debug.Log("FURNITURE");
        // Move arrow to furniture tab.
        // Text: Decorate this room with some furniture.
        instructionText.text = stepInstructions[instructionIndex].instructions[0];
        yield return new WaitForSeconds(3f);

        // After placing three blocks
        // Text: Keep designing this room. When you finish, you are ready to be a great designer.
        instructionText.text = stepInstructions[instructionIndex].instructions[1];
        yield return new WaitForSeconds(3f);
        yield return null;
    }

    IEnumerator SixthStep()
    {
        // FINAL INFORMATION
        Debug.Log("FINAL INFORMATION");
        // Text: Before you go to build amazing things...
        instructionText.text = stepInstructions[instructionIndex].instructions[0];
        yield return new WaitForSeconds(3f);

        // todo move arrow to help tab
        // todo wait until player presses the tab
        // todo move arrow to Next

        // Text: Some more information, to return to the ordinary world.
        instructionText.text = stepInstructions[instructionIndex].instructions[1];
        yield return new WaitForSeconds(3f);
        // Move arrow to Others tab.
        // Enable all buttons.
        
        yield return null;
    }

    private void ShowArrow(bool showArrow)
    {
        tutorialPointer.gameObject.SetActive(showArrow);
    }

    private void ArrowGoToNextPosition()
    {
        arrowIndex++;
        tutorialPointer.SetInteger("ArrowPlaceIndex", arrowIndex);
    }
}
