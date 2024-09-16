namespace MovieApp.MVC.ViewModels.Genre
{
    public record GenreGetVM(int Id, string Name, DateTime CreatedAt, DateTime ModifiedAt, bool IsDeleted);
    
}
