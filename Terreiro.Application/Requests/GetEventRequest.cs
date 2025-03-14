namespace Terreiro.Application.Requests;

public record GetEventRequest(DateTime? StartDate, DateTime? EndDate);
