
using System;
using System.Collections.Generic;
using System.Linq;

public class CharacterNameGenerator
{

    public static string GetRandomFirstName()
    {
        var namesTaken = GameState.Instance.PersistentCharacterRoster.Select(item => item.CharacterFullName);
        var firstName = new List<string>
        {
            "Johnny"
        }.PickRandom();

        return firstName;
    }

    public static CharacterName GenerateCharacterName()
    {
        return new CharacterName
        {
            FirstName = GetRandomFirstName(),
            LastName = "Lastname",
            Nickname = "Nickname"
        };
    }
}

public class CharacterName
{
    public string FirstName;
    public string LastName;
    public string Nickname;
}