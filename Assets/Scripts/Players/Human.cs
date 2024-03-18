//////////////////////////////////////////////
//Assignment/Lab/Project: Blackjack
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 03/18/2024
/////////////////////////////////////////////

public class Human : Player
{
    public Human(Dealer dealer)
    {
        GameDealer = dealer;
    }

    // Human references dealer for hitting
    private Dealer GameDealer { get; }

    public override void Hit()
    {
        var topCard = GameDealer.DrawCardFromDeck();
        PlayerHand.AddCardToHand(topCard);

        RaiseOnHit(topCard);
        // Avoid multiple stays before turn
        if (!IsActive) return;
        CheckForBust();
        CheckForBlackjackOr21();
    }
}