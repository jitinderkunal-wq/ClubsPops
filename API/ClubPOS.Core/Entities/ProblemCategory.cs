namespace ClubPOS.Core.Entities;

public class ProblemCategory : BaseEntity
{
    // public int ProblemCategoryId { get; set; }
    public string? ProblemCategoryName { get; set; }

    public virtual ICollection<Problem>? Problems { get; set; }
}