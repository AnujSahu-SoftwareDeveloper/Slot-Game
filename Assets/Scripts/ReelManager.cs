using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReelManager : MonoBehaviour
{
    public List<RectTransform> reelImages;  
    public RectTransform outterplaceholder; 
    private List<Vector3> positions;        
    public float spinDuration = 2f;         
    public float moveSpeed = 0.2f;      

    /// <summary>
    /// Set reels duration and speed
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="speed"></param>
    public void SetDurationandSpeed(float duration, float speed)
    {
        this.spinDuration = duration;
        this.moveSpeed = speed;
    }
    /// <summary>
    /// store a position of reels images 
    /// </summary>
    public void Init()
    {
        positions = new List<Vector3>();
        foreach (var image in reelImages)
        {
            positions.Add(image.position);
        }
        positions.Add(outterplaceholder.position); 
    }
    /// <summary>
    /// Reel spinning code 
    /// </summary>
    public void StartSpin()
    {
        StartCoroutine(SpinReel());
    }

    private IEnumerator SpinReel()
    {
        float elapsedTime = 0f;
        while (elapsedTime < spinDuration)
        {
            for (int i = 0; i < reelImages.Count; i++)
            {
                RectTransform currentImg = reelImages[i];
                int nextPosIndex = (i + 1) % positions.Count;
                Vector3 targetPosition = positions[nextPosIndex];
                currentImg.DOMove(targetPosition, moveSpeed).SetEase(Ease.Linear);
            }
            yield return new WaitForSeconds(moveSpeed);
            RectTransform lastImage = reelImages[reelImages.Count - 1];
            lastImage.position = positions[0]; 
            RectTransform movedImage = reelImages[reelImages.Count - 1];
            reelImages.RemoveAt(reelImages.Count - 1);
            reelImages.Insert(0, movedImage);
            elapsedTime += moveSpeed;
        }
        GameManager.Instance.OnReelSpinComplete();
    }
 
}
