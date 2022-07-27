using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

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

    [Header("Controllers")]
    [SerializeField] XRBaseController leftController;
    [SerializeField] XRBaseController rightController;
    [SerializeField] float defaultAmplitude = 0.2f;
    [SerializeField] float defaultDuration = 0.5f;

    ToolManager toolManager;
    TutorialEvents tutorialEvents;
    BlocksTracker blocksTracker;
    ControllerMaterial leftControllerMaterial;
    ControllerMaterial rightControllerMaterial;
    Scaler scaler;

    // bug. floor doesnt let place objects on grid because of collider
    // block scaler script.

    private void Awake()
    {
        tutorialEvents = GetComponent<TutorialEvents>();
        blocksTracker = FindObjectOfType<BlocksTracker>();
        toolManager = FindObjectOfType<ToolManager>();
        if(blocksTracker == null)
        {
            Debug.LogError("BlockTracker is missing. Did you put the script on the scene?");
        }
        scaler = FindObjectOfType<Scaler>();
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
        FindControllerComponents();
        StopAllCoroutines();
        tutorialEvents.NextInstruction();
        scaler.EnableChangeScale(false);
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
                yield return Introduction();
                break;
            case 1:
                yield return ShowProjectInformation();
                break;
            case 2:
                yield return AddFloor();
                break;
            case 3:
                yield return AddWalls();
                break;
            case 4:
                yield return EraseObjects();
                break;
            case 5:
                yield return AddFurniture();
                break;
            case 6:
                yield return FinalInformation();
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


    IEnumerator Introduction()
    {
        addFloorButton.ActivateButton(false);
        toolCanvas.gameObject.SetActive(false);
        instructionText.text = stepInstructions[instructionIndex].instructions[0];
        yield return new WaitForSeconds(3f);

        // INTRODUCTION
        //This function lets the user explore the level and waits until they
        // go to the trigger area to start the next Instruction.

        Debug.Log("INTRODUCTION");
        StartHaptics(0);
        // Text: Explore the level;
        yield return WaitUntilPressedNext();

        // Text: Move and rotate with the left thumbstick.
        instructionText.text = stepInstructions[instructionIndex].instructions[1];
        StartHaptics(0);

        StartCoroutine(leftControllerMaterial.BlinkThumstick());
        yield return WaitUntilPressedNext();
        leftControllerMaterial.StopBlinking();

        // Text: Go towards the arrow
        startTrigger.ShowArrow();
        instructionText.text = stepInstructions[instructionIndex].instructions[2];
        StartHaptics(0);
        // wait until tutorial trigger collider is true;
        yield return new WaitUntil(() => startTrigger.HasEntered);
    }

    IEnumerator ShowProjectInformation()
    {
        // INFORMATION
        Debug.Log("INFORMATION");
        
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
        StartHaptics(0);
        // wait until player has pressed button.
        yield return new WaitUntil(() => InfoButton.IsSelected);
        tutorialPointer.ShowArrow(false);
    }

    IEnumerator AddFloor()
    {
        // BUILD FLOOR
        buildTabsGroup.HidePages();
        Debug.Log("FLOOR");
        yield return new WaitForSeconds(2f);
        // Move arrow to build tab.
        tutorialPointer.ShowArrow(true);
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

        StartHaptics(0);
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

        StartHaptics(0);
        yield return new WaitUntil(() => floorTab.IsSelected);

        // Move arrow to dropdown
        tutorialPointer.GoToNextPosition();

        // Text: Choose a room from dropdown menu. (Show only bedroom as interactable)
        instructionText.text = stepInstructions[instructionIndex].instructions[1];
        yield return new WaitForSeconds(4f);

        // Move arrow to add button.
        tutorialPointer.GoToNextPosition();

        addFloorButton.ActivateButton(true);
        StartHaptics(0);
        // Wait until button has been pressed.
        yield return new WaitUntil(() => addFloorButton.IsPressed);
        StartHaptics(1);
        tutorialPointer.ShowArrow(false);

        // Text: Place the block on the grid.
        instructionText.text = stepInstructions[instructionIndex].instructions[2];
        // wait until first block is placed.
        Block floorBlock = blocksTracker.rooms[0].GetComponent<Block>();
        StartHaptics(2);
        yield return new WaitUntil(() => floorBlock.IsPlaced);

        
        // Text: Edit the size of the room, moving the handles.
        instructionText.text = stepInstructions[instructionIndex].instructions[3];
        // todo Test. wait until size is other than 1x1.
        StartHaptics(2);
        yield return new WaitUntil(() => RoomSizeChanged(floorBlock));

        // Text: When you finish editing the size, select again the object. 
        instructionText.text = stepInstructions[instructionIndex].instructions[4];
        StartHaptics(2);
        yield return new WaitUntil(() => !floorBlock.GetComponent<TransformBlock>().isEditing);
    }

    IEnumerator AddWalls()
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
        StartHaptics(0);
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
        StartHaptics(2);
        yield return new WaitUntil(() => blocksTracker.blockWalls.Count >= 1);
        tutorialPointer.ShowArrow(false);

        // Text: Rotate the direction with the right thumbstick.
        instructionText.text = stepInstructions[instructionIndex].instructions[2];
        StartHaptics(1); 

        // Text: Add some more.
        EnableButtons(wallButtons, true);

        // After placing five walls.
        yield return new WaitUntil(() => blocksTracker.blockWalls.Count >= 5);

        
    }

    IEnumerator EraseObjects()
    {
        // todo say how to erase.
        instructionText.text = stepInstructions[instructionIndex].instructions[0];
        // todo change material(color) of grip.
        StartHaptics(1);
        // todo Test.- press grip - wait until until toolinUse is eraser
        StartCoroutine(rightControllerMaterial.BlinkGripButton());
        yield return new WaitUntil(() => toolManager.toolInUse == ToolManager.ToolSelection.delete);
        rightControllerMaterial.StopBlinking();

        // Text: Select a wall to delete
        instructionText.text = stepInstructions[instructionIndex].instructions[1];
        StartHaptics(0);
        yield return WaitUntilPressedNext();
    }

    IEnumerator AddFurniture()
    {
        // ADD FURNITURE
        Debug.Log("FURNITURE");
        // Text: Furniture
        instructionText.text = stepInstructions[instructionIndex].instructions[0];
        // Go back to Build Tab
        mainTabsGroup.OnTabSelected(FindTab(mainTabsGroup,"Build"));
        // Move arrow to furniture tab.
        tutorialPointer.GoToNextPosition();
        tutorialPointer.ShowArrow(true);

        // wait until furniture tab is pressed
        TabButtons furnitureTab = FindTab(buildTabsGroup, "Furniture");
        furnitureTab.MakeInteractable();
        StartHaptics(0);
        yield return new WaitUntil(() => furnitureTab.IsSelected);

        // Text: Decorate this room with some furniture.
        instructionText.text = stepInstructions[instructionIndex].instructions[1];
        tutorialPointer.GoToNextPosition();
        StartHaptics(2);
        yield return new WaitUntil(() => blocksTracker.blockFurnitures.Count >= 1);
        tutorialPointer.ShowArrow(false);

        // Text: Place more furniture
        instructionText.text = stepInstructions[instructionIndex].instructions[2];
        // todo wait until After placing three blocks
        StartHaptics(2);
        yield return new WaitUntil(() => blocksTracker.blockFurnitures.Count >= 3);

        // Text: Keep designing this room. When you finish, you are ready to be a great designer.
        instructionText.text = stepInstructions[instructionIndex].instructions[3];
        StartHaptics(0);
        yield return WaitUntilPressedNext();
    }

    IEnumerator FinalInformation()
    {
        // FINAL INFORMATION
        Debug.Log("FINAL INFORMATION");
        // Text: Something more...
        instructionText.text = stepInstructions[instructionIndex].instructions[0];

        // todo Test. move arrow to Others tab
        tutorialPointer.ShowArrow(true);
        tutorialPointer.GoToNextPosition();

        // todo wait until player presses the tab
        TabButtons helpTab = FindTab(mainTabsGroup, "Help");
        helpTab.MakeInteractable();
        StartHaptics(0);
        yield return new WaitUntil(() => helpTab.IsSelected);

        // Text: Now, go to design amazing things
        instructionText.text = stepInstructions[instructionIndex].instructions[1];
        StartHaptics(0);

        tutorialPointer.GoToNextPosition();
        yield return null;
        tutorialPointer.ShowArrow(false);
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
        if (foundTab == null)
            Debug.LogWarning($"{tabName} doesnt correspond to any Tab, do all TabButtons have names?");
        return foundTab;
    }

    IEnumerator WaitUntilPressedNext()
    {
        tutorialButton.ShowButton(true);
        yield return new WaitUntil(() => tutorialButton.IsPressed);
        tutorialButton.ShowButton(false);
    }

    /// <summary>
    /// Starts Haptics on VR controllers
    /// 0: left controller, 1: right controller, 2: Both controllers
    /// </summary>
    /// <param name="controller">0: left controller, 1: right controller, 2: Both controllers</param>
    private void StartHaptics(int controller)
    {
        switch (controller)
        {
            case 0:
                // left
                leftController.SendHapticImpulse(defaultAmplitude, defaultDuration);
                break;
            case 1:
                // right
                rightController.SendHapticImpulse(defaultAmplitude, defaultDuration);
                break;
            case 2:
                // both
                leftController.SendHapticImpulse(defaultAmplitude, defaultDuration);
                rightController.SendHapticImpulse(defaultAmplitude, defaultDuration);
                break;
            default:
                Debug.LogWarning("Int side should be between 0 and 2 to Start Haptics");
                break;
        }       
    }

    private void FindControllerComponents()
    {
        leftControllerMaterial = leftController.GetComponentInChildren<ControllerMaterial>();
        if (leftControllerMaterial == null)
            Debug.LogError("ControllerMaterial component wasn't found in left Controller");

        rightControllerMaterial = rightController.GetComponentInChildren<ControllerMaterial>();
        if (rightControllerMaterial == null)
            Debug.LogError("ControllerMaterial component wasn't found in Right Controller");
    }

}
