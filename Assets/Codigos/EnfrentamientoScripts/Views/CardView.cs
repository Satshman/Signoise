using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer imagen;
    [SerializeField] private GameObject backGround;
    [SerializeField] private LayerMask dropLayer;

    private Vector3 dragStartPosition;
    private Quaternion dragStartRotation;

    public Card Card { get; private set; }

    // Animación
    private bool isHovering = false;
    private Coroutine hoverCoroutine;

    public void Setup(Card card)
    {
        Card = card;
        imagen.sprite = card.Image;
    }

    void OnMouseEnter()
    {
        if (Camera.main == null) return;             // ← PROTECCIÓN
        if (!Interactions.Instance.PlayerCanHover()) return;

        backGround.SetActive(false);
        Vector3 pos = new(transform.position.x, 4f, 0);

        CardViewHoverSystem.Instance.Show(Card, pos);
        isHovering = true;

        StartHoverAnimation();
    }

    void OnMouseExit()
    {
        if (Camera.main == null) return;             // ← PROTECCIÓN
        if (!Interactions.Instance.PlayerCanHover()) return;

        CardViewHoverSystem.Instance.Hide();
        backGround.SetActive(true);

        isHovering = false;
        StopHoverAnimation();
    }

    private IEnumerator PlayHoverAnimation()
    {
        List<Sprite> frames = Card.Data.HoverFrames;

        if (frames == null || frames.Count == 0)
            yield break;

        int index = 0;

        while (isHovering)
        {
            imagen.sprite = frames[index];

            index++;
            if (index >= frames.Count)
                index = 0;

            yield return new WaitForSeconds(Card.Data.HoverFrameRate);
        }

        imagen.sprite = Card.Image;
    }

    public void StartHoverAnimation()
    {
        isHovering = true;

        if (hoverCoroutine != null)
            StopCoroutine(hoverCoroutine);

        hoverCoroutine = StartCoroutine(PlayHoverAnimation());
    }

    public void StopHoverAnimation()
    {
        isHovering = false;

        if (hoverCoroutine != null)
            StopCoroutine(hoverCoroutine);

        imagen.sprite = Card.Image;
    }

    private void OnMouseDown()
    {
        if (Camera.main == null) return;            // ← PROTECCIÓN
        if (!Interactions.Instance.PlayerCanInteract()) return;

        Interactions.Instance.PlayerIsDragging = true;

        backGround.SetActive(true);
        CardViewHoverSystem.Instance.Hide();

        dragStartPosition = transform.position;
        dragStartRotation = transform.rotation;

        transform.rotation = Quaternion.Euler(0, 0, 0);

        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
    }

    private void OnMouseDrag()
    {
        if (Camera.main == null) return;            // ← PROTECCIÓN
        if (!Interactions.Instance.PlayerCanInteract()) return;

        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
    }

    private void OnMouseUp()
    {
        if (Camera.main == null) return;            // ← PROTECCIÓN
        if (!Interactions.Instance.PlayerCanInteract()) return;

        // RAYCAST SEGURO
        bool validDrop = Physics.Raycast(
            transform.position,
            Vector3.forward,
            out RaycastHit hit,
            10f,
            dropLayer
        );

        if (ManaSystem.Instance.HasEnoughMana(Card.Mana) && validDrop)
        {
            PlayCardGA playCardGA = new(Card);
            ActionSystem.Instance.Perform(playCardGA);
        }
        else
        {
            transform.position = dragStartPosition;
            transform.rotation = dragStartRotation;
        }

        Interactions.Instance.PlayerIsDragging = false;
    }
}
