//////////////////////////////////////////////
//Assignment/Lab/Project: Blackjack
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 03/18/2024
/////////////////////////////////////////////

using System.Collections.Generic;
using System.Linq;

public class Hand
{
    public List<Card> CardsInHand { get; set; } = new();

    public int CalculateHandScore()
    {
        var score = CardsInHand.Sum(card => card.CardValue);
        var elevenCount = CountAces();
        // Decrease value of aces when score is over 21
        while (elevenCount > 0 && score > 21)
        {
            score -= 10;
            elevenCount--;
        }

        return score;
    }

    private int CountAces()
    {
        return CardsInHand.Count(card => card.CardName.Contains("Ace"));
    }

    public void AddCardToHand(Card cardToAdd)
    {
        CardsInHand.Add(cardToAdd);
    }

    // For determining natural 21 or not
    public bool HasTwoCards()
    {
        return CardsInHand.Count == 2;
    }
}