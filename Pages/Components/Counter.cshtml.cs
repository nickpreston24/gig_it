using Hydro;

namespace gig_it.Pages.Components;

public class Counter : HydroComponent
{
    public int Count { get; set; }

    public void Add()
    {
        Count++;
    }
}
