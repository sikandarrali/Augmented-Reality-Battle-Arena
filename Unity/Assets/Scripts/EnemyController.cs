using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float lookRadius = 0.22f;
    private bool playerDead = false;
    private bool enemyMoveAway = false;

    public AudioSource audioSource;
    public AudioClip clip1;
    public AudioClip clip2 ;

    Transform enemySpawnPoint;
    [SerializeField]
    GameObject targetPlayerObj;
    CapsuleCollider playerCapsuleCollider;
    Transform targetPlayer;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerCapsuleCollider = GameObject.FindGameObjectWithTag("playerCharacter").GetComponent<CapsuleCollider>();
        enemySpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint_0").transform;
        targetPlayer = PlayerSingleton.playerInstance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (playerDead == false)
        {
            if(enemyMoveAway == false)
            {
                EnemyMoveTowards(targetPlayer);
            }
            else
            {
                StartCoroutine("WaitandFollowPlayer");
            }
        }
        else
        {
            StartCoroutine("WaitandFollowPlayer");
        }

        EnableDisableCollider();

        Debug.Log("Lives: " + ScoreManager.playerLives);

        if (playerDead != true)
            CheckIfPlayerIsDead();

    }

    // Move Enemy towards player or away
    private void EnemyMoveTowards(Transform etarget)
    {
        float distance = Vector3.Distance(etarget.position, transform.position);
       
        // Face Towards Target
        Vector3 direction = (etarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);

        agent.SetDestination(etarget.position);

    }

    // Collision Logic
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "playerCharacter")
        {
            audioSource.PlayOneShot(clip1);
            enemyMoveAway = true;
            ScoreManager.playerLives -= 1;
            playerCapsuleCollider.enabled = false;
        }


    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
    }

    private IEnumerator WaitatDesitnation()
    {
        audioSource.PlayOneShot(clip2);
        yield return new WaitForSeconds(1);
        agent.velocity = Vector3.zero;
    }

    private IEnumerator WaitandFollowPlayer()
    {
        EnemyMoveTowards(enemySpawnPoint);
        yield return new WaitForSeconds(4);
        enemyMoveAway = false;
    }

    private void EnableDisableCollider()
    {
        if (playerCapsuleCollider.enabled == false)
            StartCoroutine(LateCall());
    }
    IEnumerator LateCall()
    {
        yield return new WaitForSeconds(3);
        playerCapsuleCollider.enabled = true;
        //Do Function here...
    }


    private void CheckIfPlayerIsDead()
    {
        if (ScoreManager.playerLives == 0)
        {
            targetPlayer.GetComponent<Animator>().SetTrigger("Dead");
            playerDead = true;
        }
    }
}
