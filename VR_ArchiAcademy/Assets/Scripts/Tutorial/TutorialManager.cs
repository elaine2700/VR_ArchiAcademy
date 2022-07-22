using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] TutorialButton tutorialButton;
    [SerializeField] TutorialTriggerCollider startTrigger;
    [SerializeField] GameObject toolCanvas;
    [SerializeField] TabGroup mainTabsGroup;
    [SerializeField] TabGroup buildTabsGroup;
    [SerializeField] TutorialPointer tutorialPointer;
    [SerializeField] BlockButton addFloorButton;
    [SerializeField] GameObject wallInventoryGroup;
    [SerializeField] GameObject furnitureInventoryGroup;
    [SerializeField] List<StepInstructions> stepInstructions = new List<StepInstructions>();

    TutorialEvents tutorialEvents;
    BlocksTracker blocksTracker;
    int arrowIndex = 0;

    // todo missing how to delete
    // bug. floor doesnt let place objects on grid because of collider

    private void Awake()
    {
        tutorialEvents = GetComponent<TutorialEvents>();
        blocksTracker = FindObjectOfType<BlocksTracker>();
        if(blocksTracker == null)
        {
            Debug.LogError("BlockTracker is missing. Did you put the script on the scene?");
        }
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
        //buildTabsGroup.HidePages();
        //
        

        StopAllCoroutines();
        tutorialEvents.NextInstruction();
    }

    private void StartNewInstruction()
    {
        StopAllCoroutines();
        StartCoroutine(TellInstruction());
    }

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
        addFloorButton.ActivateButton(false);
        toolCanvas.gameObject.SetActive(false);
        instructionText.text = stepInstructions[instructionIndex].instructions[0];
        yield return new WaitForSeconds(3f);
        // INTRODUCTION
        //This function lets the user explore the level and waits until they
        // go to the trigger area to start the next Instruction.
        Debug.Log("INTRODUCTION");
        // Todo activate haptics to indicate to look at the controllers
        // Text: Explore the level;
        yield return WaitUntilPressedNext();

        // Text: Move and rotate with the left thumbstick.
        instructionText.text = stepInstructions[instructionIndex].instructions[1];
        yield return WaitUntilPressedNext();

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
        toolCanvas.SetActive(true);
        mainTabsGroup.HidePages();
        // Disable all buttons, except the info
        mainTabsGroup.DisableAllTabs();
        Debug.Log($"Main tabs number: {mainTabsGroup.tabButtons.Count}");
        TabButtons InfoButton = FindTab(mainTabsGroup, "Information");
        InfoButton.MakeInteractable();
        
        if (InfoButton == null)
        {
            Debug.LogError("InfoButton wasnt found. Do all TabButtons have names?." +
                "This coroutine will never end.");
        }
        // Animation: Indicate towards the info page.
        tutorialPointer.ShowArrow(true);
        // Text: "This is the information of the project and what you have to know to design your project.
        instructionText.text = stepInstructions[instructionIndex].instructions[0];
        // wait until player has pressed button.
        yield return new WaitUntil(() => InfoButton.IsSelected);
    }

    IEnumerator ThirdStep()
    {
        // BUILD FLOOR
        Debug.Log("FLOOR");
        // todo haptics

        // Move arrow to build tab.
        tutorialPointer.GoToNextPosition();

        // Text: Let's start...
        instructionText.text = stepInstructions[instructionIndex].instructions[0];
        TabButtons buildTab = FindTab(mainTabsGroup, "Build");
        buildTab.MakeInteractable();

        if (buildTab == null)
        {
            Debug.LogError("BuildTab wasnt found. Do all TabButtons have names?. " +
                "This coroutine will never end");
        }
        yield return new WaitUntil(() => buildTab.IsSelected);

        // Move arrow to floorTab
        tutorialPointer.GoToNextPosition();

        buildTabsGroup.DisableAllTabs();
        // Disable walls and furniture tab
        TabButtons floorTab = FindTab(buildTabsGroup, "Floor");
        floorTab.MakeInteractable();
        if (floorTab == null)
        {
            Debug.LogError("FloorTab wasnt found. Do all TabButtons have names?. " +
                "This coroutine will never end");
        }
        yield return new WaitUntil(() => floorTab.IsSelected);

        // Move arrow to dropdown
        tutorialPointer.GoToNextPosition();

        // Text: Choose a room from dropdown menu. (Show only bedroom as interactable)
        instructionText.text = stepInstructions[instructionIndex].instructions[1];
        yield return new WaitForSeconds(4f);

        // Move arrow to add button.
        tutorialPointer.GoToNextPosition();

        addFloorButton.ActivateButton(true);
        // Wait until button has been pressed.
        yield return new WaitUntil(() => addFloorButton.IsPressed);
        tutorialPointer.ShowArrow(false);

        // Text: Place the block on the grid.
        instructionText.text = stepInstructions[instructionIndex].instructions[2];
        // wait until first block is placed.
        Block floorBlock = blocksTracker.rooms[blocksTracker.rooms.Count - 1].GetComponent<Block>();
        yield return new WaitUntil(() => floorBlock.IsPlaced);

        
        // Text: Edit the size of the room, moving the handles.
        instructionText.text = stepInstructions[instructionIndex].instructions[3];
        // todo Test. wait until size is other than 1x1.
        yield return new WaitUntil(() => RoomSizeChanged(floorBlock));

        // -------Everything works before this line---------------------------
        // ---------------------------

        // Text: When you finish editing the size, select again the object. 
        instructionText.text = stepInstructions[instructionIndex].instructions[4];
        // ---repair here---
        // todo Test. wait until block is not in editMode
        yield return new WaitForSeconds(5f);
        yield return new WaitUntil(() => !floorBlock.isEditing);
    }

    

    IEnumerator FourthStep()
    {
        // BUILD WALLS
        Debug.Log("WALLS");
        // Move arrow to wallsTab.
        tutorialPointer.ShowArrow(true);
        tutorialPointer.GoToNextPosition();
        // Text: It is time to add some walls.
        instructionText.text = stepInstructions[instructionIndex].instructions[0];

        // wait until pressed wallTab
        TabButtons wallTab = FindTab(buildTabsGroup, "Walls");
        if(wallTab == null)
        {
            Debug.LogError("wallTab wasn't found. Do all TabButtons have names?. " +
                "This coroutine will never end");
        }
        wallTab.MakeInteractable();
        yield return new WaitUntil(() => wallTab.IsSelected);

        // Move arrow to first BlockButton
        tutorialPointer.GoToNextPosition();

        // Disable all walls buttons.
        Button[] wallButtons = wallInventoryGroup.GetComponentsInChildren<Button>();
        EnableButtons(wallButtons, false);
        wallButtons[0].interactable = true;

        // Text: Choose a block to place on grid.
        instructionText.text = stepInstructions[instructionIndex].instructions[1];

        // todo wait until there is a Wall on the BlocksTracker
        yield return new WaitUntil(() => blocksTracker.blockWalls.Count >= 1);
        tutorialPointer.ShowArrow(false);

        // Text: Rotate the direction with the right thumbstick.
        instructionText.text = stepInstructions[instructionIndex].instructions[2];
        yield return WaitUntilPressedNext();

        // Text: Add some more.
        EnableButtons(wallButtons, true);
        instructionText.text = stepInstructions[instructionIndex].instructions[3];

        // After placing five walls.
        yield return new WaitUntil(() => blocksTracker.blockWalls.Count >= 5);

        // todo say how to erase.
        // press grip until toolinUse is eraser
        // Text: Select a wall to delete
    }

    IEnumerator FifthStep()
    {
        // ADD FURNITURE
        Debug.Log("FURNITURE");

        // Move arrow to furniture tab.
        tutorialPointer.GoToNextPosition();
        tutorialPointer.ShowArrow(true);

        // wait until furniture tab is pressed
        TabButtons furnitureTab = FindTab(buildTabsGroup, "Furniture");
        furnitureTab.MakeInteractable();
        yield return new WaitUntil(() => furnitureTab.IsSelected);

        // Text: Decorate this room with some furniture.
        instructionText.text = stepInstructions[instructionIndex].instructions[0];
        tutorialPointer.GoToNextPosition();
        yield return new WaitUntil(() => blocksTracker.blockFurnitures.Count >= 1);

        // Text: Place more furniture
        instructionText.text = stepInstructions[instructionIndex].instructions[1];
        // todo wait until After placing three blocks
        yield return new WaitUntil(() => blocksTracker.blockFurnitures.Count >= 3);

        // Text: Keep designing this room. When you finish, you are ready to be a great designer.
        instructionText.text = stepInstructions[instructionIndex].instructions[2];
        yield return WaitUntilPressedNext();
    }

    IEnumerator SixthStep()
    {
        // FINAL INFORMATION
        Debug.Log("FINAL INFORMATION");
        // Text: Before you go to build amazing things...
        instructionText.text = stepInstructions[instructionIndex].instructions[0];
        yield return WaitUntilPressedNext();

        // todo Test. move arrow to Others tab
        tutorialPointer.GoToNextPosition();

        // todo wait until player presses the tab
        TabButtons helpTab = FindTab(mainTabsGroup, "Help");
        helpTab.MakeInteractable();
        yield return new WaitUntil(() => helpTab.IsSelected);

        // Text: Some more information...
        instructionText.text = stepInstructions[instructionIndex].instructions[1];

        yield return WaitUntilPressedNext();

        tutorialPointer.GoToNextPosition();
        yield return null;
    }

    private static bool RoomSizeChanged(Block floorBlock)
    {
        float currentFloorSizeX = floorBlock.GetComponent<Blockfloor_V2>().RoomSize.x;
        float currentFloorSizeY = floorBlock.GetComponent<Blockfloor_V2>().RoomSize.y;
        bool floorWasEdited = currentFloorSizeX > 1f || currentFloorSizeY > 1f;
        Debug.Log($"Floor Width: {currentFloorSizeX}, floor depth:{currentFloorSizeY}");
        return floorWasEdited;
    }

    private void EnableButtons(Button[] buttons, bool enable)
    {
        foreach (Button button in buttons)
        {
            button.interactable = enable;
        }
    }

    private TabButtons FindTab(TabGroup tabGroup, string tabName)
    {
        TabButtons foundTab = null;
        foreach(TabButtons tab in tabGroup.tabButtons)
        {
            if(tab.ButtonName == tabName)
            {
                foundTab = tab;
                break;
            }   
        }
        return foundTab;
    }

    IEnumerator WaitUntilPressedNext()
    {
        tutorialButton.ShowButton(true);
        yield return new WaitUntil(() => tutorialButton.IsPressed);
        tutorialButton.ShowButton(false);
    }
}
