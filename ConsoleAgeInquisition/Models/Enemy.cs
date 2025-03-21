namespace ConsoleAgeInquisition.Models;

public class Enemy : Character
{
    /// <summary>
    /// Method to attack the hero
    /// </summary>
    /// <returns>Amount of HP hero should loose.</returns>
    public virtual int AttackHero()
    {
        var random = new Random();
        var randomNumber = random.Next(1, 11);

        return randomNumber switch
        {
            <= 10 and >= 7 => Attack, // 33% on a full hit
            <= 6 and >= 4 => Attack / 2, // 33% on a half hit
            <= 3 => 0,  // 33% on a miss
            _ => 0
        };
    }
}