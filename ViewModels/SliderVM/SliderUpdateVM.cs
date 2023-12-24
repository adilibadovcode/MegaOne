namespace MegaOne.ViewModels.SliderVM;

public class SliderUpdateVM
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public string Description { get; set; }
    public bool IsLeft { get; set; }
    public IFormFile ImageFile { get; set; }

}
