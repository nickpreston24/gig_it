
namespace gig_it.Models;

public record Person(string Name, string[] Hobbies)
{
    public override string ToString()
    {
        return $"{Name} likes {Hobbies.ToOxfordComma()}";
    }
};