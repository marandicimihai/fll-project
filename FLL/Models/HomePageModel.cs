using FLL.Models;

public class HomePageModel
{
    public Exhibit? LikedExhibit { get; set; }
    public IEnumerable<Exhibit> Exhibits { get; set; }
}