using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class ListAnimation : MonoBehaviour
{
    /*[Title("Position Data")]
    [SerializeField] private Vector3 closePosition;
    [SerializeField] private Vector3 midPosition;
    [SerializeField] private Vector3 openPosition;

    [SerializeField] private float duration = 0.3f;
    [SerializeField] private Ease easeType = Ease.OutQuad;*/

    [SerializeField] private Animator animator;

    [ReadOnly, ShowInInspector]
    private bool isOpen = false;

    [Button]
    public void ToggleList()
    {
        isOpen = !isOpen;

        if (animator != null)
        {
            animator.SetBool("isOpen", isOpen);
        }

        /*Vector3 startPos = transform.localPosition;
        Vector3 midPos = midPosition;
        Vector3 endPos = isOpen ? openPosition : closePosition;

 

        Sequence parabolaSeq = DOTween.Sequence();

        parabolaSeq.Append(transform.DOLocalMove(midPos, duration / 2f)
                                    .SetEase(easeType));
        parabolaSeq.Append(transform.DOLocalMove(endPos, duration / 2f)
                                    .SetEase(easeType));*/
    }
}
