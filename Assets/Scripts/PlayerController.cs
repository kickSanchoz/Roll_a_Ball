using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float speed;
    private bool onGround;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI victoryText;
    public TextMeshProUGUI timeText;

    private int finalTimeText;

    private Rigidbody rb;
    private int score;
    private float startTime;

    [HideInInspector]
    public float finalTime;
    [HideInInspector]
    public bool finished = false;

    public float jumpForce = 10f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        scoreText.text = "Score: " + score.ToString();
        victoryText.enabled = false;
        onGround = true;
        startTime = Time.time;
    }
    private void Update()
    {
        if (onGround == true)
            Jump();

        if (finished == false)
        {
            finalTime = Time.time - startTime;
            finalTimeText = (int)finalTime;
            timeText.text = "Time: " + finalTimeText.ToString() + " sec";
            PlayerPrefs.SetInt("isFinished", 0);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            Physics.gravity = new Vector3(0, -20, 0);
        }            
    }

    void SetFinalText()
    {
        victoryText.enabled = true;
        victoryText.text = "VICTORY\n Total score: " + score.ToString() + "\nTime: " + finalTimeText.ToString() + " sec";
        scoreText.enabled = false;
        timeText.enabled = false;
        StartCoroutine(GoMenu());
    }


    IEnumerator GoMenu()
    {
        yield return new WaitForSeconds(3);
        PlayerPrefs.SetFloat("NeedTime", finalTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            score++;
            scoreText.text = "Score: " + score.ToString();
        }

        if (other.gameObject.CompareTag("StaticObject"))
        {
            onGround = true;
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            finished = true;
            PlayerPrefs.SetInt("isFinished", 1);
            SetFinalText();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("StaticObject"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("StaticObject"))
        {
            onGround = false;
        }
    }

}
