using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewHoverSystem : Singleton<CardViewHoverSystem>
{
    [SerializeField] private CardView cardViewHover;
    //Animacion
    [SerializeField] private CardView hoverCardView;
    public void Show(Card card, Vector3 position)
    {
        cardViewHover.gameObject.SetActive(true);
        cardViewHover.Setup(card);
        cardViewHover.transform.position = position;
        //Animacion
        hoverCardView.StartHoverAnimation();
    }

    public void Hide()
    {
        //Animacion
        cardViewHover.StopHoverAnimation();
        //
        hoverCardView.gameObject.SetActive(false);

    }
}
