using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NPC_Actions : MonoBehaviour
{
    private NavMeshAgent agent;

    public bool isComplete;
    public bool isFollower;
    public bool isActive;

    public Transform playerTransform;
    public string npcName;

    public bool consumesRequirements;
    public Dictionary<Interactible_type, int> inventoryRequirements;
    //Para a criança
    public NPC_Actions[] actionRequirements;

    public Interactible_type[] inventoryReq;
    public int[] reqCount;
    
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
        inventoryRequirements = new Dictionary<Interactible_type, int>();

        if (inventoryReq.Length != reqCount.Length)
            Debug.LogError("Tamanhos diferentes");

        for (int i = 0; i < reqCount.Length; i++)
        {
            inventoryRequirements[inventoryReq[i]] = reqCount[i];
        }
    }

    private void Update()
    {
        if (isFollower)
            FollowPlayer();

        isActive = false;
    }

    public void Activate()
    {
        isActive = true;
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
