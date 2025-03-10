﻿using Microsoft.EntityFrameworkCore;
using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Data;

namespace Terreiro.Persistence.Repositories;

public class EventRepository(TerreiroDbContext db) : Repository<Event>(db), IEventRepository
{
    public async Task<IEnumerable<Event>> Get(DateTime? startDate, DateTime? endDate) =>
        await dbSet
            .AsNoTracking()
            .Where(e =>
                !e.DeletedAt.HasValue &&
                (!startDate.HasValue || e.Period.StartDate >= startDate) &&
                (!endDate.HasValue || e.Period.StartDate <= endDate)
        ).ToListAsync();
}
