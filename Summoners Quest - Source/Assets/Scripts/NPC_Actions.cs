using UnityEngine;
using UnityEngine.AI;

public class NPC_Actions : MonoBehaviour
{
    private NavMeshAgent agent;

    public bool isComplete = false;

    public bool isFollower;
    
    public Transform playerTransform;
    public string npcName;

    public bool consumesRequirements;
    public Interactible[] inventoryRequirements;
    public NPC_Actions[] actionRequirements;

    [TextArea(3,10)]
    public string givesQuest;
    [TextArea(3, 10)]
    public string askForQuest;
    [TextArea(3, 10)]
    public string thankyou;
    [TextArea(3, 10)]
    public string givesClue;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isFollower = false;
    }

    private void Update()
    {
        if(isFollower)
            FollowPlayer();
    }

    public void FollowPlayer()
    {
        if (isFollower && agent != null && isComplete == false)
            agent.destination = playerTransform.position;
        else if (isComplete)
            agent = null;
    }

    public void PlayAnimation(string animationName)
    {
        Animator animator = GetComponent<Animator>();

        if (animator != null)
            animator.SetTrigger(animationName);
    }
}
