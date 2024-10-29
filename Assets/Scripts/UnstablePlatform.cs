using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstablePlatform : MonoBehaviour
{
    [SerializeField] float breakingSpeed = 0.5f;
    [SerializeField] float rechargeSpeed = 3f;
    public PlatformStateSprites[] sprites;
    [SerializeField] private PlatformStages currentStage;
    [Space(10)]
    [SerializeField] private AudioSource soundStage1;
    [SerializeField] private AudioSource soundStage2;
    [SerializeField] private AudioSource soundStage3;
    [SerializeField] private bool reset = false;

    private void OnEnable()
    {
        ResetPlatforms.addPlatform(gameObject);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            //reset = true;
            StopAllCoroutines(); // Stop any running coroutines
            RegeneratePlatform(); // Regenerate immediately
        }
    }
    public void Reset()
    {
        reset=true;
        StopAllCoroutines(); // Stop any running coroutines
        RegeneratePlatform(); // Regenerate immediately
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentStage == PlatformStages.Idle)
        {
            StartCoroutine(PlatformChangingCor());
        }
    }

    public void NextPlatformStage()
    {
        PlatformStages newState = currentStage + 1;
        if (newState >= PlatformStages.count)
        {
            newState = PlatformStages.Idle;
        }
        PlatformChange(newState);
    }

    private IEnumerator PlatformChangingCor()
    {
        NextPlatformStage();
        soundStage1.Play();

        yield return new WaitForSeconds(breakingSpeed);
        if (reset) { RegeneratePlatform(); yield break; }
        soundStage2.Play();
        NextPlatformStage();

        yield return new WaitForSeconds(breakingSpeed);
        if (reset) { RegeneratePlatform(); yield break; }
        soundStage3.Play();
        NextPlatformStage();

        yield return new WaitForSeconds(0.2f);
        if (reset) { RegeneratePlatform(); yield break; }
        NextPlatformStage();

        yield return new WaitForSeconds(0.2f);
        if (reset) { RegeneratePlatform(); yield break; }
        NextPlatformStage();

        yield return new WaitForSeconds(0.2f);
        if (reset) { RegeneratePlatform(); yield break; }
        NextPlatformStage();

        // Recharge platform and respawn
        yield return StartCoroutine(WaitForRecharge(rechargeSpeed));
        RegeneratePlatform();
    }

    private IEnumerator WaitForRecharge(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            if (reset)
            {
                RegeneratePlatform();
                yield break; // Exit if reset is triggered
            }
            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
    }

    private void RegeneratePlatform()
    {
        reset = false; // Reset the boolean
        currentStage = PlatformStages.Idle; // Set to Idle stage
        DoStuffWithState(); // Call the method to apply the state changes
    }

    void PlatformChange(PlatformStages state)
    {
        currentStage = state;
        DoStuffWithState();
    }

    private void DoStuffWithState()
    {
        switch (currentStage)
        {
            case PlatformStages.Idle:
                GetComponent<BoxCollider2D>().enabled = true;
                break;
            case PlatformStages.Broken:
                GetComponent<BoxCollider2D>().enabled = false;
                break;
        }

        foreach (var sprite in sprites)
        {
            if (sprite.state == currentStage)
            {
                GetComponent<SpriteRenderer>().sprite = sprite.sprite;
                break;
            }
        }
    }
}

public enum PlatformStages
{
    Idle,
    Breaking1,
    Breaking2,
    Breaking3,
    Breaking4,
    Breaking5,
    Broken,
    Respawn1,
    Respawn2,
    Respawn3,

    count
}

[System.Serializable]
public struct PlatformStateSprites
{
    public PlatformStages state;
    public Sprite sprite;
}
