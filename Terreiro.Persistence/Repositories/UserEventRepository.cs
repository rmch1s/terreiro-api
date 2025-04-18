﻿using Terreiro.Application.Repositories;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Configurations;

namespace Terreiro.Persistence.Repositories;

internal class UserEventRepository(TerreiroDbContext db) : RepositoryBase<UserEvent>(db), IUserEventRepository;