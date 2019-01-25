using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class for NPC's actions
/// </summary>
public class NPCActions : MonoBehaviour
{
    // Instance of NavMeshAgent
    private NavMeshAgent agent;
    // Bool variable set at false
    private bool isRunning = false;
    // Bool variable set at false
    private bool isIdle = false;

    // Bool variable to check if the mission is complete
    public bool isComplete;
    // Bool variable to check if the npc is a follower
    public bool isFollower;
    // Bool variable to check if it's active
    public bool isActive;

    // Transform variable
    public Transform playerTransform;
    // String for the npc's names
    public string npcName;

    // Bool variable to check if the requirement was consumed
    public bool consumesRequirements;
    // Array of InteractibleType
    public InteractibleType[] inventoryRequirements;
    // Array of NPCActions
    public NPCActions[] actionRequirements;
    
    // Text box that will appear on Unity's editor
    [TextArea(3,10)]
    // String for givesQuest text
    public string givesQuest;
    // Text box that will appear on Unity's editor
    [TextArea(3, 10)]
    // String for askForQuest text
    public string askForQuest;
    // Text box that will appear on Unity's editor
    [TextArea(3, 10)]
    // String for thankyoy text
    public string thankyou;
    // Text box that will appear on Unity's editor
    [TextArea(3, 10)]
    // String for givesClue text
    public string givesClue;

    /// <summary>
    /// Start method
    /// </summary>
    void Start()
    {
        // Sets the agent to the NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        // Sets it at false
        isFollower = false;
    }

    /// <summary>
    /// Update method
    /// </summary>
    private void Update()
    {
        // An if to check if it's following
        if (isFollower) {
            // If it isn't running
            if (!isRunning)
            {
                // Plays the "BoyRun" animation
                PlayAnimation("BoyRun", true);
                // Sets it at true
                isRunning = true;
            }
            // Calls the FollowPlayer() method
            FollowPlayer();            
        }
        else
        {
            // It it's not on idle
            if (!isIdle)
            {
                // Stops playing the "BoyRun" animation
                PlayAnimation("BoyRun", false);
            }
        }
    }

    /// <summary>
    /// Method for the follow player
    /// </summary>
    public void FollowPlayer()
    {
        // If the mission isn't complete
        if (isFollower && agent != null && isComplete == false)
            // Sets the agent's position to the player's position
            agent.destination = playerTransform.position;
        // If the mission is complete
        else if (isComplete)
            // Set the agent at null
            agent = null;
    }

    /// <summary>
    /// Method that will play the animation
    /// </summary>
    /// <param name="animationName"></param>
    /// <param name="isAnimated"></param>
    public void PlayAnimation(string animationName, bool isAnimated)
    {
        // Creates an instance of Animator
        Animator animator = GetComponentInChildren<Animator>();

        // Sets it as bool
        animator.SetBool(animationName, isAnimated);
    }
}
