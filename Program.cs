using System;

class Program
{
    static void Main(string[] args)
    {
        const string UsualAttack = "1";
        const string FireBallAttack = "2";
        const string RemedyAttack = "3";
        const string BlowAttack = "4";

        string typeOfAttack;
        string typeOfAttackPrewiusRaund = "";
        int enemyHealth = 100;
        int heroHealth = 100;
        int heroMana = 100;
        int enemyHealthMax = enemyHealth;
        int heroHealthMax = heroHealth;
        int heroManaMax = heroMana;
        int deltaMana = 10;
        int usualAttackDamageMin = 15;
        int usualAttackDamageMax = 25;
        int fireBallAttackDamageMin = 0;
        int fireBallAttackDamageMax = 40;
        int remedyAttackMin = 20;
        int remedyAttackMax = 33;
        int blowAttackDamageMin = 0;
        int blowAttackDamageMax = 100;
        int countOfRound = 1;
        int countUseRemedyAttack = 0;
        int limitRemedyAttackTry = 3;
        Random random = new Random();

        while ((enemyHealth > 0) && (heroHealth > 0))
        {
            Console.WriteLine($"Герой жизнь {heroHealth}    герой мана {heroMana}   злодей жизнь {enemyHealth}");
            Console.WriteLine($"Раунд № {countOfRound}");
            Console.WriteLine($"Атака: обычная {UsualAttack}; огненный шар { FireBallAttack};" +
                              $" лечение {RemedyAttack}; взрыв {BlowAttack} ");
            Console.Write("Герой атакует злодея, выберете заклинание: ");
            typeOfAttack = Console.ReadLine();
            countOfRound++;
            Console.WriteLine();

            switch (typeOfAttack)
            {
                case UsualAttack:
                    enemyHealth -= random.Next(usualAttackDamageMin, usualAttackDamageMax);
                    heroHealth -= random.Next(usualAttackDamageMin, usualAttackDamageMax);
                    break;

                case FireBallAttack:
                    if ((heroMana -= deltaMana) > 0)
                        enemyHealth -= random.Next(fireBallAttackDamageMin, fireBallAttackDamageMax);
                    else
                        enemyHealth -= random.Next(usualAttackDamageMin, usualAttackDamageMax);

                    heroHealth -= random.Next(usualAttackDamageMin, usualAttackDamageMax);
                    break;

                case BlowAttack:
                    if (typeOfAttackPrewiusRaund == FireBallAttack)
                    {
                        enemyHealth -= random.Next(blowAttackDamageMin, blowAttackDamageMax);
                    }
                    else
                    {
                        enemyHealth -= random.Next(usualAttackDamageMin, usualAttackDamageMax);
                        Console.WriteLine("Вы попытались использовать атаку взрыв," +
                                          " не после огненного шара," +
                                          " вместо нее теперь будет использоватся обычная атака");
                    }

                    heroHealth -= random.Next(blowAttackDamageMin, blowAttackDamageMax);
                    break;

                case RemedyAttack:
                    countUseRemedyAttack++;

                    if (countUseRemedyAttack <= limitRemedyAttackTry)
                    {
                        heroHealth += random.Next(remedyAttackMin, remedyAttackMax);

                        if (heroHealth > heroHealthMax)
                            heroHealth = heroHealthMax;

                        heroMana += random.Next(remedyAttackMin, remedyAttackMax);

                        if (heroMana > heroManaMax)
                            heroMana = heroManaMax;
                    }
                    else
                    {
                        Console.WriteLine("Вы потратили все запасы восстановления");
                    }

                    enemyHealth += random.Next(remedyAttackMin, remedyAttackMax);

                    if (enemyHealth > enemyHealthMax)
                        enemyHealth = enemyHealthMax;

                    break;

                default:
                    Console.WriteLine("Герой пропускает ход, атака босса");
                    heroHealth -= random.Next(usualAttackDamageMin, usualAttackDamageMax);
                    break;
            }

            typeOfAttackPrewiusRaund = typeOfAttack;
        }

        Console.WriteLine($"Конец игры");
        Console.WriteLine($"Герой жизнь {heroHealth}    герой мана {heroMana}   злодей жизнь {enemyHealth}");

        if (heroHealth <= 0 && enemyHealth <= 0)
            Console.WriteLine("Оба проиграли");
        else if (heroHealth <= 0)
            Console.WriteLine("Враг победил");
        else if (enemyHealth <= 0)
            Console.WriteLine("Герой победил");
    }
}