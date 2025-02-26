namespace Terreiro.Domain.ValueObjects;

public class Period(DateTime startDate, DateTime? endDate)
{
    public DateTime StartDate { get; private set; } = startDate;
    public DateTime? EndDate { get; private set; } = endDate;

    public void SetPeriod(DateTime startDate, DateTime? endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}
