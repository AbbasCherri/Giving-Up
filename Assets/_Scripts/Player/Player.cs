using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] public int maxhealth;
    [SerializeField] private Bar healthBar;
    public int currhealth;
    CharacterController characterController;
    private int lastIndex = -1;
    private float stepCooldown = 0;
    private bool wasgrounded;
    public static bool isReadingNote = false;
    private Note note;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        currhealth = maxhealth;
    }

    void Update()
    {
        double currtime = Time.time;
        
        if (currhealth <= 0)
        {
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveLoad.Save(this);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Loading...");
            PlayerData data = SaveLoad.Load();
            if (data != null)
            {
                currhealth = data.health;
                CharacterController cc = GetComponent<CharacterController>();
                cc.enabled = false;
                transform.position = data.position;
                cc.enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isReadingNote)
            {
                note.CloseNote();
                isReadingNote = false;
                note = null;
            }
            else
            {
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                if(Physics.Raycast(ray,out RaycastHit hit, 3f))
                {
                    note = hit.collider.GetComponentInParent<Note>();
                    if (note)
                    {
                        note.OpenNote();
                        isReadingNote = true;
                    }
                }
            }
        }
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if ((horizontal != 0 || vertical != 0) && characterController.isGrounded && Time.time > stepCooldown)
        {
            stepCooldown = (float)(currtime + 0.5);
            PlayFootstepSound();
            
        }

        if (!wasgrounded &&  characterController.isGrounded)
        {
            AudioManager.Instance.PlaySound("Land");

        }
        wasgrounded =  characterController.isGrounded;
        
    }

    public void TakeDamage(int damage)
    {
        currhealth -= damage;
        AudioManager.Instance.PlaySound("Hurt");
        healthBar.BarProgress(currhealth,maxhealth);
    }
    public void PlayFootstepSound()
    {
        
        int index = 1;
        do
        {
            index = Random.Range(0, 3);
        } while(index == lastIndex);
        lastIndex = index;
        AudioManager.Instance.PlaySound("Footstep" + (index+1));
    }
    
}
