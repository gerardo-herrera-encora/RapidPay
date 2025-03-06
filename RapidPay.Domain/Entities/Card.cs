namespace RapidPay.Domain.Entities;

public class Card
{
    public string Number { get; private set; }
    public decimal Balance { get; private set; }

    public Card(string number, decimal initialBalance)
    {
        Number = number;
        Balance = initialBalance;
    }

    public void Pay(decimal amount)
    {
        if (amount <= 0)
        {
            throw new InvalidOperationException("Amount must be greater than zero");
        }
        if (Balance < amount)
        {
            throw new InvalidOperationException("Insufficient balance");
        }
        Balance -= amount;
    }
}