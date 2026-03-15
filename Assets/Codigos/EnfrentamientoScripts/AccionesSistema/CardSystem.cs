//using DG.Tweening;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class CardSystem : Singleton<CardSystem>
//{
//    [SerializeField] private HandView handView;
//    [SerializeField] private Transform drawPilePoint;
//    [SerializeField] private Transform discardPilePoint;
//    private readonly List<Card> drawPile = new ();
//    private readonly List<Card> discardPile = new();
//    private readonly List<Card> hand = new();
//
//    private void OnEnable()
//    {
//        ActionSystem.AttachPerformer<DrawCardsGa>(DrawCardsPerformer);
//        ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
//        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
//        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyturnPreReaction, ReactionTiming.PRE);
//        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
//    }
//
//    private void OnDisable()
//    {
//        ActionSystem.DetachPerformer<DrawCardsGa>();
//        ActionSystem.DetachPerformer<DiscardAllCardsGA>();
//        ActionSystem.DetachPerformer<PlayCardGA>();
//        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyturnPreReaction, ReactionTiming.PRE);
//        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
//    }
//    //Publics
//    public void Setup(List<CardData> deckData)
//    {
//        foreach (var cardData in deckData)
//        {
//            Card card = new(cardData);
//            drawPile.Add(card);
//        }
//    }
//
//    //Performers
//
//    private IEnumerator DrawCardsPerformer(DrawCardsGa drawCardsGa)
//    {
//        int actualAmount = Mathf.Min(drawCardsGa.Amount, drawPile.Count);
//        int notDrawnAmount=drawCardsGa.Amount-actualAmount;
//        for (int i = 0; i < actualAmount; i++)
//        {
//            yield return DrawCard();
//        }
//        if (notDrawnAmount>0)
//        {
//            RefillDeck();
//            for (int i = 0; i < notDrawnAmount; i++)
//            {
//                yield return DrawCard();
//            }
//        }
//    }
//
//    private IEnumerator DiscardAllCardsPerformer(DiscardAllCardsGA discardAllCardsGA)
//    {
//        foreach (var card in hand)
//        {
//            CardView cardView =handView.RemoveCard(card); 
//            yield return DiscardCard(cardView);
//        }
//        hand.Clear();
//    }
//
//    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
//    {
//        hand.Remove(playCardGA.Card);
//        CardView cardView = handView.RemoveCard(playCardGA.Card);
//        yield return DiscardCard(cardView);
//
//        SpendManaGA spendManaGA = new(playCardGA.Card.Mana);
//        ActionSystem.Instance.AddReaction(spendManaGA);
//
//        foreach (var effectWrapper in playCardGA.Card.OtherEffects)
//        {
//            List<CombatanView> targets = effectWrapper.TargetMode.GetTargets();
//            PerformEffectGA performEffectGA =new(effectWrapper.Effect,targets);
//            ActionSystem.Instance.AddReaction(performEffectGA);
//        }
//    }
//
//    //Reacciones
//    private void EnemyturnPreReaction(EnemyTurnGA enemyTurnGA)
//    {
//        DiscardAllCardsGA discardAllCardsGA = new();
//        ActionSystem.Instance.AddReaction(discardAllCardsGA);
//    }
//
//    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
//    {
//        DrawCardsGa drawCardsGa = new(6);
//        ActionSystem.Instance.AddReaction(drawCardsGa);
//    }
//
//    private IEnumerator DrawCard()
//    {
//        Card card = drawPile.Draw();
//        hand.Add(card);
//        CardView cardView=CardViewCreator.Instance.CreateCardView(card,drawPilePoint.position,drawPilePoint.rotation);
//        yield return handView.AddCard(cardView);
//    }
//
//    private void RefillDeck()
//    {
//        drawPile.AddRange(discardPile);
//        discardPile.Clear();
//    }
//
//    private IEnumerator DiscardCard(CardView cardView)
//    {
//        discardPile.Add(cardView.Card);
//        cardView.transform.DOScale(Vector3.zero, 0.15f);
//        Tween tween = cardView.transform.DOMove(discardPilePoint.position, 0.15f);
//        yield return tween.WaitForCompletion();
//        Destroy(cardView.gameObject);
//    }
//
//}
//